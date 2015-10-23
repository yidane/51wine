using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_hotel_room_manage
    {
        public int Add(Model.wx_hotel_room_manage model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Insert Into[dbo].[wx_hotel_room_manage]");
            query.Append("   ([RoomId],[Operator],[OperateName],[OperateTime],[Comment])");
            query.Append("Values");
            query.Append("  (@RoomId, @Operator, @OperateName, @OperateTime, @Comment)");
            query.Append("Select @Id = Scope_Identity()");

            using(IDbConnection db = DbFactory.GetOpenedConnection())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(query.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");

            }
        }
    }
}
