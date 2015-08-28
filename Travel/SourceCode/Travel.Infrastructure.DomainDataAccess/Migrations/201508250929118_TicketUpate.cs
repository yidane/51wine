namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketUpate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Coupon",
                c => new
                    {
                        CouponId = c.Guid(nullable: false),
                        Title = c.String(),
                        SubTitle = c.String(),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CouponTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CouponId)
                .ForeignKey("dbo.tb_CouponType", t => t.CouponTypeId, cascadeDelete: true)
                .Index(t => t.CouponTypeId);
            
            CreateTable(
                "dbo.tb_CouponType",
                c => new
                    {
                        CouponTypeId = c.Guid(nullable: false),
                        CouponTypeName = c.String(),
                    })
                .PrimaryKey(t => t.CouponTypeId);
            
            CreateTable(
                "dbo.tb_CouponUsage",
                c => new
                    {
                        CouponUsageId = c.Guid(nullable: false),
                        CouponId = c.Guid(nullable: false),
                        OpenId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CouponUsageId)
                .ForeignKey("dbo.tb_Coupon", t => t.CouponId, cascadeDelete: true)
                .ForeignKey("dbo.tb_UserInfo", t => t.OpenId)
                .Index(t => t.CouponId)
                .Index(t => t.OpenId);
            
            CreateTable(
                "dbo.tb_UserInfo",
                c => new
                    {
                        OpenId = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                        Country = c.String(),
                        FakeId = c.String(),
                        GroupID = c.Int(nullable: false),
                        NickName = c.String(),
                        Province = c.String(),
                        ReMarkName = c.String(),
                        Sex = c.String(),
                        Signature = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.OpenId);
            
            CreateTable(
                "dbo.tb_UserGroup",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        UserInfo_OpenId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.tb_UserInfo", t => t.UserInfo_OpenId)
                .Index(t => t.UserInfo_OpenId);
            
            CreateTable(
                "dbo.tb_DateTicketEntity",
                c => new
                    {
                        DateTicketId = c.Int(nullable: false, identity: true),
                        TicketCode = c.String(nullable: false, maxLength: 50),
                        TicketPackageId = c.Int(nullable: false),
                        TicketName = c.String(nullable: false, maxLength: 50),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketType = c.String(nullable: false, maxLength: 20),
                        SearchDateTime = c.DateTime(nullable: false),
                        CurrentStatus = c.String(nullable: false, maxLength: 20),
                        LatestStatusModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DateTicketId);
            
            CreateTable(
                "dbo.tb_OrderEntity",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        OrderCode = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        OpenId = c.String(),
                        ContactPersonName = c.String(),
                        MobilePhoneNumber = c.String(),
                        IdentityCardNumber = c.String(),
                        OrderStatus = c.String(),
                        HasCoupon = c.Boolean(nullable: false),
                        CouponId = c.Guid(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.tb_OrderDetailEntity",
                c => new
                    {
                        OrderDetailId = c.Guid(nullable: false),
                        OrderDetailCategoryId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        TicketCategoryId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        SingleTicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDiscount = c.Boolean(nullable: false),
                        DiscountCategoryId = c.Guid(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.tb_OrderEntity", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.tb_TicketEntity",
                c => new
                    {
                        TicketId = c.Int(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        TicketCategoryId = c.Guid(nullable: false),
                        TicketCode = c.String(nullable: false, maxLength: 50),
                        TicketStatus = c.String(nullable: false, maxLength: 30),
                        ECode = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        LatestModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.tb_OrderEntity", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.tb_TicketCategoryEntity",
                c => new
                    {
                        TicketCategoryId = c.Guid(nullable: false),
                        ImplementationDate = c.DateTime(nullable: false),
                        TicketPackageId = c.Int(nullable: false),
                        TicketType = c.String(nullable: false, maxLength: 20),
                        Type = c.String(nullable: false, maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TicketName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TicketCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_TicketEntity", "OrderId", "dbo.tb_OrderEntity");
            DropForeignKey("dbo.tb_OrderDetailEntity", "OrderId", "dbo.tb_OrderEntity");
            DropForeignKey("dbo.tb_CouponUsage", "OpenId", "dbo.tb_UserInfo");
            DropForeignKey("dbo.tb_UserGroup", "UserInfo_OpenId", "dbo.tb_UserInfo");
            DropForeignKey("dbo.tb_CouponUsage", "CouponId", "dbo.tb_Coupon");
            DropForeignKey("dbo.tb_Coupon", "CouponTypeId", "dbo.tb_CouponType");
            DropIndex("dbo.tb_TicketEntity", new[] { "OrderId" });
            DropIndex("dbo.tb_OrderDetailEntity", new[] { "OrderId" });
            DropIndex("dbo.tb_UserGroup", new[] { "UserInfo_OpenId" });
            DropIndex("dbo.tb_CouponUsage", new[] { "OpenId" });
            DropIndex("dbo.tb_CouponUsage", new[] { "CouponId" });
            DropIndex("dbo.tb_Coupon", new[] { "CouponTypeId" });
            DropTable("dbo.tb_TicketCategoryEntity");
            DropTable("dbo.tb_TicketEntity");
            DropTable("dbo.tb_OrderDetailEntity");
            DropTable("dbo.tb_OrderEntity");
            DropTable("dbo.tb_DateTicketEntity");
            DropTable("dbo.tb_UserGroup");
            DropTable("dbo.tb_UserInfo");
            DropTable("dbo.tb_CouponUsage");
            DropTable("dbo.tb_CouponType");
            DropTable("dbo.tb_Coupon");
        }
    }
}
