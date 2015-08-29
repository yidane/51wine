namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coupon4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_CouponUsage", "State", c => c.Int(nullable: false));
            AddColumn("dbo.tb_CouponUsage", "UsedTime", c => c.DateTime());
            DropColumn("dbo.tb_Coupon", "State");
            DropColumn("dbo.tb_Coupon", "UsedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Coupon", "UsedTime", c => c.DateTime());
            AddColumn("dbo.tb_Coupon", "State", c => c.Int(nullable: false));
            DropColumn("dbo.tb_CouponUsage", "UsedTime");
            DropColumn("dbo.tb_CouponUsage", "State");
        }
    }
}
