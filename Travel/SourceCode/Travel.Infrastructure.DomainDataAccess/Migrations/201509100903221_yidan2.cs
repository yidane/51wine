namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yidan2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_DailyProductEntity",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SearchDate = c.DateTime(nullable: false),
                        ProductSource = c.String(nullable: false, maxLength: 128),
                        ProductPackageId = c.Int(nullable: false),
                        ProductCode = c.String(),
                        ProductName = c.String(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductType = c.String(),
                        ProductCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SearchDate, t.ProductSource, t.ProductPackageId });
            
            CreateTable(
                "dbo.tb_ProductCategoryEntity",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ProductPackageId = c.Int(nullable: false),
                        ProductSource = c.String(nullable: false, maxLength: 128),
                        ProductCategoryId = c.Guid(nullable: false),
                        ProductName = c.String(),
                        OldProductName = c.String(),
                        ProductType = c.String(),
                        ProductCode = c.String(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductDescription = c.String(),
                        IsCombinedProduct = c.Boolean(nullable: false),
                        IsSelfDefinedProduct = c.Boolean(nullable: false),
                        CurrentStatus = c.String(),
                        FirstSort = c.String(),
                        SecondSort = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ProductPackageId, t.ProductSource });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_ProductCategoryEntity");
            DropTable("dbo.tb_DailyProductEntity");
        }
    }
}
