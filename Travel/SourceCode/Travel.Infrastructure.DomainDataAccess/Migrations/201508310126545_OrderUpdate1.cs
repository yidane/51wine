namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderEntity", "WXOrderCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_OrderEntity", "WXOrderCode");
        }
    }
}
