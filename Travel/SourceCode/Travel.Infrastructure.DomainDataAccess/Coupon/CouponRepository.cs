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
        public CouponRepository()
        {

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

        public Entitys.CouponUsage GetCouponByUser(Guid couponUsageId, string openid)
        {
            Entitys.CouponUsage result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{



            var coupon = db.CouponUsage
              .FirstOrDefault(item => item.CouponUsageId == couponUsageId && item.OpenId == openid);
            result = coupon;

            //}


            return result;
        }

        public List<Entitys.CouponUsage> GetUnExpiredCoupons(UserInfo user)
        {
            List<Entitys.CouponUsage> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
            //   var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
            var nowTime = DateTime.Now;
            var coupons = db.CouponUsage.Where(item => item.State == CouponState.NotUsed
             && item.OpenId == user.openid &&
           item.Coupon.EndTime >= nowTime
          );
            result = coupons.ToList();
            //var coupons = db.Coupon.Where(item => item.State== CouponState.NotUsed &&
            //item.EndTime >= nowTime
            //&&item.Type.CouponTypeId== typeId)
            //.Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
            //, c => c.CouponId, u => u.CouponId, (c, u) => c);
            //result = coupons.ToList();

            //}


            return result;
        }

        public List<Entitys.CouponUsage> GetExpiredCoupons(UserInfo user)
        {
            List<Entitys.CouponUsage> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
            //   var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
            var nowTime = DateTime.Now;
            var coupons = db.CouponUsage.Where(item => item.State == CouponState.NotUsed
             && item.OpenId == user.openid
            && item.Coupon.EndTime < nowTime
            );
            //var coupons = db.Coupon.Where(item => item.State == CouponState.NotUsed &&
            //    item.EndTime < nowTime
            //    && item.Type.CouponTypeId == typeId).Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
            //    , c => c.CouponId, u => u.CouponId, (c, u) => c);
            result = coupons.ToList();

            //}

            return result;
        }

        public List<Entitys.CouponUsage> GetUsedCoupons(UserInfo user)
        {
            List<Entitys.CouponUsage> result = null;
            var db = new TravelDBContext();
            //using (var db = new TravelDBContext())
            //{
            // var typeId = Guid.Parse("63313E55-A213-4B38-AF64-E6F2ABF68E56");
            var nowTime = DateTime.Now;
            var coupons = db.CouponUsage.Where(item => item.State == CouponState.Used
            && item.OpenId == user.openid
         );
            //result = db.Coupon.Where(item => item.State == CouponState.Used && 
            //item.Type.CouponTypeId == typeId).Join(db.CouponUsage.Where(item => item.OpenId == user.openid)
            //    , c => c.CouponId, u => u.CouponId, (c, u) => c).ToList();

            result = coupons.ToList();
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
                var coupons =GetAvailableCoupon();
                result = coupons.Any();

            }
            //var nowTime = DateTime.Now;
            //var coupons = _testCoupon.Where(item => item.BeginTime <= nowTime && item.EndTime >= nowTime);
            //result = coupons.Any();
            return result;
        }

        /// <summary>
        ///  判断是否参与过摇奖
        /// </summary>
        /// <returns></returns>
        public bool BeParticipatedInCoupon(string openId)
        {
            var result = false;
            using (var db = new TravelDBContext())
            {
                var nowTime = DateTime.Now;
                //获取现有优惠券

                var coupon = db.Coupon.FirstOrDefault(item =>
                item.BeginTime <= nowTime &&
                item.EndTime >= nowTime);

                if (coupon!=null)
                {
                    var usage = db.CouponUsage.FirstOrDefault(item =>
                 item.ReceivedTime >= coupon.BeginTime &&
                 item.ReceivedTime <= coupon.EndTime && item.OpenId
                 == openId);
                    if (usage!=null)
                    {
                        result = true;
                    }
                }

                
                 

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
            //获取现有优惠券
           
            var coupons = db.Coupon.Where(item =>
            item.BeginTime <= nowTime &&
            item.EndTime >= nowTime);

            //获取优惠券使用数量
            //todo:方法有问题，返回的是空值

            //var dt =  from couponUsage in db.CouponUsage
            //    join coupon in coupons on couponUsage.CouponId equals coupon.CouponId
            //        into cu
            //    from u in cu.DefaultIfEmpty()
            //    select new
            //    {
            //        CouponId = u.CouponId,
            //        OpenId = couponUsage.OpenId
            //    };
           


            var usageCount = db.CouponUsage
            .Join(coupons, u => u.CouponId,
            c => c.CouponId, (u, c) => u).GroupBy(u => u.CouponId)
            .Select(u => new
            {
                CouponId = u.Key,
                Count = u.Count()==null?0:u.Count()
            });

            var da = from c in coupons
                join u in usageCount on c.CouponId equals u.CouponId
                    into cu
                from cu1 in cu.DefaultIfEmpty()
                 //    where c.StockQuantity > (cu1.Count == null ? 0 : cu1.Count)
                select c;

            result = da.ToList();

//            result = coupons.Join(usageCount, c => c.CouponId,
//                u => u.CouponId, (c, u) => new { c = c, u = u })
//                .Where(item => item.c.StockQuantity > item.u.Count)
//                .Select(item => item.c).ToList();
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
                var usage = new CouponUsage()
                {
                    CouponUsageId = Guid.NewGuid(),
                    CouponId = coupon.CouponId,
                    OpenId = openid,
                    ReceivedTime = DateTime.Now
                };
                db.CouponUsage.Add(usage);
                db.SaveChanges();
            }


        }

        public void UseCoupon(string openId, Guid id)
        {
            using (var db = new TravelDBContext())
            {
                var usage = db.CouponUsage.FirstOrDefault(p => p.CouponUsageId == id && p.OpenId == openId);

                if (usage != null)
                {

                    usage.State = CouponState.Used;
                    usage.UsedTime = DateTime.Now;
                    db.Entry(usage).State = System.Data.Entity.EntityState.Modified;
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
            int i = r.Next(0, availableCoupons.Count);


            //using (var db = new TravelDBContext())
            //{
            //    //得到随机的优惠券
            //    result = availableCoupons[i];
            //    result.Type = db.CouponType.Find(result.CouponTypeId);
            //}
            result = availableCoupons[i];


            return result;
        }

        public Entitys.Coupon GetCoupon(Guid couponId)
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
