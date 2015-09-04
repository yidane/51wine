namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCoupon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Coupon", "StockQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Coupon", "Extend1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Coupon", "Extend1", c => c.String());
            DropColumn("dbo.tb_Coupon", "StockQuantity");
        }
    }
}
