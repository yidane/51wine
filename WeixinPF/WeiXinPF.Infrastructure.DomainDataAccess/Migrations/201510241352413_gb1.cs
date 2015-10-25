namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gb1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.wx_Verification_IdentifyingCodeInfo", "ShopId", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.wx_Verification_IdentifyingCodeInfo", "ShopId", c => c.String(nullable: false));
        }
    }
}
