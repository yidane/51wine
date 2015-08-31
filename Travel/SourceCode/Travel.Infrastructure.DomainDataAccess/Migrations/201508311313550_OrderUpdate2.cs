namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_TicketEntity", "RefundOrderId", c => c.Guid());
            AlterColumn("dbo.tb_TicketEntity", "RefundOrderDetailId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_TicketEntity", "RefundOrderDetailId", c => c.Guid(nullable: false));
            AlterColumn("dbo.tb_TicketEntity", "RefundOrderId", c => c.Guid(nullable: false));
        }
    }
}
