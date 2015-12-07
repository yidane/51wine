using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Payment
{
    [Table("wx_Payment_RefundmentInfo")]
    public class RefundmentInfo
    {
        [Key]
        public Guid RefundID { get; set; }

        [Required]
        public int Wid { get; set; }

        [Required, MaxLength(50)]
        public string Pid { get; set; }

        [Required, MaxLength(50)]
        public string OrderId { get; set; }

        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        [Required]
        public decimal RefundAmount { get; set; }

        [Required, MaxLength(50)]
        public string ModuleName { get; set; }

        [Required, MaxLength(50)]
        public string WXOrderCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public void Add()
        {
            using (var db = new WXDBContext())
            {
                db.RefundmentInfo.Add(this);
                db.SaveChanges();
            }
        }

        public void Modify()
        {
            using (var db = new WXDBContext())
            {
                db.RefundmentInfo.Attach(this);
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  根据条件获取结果
        /// </summary>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<RefundmentInfo> Get(Func<RefundmentInfo, bool> condition)
        {
            using (var db = new WXDBContext())
            {
                return db.RefundmentInfo.Where(condition).ToList();
            }
        }
    }
}
