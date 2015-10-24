using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Payment
{
    [Table("wx_Payment_PayNotifyInfo")]
    public class PayNotifyInfo
    {
        [Key]
        public Guid NotifyID { get; set; }
        [Required, MaxLength(20)]
        public string ModuleName { get; set; }
        [MaxLength(50)]
        public string out_trade_no { get; set; }
        [MaxLength(50)]
        public string appid { get; set; }
        [MaxLength(500)]
        public string attach { get; set; }
        [Required]
        public string bank_type { get; set; }
        public int cash_fee { get; set; }
        [MaxLength(50)]
        public string cash_fee_type { get; set; }
        public int coupon_count { get; set; }
        public int coupon_fee { get; set; }
        [MaxLength(50)]
        public string device_info { get; set; }
        [MaxLength(50)]
        public string err_code { get; set; }
        [MaxLength(50)]
        public string err_code_des { get; set; }
        [MaxLength(50)]
        public string fee_type { get; set; }
        [MaxLength(50)]
        public string is_subscribe { get; set; }
        [MaxLength(50)]
        public string mch_id { get; set; }
        [MaxLength(50)]
        public string nonce_str { get; set; }
        [MaxLength(50)]
        public string openid { get; set; }
        [MaxLength(50)]
        public string result_code { get; set; }
        [MaxLength(50)]
        public string return_code { get; set; }
        [MaxLength(50)]
        public string return_msg { get; set; }
        [MaxLength(50)]
        public string sign { get; set; }
        [MaxLength(50)]
        public string time_end { get; set; }
        [Required]
        public int total_fee { get; set; }
        [MaxLength(50)]
        public string trade_type { get; set; }
        [MaxLength(50)]
        public string transaction_id { get; set; }

        public void Add()
        {
            using (var db = new WXDBContext())
            {
                db.PayNotifyInfo.Add(this);
                db.SaveChanges();
            }
        }

        public void Modify()
        {
            using (var db = new WXDBContext())
            {
                db.PayNotifyInfo.Attach(this);
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IList<PayNotifyInfo> Get(Func<PayNotifyInfo, bool> condition)
        {
            using (var db = new WXDBContext())
            {
                return db.PayNotifyInfo.Where(condition).ToList();
            }
        }

        /// <summary>
        /// 防止存在相同参数
        /// </summary>
        /// <param name="moudleName"></param>
        /// <param name="out_trade_No"></param>
        /// <returns></returns>
        public bool ContainPayNotufy(string moudleName, string out_trade_No)
        {
            using (var db = new WXDBContext())
            {
                return
                    db.PayNotifyInfo.Any(
                        item =>
                        string.Equals(item.ModuleName, moudleName) && string.Equals(out_trade_no, item.out_trade_no));
            }
        }
    }
}
