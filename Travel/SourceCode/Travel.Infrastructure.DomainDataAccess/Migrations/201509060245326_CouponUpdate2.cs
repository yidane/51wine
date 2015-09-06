namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CouponUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Coupon", "ShakeToReceive", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_Coupon", "Extend2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Coupon", "Extend2", c => c.String());
            DropColumn("dbo.tb_Coupon", "ShakeToReceive");
        }
    }
}
