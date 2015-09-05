namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class liantiao1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_TicketEntity");
            AddPrimaryKey("dbo.tb_TicketEntity", new[] { "TicketId", "OrderId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_TicketEntity");
            AddPrimaryKey("dbo.tb_TicketEntity", "TicketId");
        }
    }
}
