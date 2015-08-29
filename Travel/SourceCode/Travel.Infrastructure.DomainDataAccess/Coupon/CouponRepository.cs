using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.DomainDataAccess.Coupon.Entitys;
using Travel.Infrastructure.DomainDataAccess.User;

namespace Travel.Infrastructure.DomainDataAccess.Coupon
{
    public class CouponRepository
    {
        //private static List<CouponType> _testCouponType = new List<CouponType>();
        //private static List<Entitys.Coupon> _testCoupon = new List<Entitys.Coupon>();
        public CouponRepository() {

            //var youhuiquan = new Coupon.Entitys.CouponType()
            //{
            //    CouponTypeId = Guid.NewGuid(),
            //    CouponTypeName = "优惠券"
            //};
            //var xianjinquan = new Coupon.Entitys.CouponType()
            //{
            //    CouponTypeId = Guid.NewGuid(),
            //    CouponTypeName = "现金券"
            //};

            //_testCouponType.Add(youhuiquan);
            //_testCouponType.Add(xianjinquan);
            //_testCoupon.AddRange(new Entitys.Coupon[] {
            //    new Coupon.Entitys.Coupon()
            //{
            //    CouponId = Guid.NewGuid(),
            //    Title = "10元优惠券",
            //    SubTitle = "10元",
            //    BeginTime = DateTime.Parse("2015-08-20"),
            //    EndTime = DateTime.Parse("2015-08-30"),
            //    Type = youhuiquan
            //},
            //new Coupon.Entitys.Coupon()
            //{
            //    CouponId = Guid.NewGuid(),
            //    Title = "20元优惠券",
            //    SubTitle = "20元",
            //    BeginTime = DateTime.Parse("2015-08-20"),
            //    EndTime = DateTime.Parse("2015-08-26"),
            //    Type = youhuiquan
            //},
            //new Coupon.Entitys.Coupon()
            //{
            //    CouponId = Guid.NewGuid(),
            //    Title = "30元现金券",
            //    SubTitle = "30元",
            //    BeginTime = DateTime.Parse("2015-08-20"),
            //    EndTime = DateTime.Parse("2015-08-30"),
            //    Type = xianjinquan
            //},
            // new Coupon.Entitys.Coupon()
            // {
            //     CouponId = Guid.NewGuid(),
            //     Title = "15元现金券",
            //     SubTitle = "15元",
            //     BeginTime = DateTime.Parse("2015-08-20"),
            //     EndTime = DateTime.Parse("2015-08-26"),
            //     Type = xianjinquan
            // },
            // new Coupon.Entitys.Coupon()
            // {
            //     CouponId = Guid.NewGuid(),
            //     Title = "15元现金券",
            //     SubTitle = "15元",
            //     BeginTime = DateTime.Parse("2015-08-20"),
            //     EndTime = DateTime.Parse("2015-08-23"),
            //     Type = xianjinquan
            // }
            //});

           
        }

        public Entitys.Coupon GetCouponByUser(Guid couponId, string openid)
        {
            Entitys.Coupon result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{

           
           
            var coupon = db.Coupon.Where(item => item.CouponId == couponId)
                .Join(db.CouponUsage.Where(item => item.OpenId == openid)
                , c => c.CouponId, u => u.CouponId, (c, u) => c).FirstOrDefault();
            result = coupon;

            //}


            return result;
        }

        public List<Entitys.Coupon> GetUnExpiredCoupons(UserInfo user)
        {
            List<Entitys.Coupon> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
                var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
                var nowTime = DateTime.Now;
                var coupons = db.Coupon.Where(item => item.State== CouponState.NotUsed &&
                item.EndTime >= nowTime
                &&item.Type.CouponTypeId== typeId)
                .Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
                , c => c.CouponId, u => u.CouponId, (c, u) => c);
                result = coupons.ToList();

            //}
         

            return result;
        }

        public List<Entitys.Coupon> GetExpiredCoupons(UserInfo user)
        {
            List<Entitys.Coupon> result =null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
                var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
                var nowTime = DateTime.Now;
                var coupons = db.Coupon.Where(item => item.State == CouponState.NotUsed &&
                item.EndTime < nowTime
                && item.Type.CouponTypeId == typeId).Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
                , c => c.CouponId, u => u.CouponId, (c, u) => c);
            result = coupons.ToList();

            //}
             
            return result;
        }

        public List<Entitys.Coupon> GetUsedCoupons(UserInfo user)
        {
            List<Entitys.Coupon> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
            var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
            var nowTime = DateTime.Now;
            result = db.Coupon.Where(item => item.State == CouponState.Used && 
            item.Type.CouponTypeId == typeId).Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
                , c => c.CouponId, u => u.CouponId, (c, u) => c).ToList();
            

            //}

            return result;
        }

        /// <summary>
        /// 判断是否还有优惠券
        /// </summary>
        /// <returns></returns>
        public bool HasCoupon()
        {
            var result = false;
            using (var db = new TravelDBContext())
            {
                var nowTime = DateTime.Now;
                var coupons = db.Coupon.Where(item => item.BeginTime <= nowTime && item.EndTime >= nowTime);
                result = coupons.Any();

            }
            //var nowTime = DateTime.Now;
            //var coupons = _testCoupon.Where(item => item.BeginTime <= nowTime && item.EndTime >= nowTime);
            //result = coupons.Any();
            return result;
        }
        /// <summary>
        /// 获取现有可用的优惠券
        /// </summary>
        /// <returns></returns>
        public List<Entitys.Coupon> GetAvailableCoupon()
        {
            List<Entitys.Coupon> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
                var nowTime = DateTime.Now;
                result = db.Coupon.Where(item => item.BeginTime <= nowTime && item.EndTime >= nowTime).ToList();

            //}
            //var nowTime = DateTime.Now;
            //result = _testCoupon.Where(item => item.BeginTime <= nowTime && item.EndTime >= nowTime).ToList();

            return result;
        }

        /// <summary>
        /// 绑定优惠券到用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="coupon"></param>
        public void AddCouponToUser(string openid, Entitys.Coupon coupon)
        {

            using (var db = new TravelDBContext())
            {
                var usage = new CouponUsage() {
                    CouponUsageId =Guid.NewGuid(),
                    CouponId = coupon.CouponId,
                    OpenId = openid,
                    ReceivedTime=DateTime.Now
                };
                db.CouponUsage.Add(usage);
                db.SaveChanges();
            }


        }

        public void UseCoupon(string openId, Guid id)
        {
            using (var db = new TravelDBContext())
            {
                var usages = db.CouponUsage.Where(p => p.CouponId == id && p.OpenId == openId);
                
                if (usages != null&& usages.Any())
                {
                    var coupon = db.Coupon.Find(id);
                    coupon.State = CouponState.Used;
                    coupon.UsedTime = DateTime.Now;
                    db.Entry(coupon).State = System.Data.Entity.EntityState.Modified;
                   // db.Coupon.Attach(coupon);
                    db.SaveChanges();
                }
            }
        }

        public Entitys.Coupon GetRandomCoupon(List<Entitys.Coupon> availableCoupons)
        {
            Entitys.Coupon result = null;
            //取随机数
            Random r = new Random();
            int i = r.Next(0, availableCoupons.Count );


            //using (var db = new TravelDBContext())
            //{
            //    //得到随机的优惠券
            //    result = availableCoupons[i];
            //    result.Type = db.CouponType.Find(result.CouponTypeId);
            //}
            result = availableCoupons[i];


            return result;
        }

        public Entitys.Coupon GetCoupon( Guid couponId)
        {
            Entitys.Coupon result = null;
            using (var db = new TravelDBContext())
            {
                result = db.Coupon.Find(couponId);
            }
           // result = _testCoupon.Where(item=>item.CouponId==couponId).FirstOrDefault();
            return result;
        }
    }
}
