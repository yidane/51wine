namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coupon3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Coupon", "Extend1", c => c.String());
            AddColumn("dbo.tb_Coupon", "Extend2", c => c.String());
            AddColumn("dbo.tb_Coupon", "Extend3", c => c.String());
            AddColumn("dbo.tb_Coupon", "Extend4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Coupon", "Extend4");
            DropColumn("dbo.tb_Coupon", "Extend3");
            DropColumn("dbo.tb_Coupon", "Extend2");
            DropColumn("dbo.tb_Coupon", "Extend1");
        }
    }
}
