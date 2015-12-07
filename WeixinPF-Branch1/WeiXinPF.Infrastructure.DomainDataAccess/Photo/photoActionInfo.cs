using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Photo
{
    [Table("wx_travel_photoActionInfo")]
    public partial class photoActionInfo
    {
        #region Properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int wid { get; set; }
        public string actName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createTime { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string brief { get; set; }
        public string actContent { get; set; }
        public bool isAllowSharing { get; set; }

        public virtual ICollection<photoUserParticipated> Users { get; set; }
        public virtual ICollection<photoScenesInfo> Scenes { get; set; }

        #endregion

       
    }

    public partial class photoActionInfo
    {
        #region Method

        /// <summary>
        /// 获取列表
        /// </summary> 
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<photoActionInfo> GetList(
            Expression<Func<photoActionInfo, bool>> predicate)
        {
            IQueryable<photoActionInfo> result = null;
            var db = new WXDBContext();
            result = db.photoActionInfo.Where(predicate);

            return result;
        }



        #endregion

        public void Add()
        {
            var db = new WXDBContext();
            db.photoActionInfo.Add(this);
            db.SaveChanges();

        }

        public void Modify()
        {
            var db = new WXDBContext();
            db.photoActionInfo.Attach(this);
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();

        }


        public static void Delete(int id)
        {
            var db = new WXDBContext();
            var info=new photoActionInfo() {id=id};
            db.photoActionInfo.Attach(info);
            db.photoActionInfo.Remove(info);
            db.SaveChanges();
        }

        public static photoActionInfo GetModel(int id)
        {
            photoActionInfo result = null;
            var db = new WXDBContext();
            result = db.photoActionInfo.Find(id);
            return result;
        }

        public static bool Exists(int id)
        {
            bool result = false;
            var db = new WXDBContext();
             
            if (db.photoActionInfo.Any(p=>p.id==id))
            {
                result = true;
            }
            return result;
        }
    }
}
