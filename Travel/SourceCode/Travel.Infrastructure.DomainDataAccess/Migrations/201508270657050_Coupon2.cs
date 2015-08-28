namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coupon2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Coupon", "UsedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Coupon", "UsedTime", c => c.DateTime(nullable: false));
        }
    }
}
