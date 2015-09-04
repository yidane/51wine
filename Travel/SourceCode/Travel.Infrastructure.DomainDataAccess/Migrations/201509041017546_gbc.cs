namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gbc : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_DateTicketEntity");
            AddPrimaryKey("dbo.tb_DateTicketEntity", new[] { "DateTicketId", "SearchDateTime" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_DateTicketEntity");
            AddPrimaryKey("dbo.tb_DateTicketEntity", "DateTicketId");
        }
    }
}
