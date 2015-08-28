namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateticketentityupdate2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_DateTicketEntity");
            AlterColumn("dbo.tb_DateTicketEntity", "DateTicketId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tb_DateTicketEntity", "DateTicketId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tb_DateTicketEntity");
            AlterColumn("dbo.tb_DateTicketEntity", "DateTicketId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tb_DateTicketEntity", "DateTicketId");
        }
    }
}
