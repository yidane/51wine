namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoMigration1016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.wx_Payment_RefundmentInfo",
                c => new
                    {
                        RefundID = c.Guid(nullable: false),
                        Wid = c.Int(nullable: false),
                        Pid = c.String(nullable: false, maxLength: 50),
                        OrderId = c.String(nullable: false, maxLength: 50),
                        OrderCode = c.String(nullable: false, maxLength: 50),
                        RefundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModuleName = c.String(nullable: false, maxLength: 50),
                        WXOrderCode = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RefundID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.wx_Payment_RefundmentInfo");
        }
    }
}
