namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identifycodeinfo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.wx_Verification_IdentifyingCodeInfo", "Wid", c => c.Int(nullable: false));
            AddColumn("dbo.wx_Verification_IdentifyingCodeInfo", "ShopId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.wx_Verification_IdentifyingCodeInfo", "ShopId");
            DropColumn("dbo.wx_Verification_IdentifyingCodeInfo", "Wid");
        }
    }
}
