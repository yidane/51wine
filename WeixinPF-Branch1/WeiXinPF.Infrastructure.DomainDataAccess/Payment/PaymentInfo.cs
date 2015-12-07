using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Payment
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("wx_Payment_PaymentInfo")]
    public class PaymentInfo
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public int Wid { get; set; }

        [Required, MaxLength(50)]
        public string Pid { get; set; }

        [Required, MaxLength(100)]
        public string ShopName { get; set; }

        [Required, MaxLength(50)]
        public string OrderId { get; set; }

        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        [Required]
        public decimal PayAmount { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required, MaxLength(50)]
        public string ModuleName { get; set; }

        [Required, MaxLength(50)]
        public string WXOrderCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public DateTime ModifyTime { get; set; }

        [Required]
        public int Status { get; set; }

        public void Add()
        {
            using (var db = new WXDBContext())
            {
                db.PaymentInfo.Add(this);
                db.SaveChanges();
            }
        }

        public void Modify()
        {
            using (var db = new WXDBContext())
            {
                db.PaymentInfo.Attach(this);
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
        public IList<PaymentInfo> Get(Func<PaymentInfo, bool> condition)
        {
            using (var db = new WXDBContext())
            {
                return db.PaymentInfo.Where(condition).ToList();
            }
        }
    }
}
