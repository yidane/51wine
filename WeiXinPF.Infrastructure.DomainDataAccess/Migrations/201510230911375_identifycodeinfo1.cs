namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identifycodeinfo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.wx_Verification_IdentifyingCodeInfo",
                c => new
                    {
                        IdentifyingCodeId = c.Guid(nullable: false),
                        ModuleName = c.String(nullable: false, maxLength: 50),
                        OrderId = c.String(nullable: false, maxLength: 50),
                        OrderCode = c.String(nullable: false, maxLength: 50),
                        ProductId = c.String(nullable: false, maxLength: 100),
                        ProductCode = c.String(nullable: false, maxLength: 100),
                        IdentifyingCode = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdentifyingCodeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.wx_Verification_IdentifyingCodeInfo");
        }
    }
}
