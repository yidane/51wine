namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_RefundOrderEntity",
                c => new
                    {
                        RefundOrderId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        RefundOrderCode = c.String(nullable: false),
                        WXRefundOrderCode = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        RefundStatus = c.String(nullable: false),
                        LatestModifyTime = c.DateTime(nullable: false),
                        OperatorName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RefundOrderId);
            
            CreateTable(
                "dbo.tb_RefundOrderQueueEntity",
                c => new
                    {
                        QueueId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        ECode = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        RefundQueueStatus = c.String(),
                    })
                .PrimaryKey(t => t.QueueId);
            
            AddColumn("dbo.tb_TicketEntity", "RefundOrderId", c => c.Guid(nullable: false));
            AddColumn("dbo.tb_TicketEntity", "RefundOrderDetailId", c => c.Guid(nullable: false));
            AddColumn("dbo.tb_TicketEntity", "OrderDetailId", c => c.Guid(nullable: false));
            AddColumn("dbo.tb_TicketEntity", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_TicketEntity", "Price");
            DropColumn("dbo.tb_TicketEntity", "OrderDetailId");
            DropColumn("dbo.tb_TicketEntity", "RefundOrderDetailId");
            DropColumn("dbo.tb_TicketEntity", "RefundOrderId");
            DropTable("dbo.tb_RefundOrderQueueEntity");
            DropTable("dbo.tb_RefundOrderEntity");
        }
    }
}
