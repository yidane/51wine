namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coupon1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Coupon", "State", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Coupon", "UsedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_CouponUsage", "ReceivedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_CouponUsage", "ReceivedTime");
            DropColumn("dbo.tb_Coupon", "UsedTime");
            DropColumn("dbo.tb_Coupon", "State");
        }
    }
}
