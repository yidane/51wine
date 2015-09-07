namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_TicketEntity", "TicketProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_TicketEntity", "TicketProductId");
        }
    }
}
