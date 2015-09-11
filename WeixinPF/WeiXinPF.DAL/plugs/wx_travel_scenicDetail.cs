using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_travel_scenicDetail
    {
        public wx_travel_scenicDetail()
        {
        }

        public int Add(Model.wx_travel_scenicDetail model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                var trans = db.BeginTransaction();
                try
                {

                    string query = @"Insert Into dbo.wx_travel_scenicDetail
                                (ScenicId,Name,Cover,BackgroundImage,Digest,Content,Audio,AutoAudio,LoopAudio,Video,AutoVideo,OriginalLink)
                                Values  (@ScenicId,@Name,@Cover,@BackgroundImage,@Digest,@Content,@Audio,@AutoAudio,@LoopAudio,@Video,@AutoVideo,@OriginalLink)
                                Select @Id=Scope_Identity()
                                ";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.AddDynamicParams(model);
                    dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    db.Execute(query, dynamicParameters, trans);

                    model.Id = dynamicParameters.Get<int>("@Id");

                    if (model.Albumses.Any())
                    {
                        string queryAlbums = @"Insert Into dbo.wx_travel_albums
                                                    (ScenicDetailId, ThumbPath)
                                               Values
                                                    (@ScenicDetailId,@ThumbPath)
                                             ";

                        db.Execute(queryAlbums, model.Albumses.Select(albums => new
                        {
                            ScenicDetailId = model.Id,
                            ThumbPath = albums.ThumbPath
                        }),trans);
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
                return model.Id;
            }
        }

        public Model.wx_travel_scenicDetail GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = @"Select * From dbo.wx_travel_scenicDetail Where Id=@Id
                                 Select * From dbo.wx_travel_albums Where ScenicDetailId=@Id";

                using (var multi = db.QueryMultiple(query, new { Id = id }))
                {
                    var model = multi.Read<Model.wx_travel_scenicDetail>().Single();
                    model.Albumses = multi.Read<Model.wx_travel_albums>().ToList();

                    return model;
                }
            }
        }
    }
}
