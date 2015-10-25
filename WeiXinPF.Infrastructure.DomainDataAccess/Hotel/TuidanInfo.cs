using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Hotel
{
    [Table("wx_hotel_tuidan_manage")]
    public partial class TuidanInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  DateTime CreateTime { get; set; }
        public int dingdanid { get; set; }
        public int hotelid { get; set; }
        public string openid { get; set; }
        public int wid { get; set; }
        public int roomid { get; set; }
        public DateTime refundTime { get; set; }
        public double refundAmount { get; set; }
        public int operateUser { get; set; }
        public string remarks { get; set; }
    }

    public partial class TuidanInfo
    {
        #region Method
        public void Add()
        {
            var db = new WXDBContext();
//            this.CreateTime = DateTime.Now;
            db.TuidanInfo.Add(this);
            db.SaveChanges();

        }

        public void Modify()
        {
            var db = new WXDBContext();
            db.TuidanInfo.Attach(this);
            db.Entry(this).State = EntityState.Modified;
            db.SaveChanges();

        }


        public static void Delete(int id)
        {
            var db = new WXDBContext();
            var info = new TuidanInfo() { id = id };
            db.TuidanInfo.Attach(info);
            db.TuidanInfo.Remove(info);
            db.SaveChanges();
        }

        public static TuidanInfo GetModel(int id)
        {
            TuidanInfo result = null;
            var db = new WXDBContext();
            result = db.TuidanInfo.Find(id);
            return result;
        }

        public static IQueryable<TuidanInfo> GetList(
          Expression<Func<TuidanInfo, bool>> predicate)
        {
            IQueryable<TuidanInfo> result = null;
            var db = new WXDBContext();
            result = db.TuidanInfo.Where(predicate);

            return result;
        }
        #endregion
    }
}
