namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Travel.Infrastructure.DomainDataAccess.TravelDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Travel.Infrastructure.DomainDataAccess.TravelDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
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

            //context.CouponType.AddOrUpdate(p => p.CouponTypeId, youhuiquan, xianjinquan
            //);
            //context.Coupon.AddOrUpdate(p => p.CouponId, new Coupon.Entitys.Coupon()
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
            //    EndTime = DateTime.Parse("2015-08-24"),
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
            //     EndTime = DateTime.Parse("2015-08-24"),
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
            //);
        }
    }
}
