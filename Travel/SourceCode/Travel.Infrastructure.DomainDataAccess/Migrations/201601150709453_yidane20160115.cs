namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yidane20160115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderEntity", "PreUseTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_OrderEntity", "PreUseTime");
        }
    }
}
