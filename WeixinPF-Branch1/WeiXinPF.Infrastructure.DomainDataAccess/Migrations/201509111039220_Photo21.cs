namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.wx_travel_photoActionInfo", "aid", "dbo.wx_travel_photoScenesInfo");
            DropIndex("dbo.wx_travel_photoActionInfo", new[] { "aid" });
            CreateIndex("dbo.wx_travel_photoScenesInfo", "aid");
            AddForeignKey("dbo.wx_travel_photoScenesInfo", "aid", "dbo.wx_travel_photoActionInfo", "id", cascadeDelete: true);
            DropColumn("dbo.wx_travel_photoActionInfo", "aid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.wx_travel_photoActionInfo", "aid", c => c.Int(nullable: false));
            DropForeignKey("dbo.wx_travel_photoScenesInfo", "aid", "dbo.wx_travel_photoActionInfo");
            DropIndex("dbo.wx_travel_photoScenesInfo", new[] { "aid" });
            CreateIndex("dbo.wx_travel_photoActionInfo", "aid");
            AddForeignKey("dbo.wx_travel_photoActionInfo", "aid", "dbo.wx_travel_photoScenesInfo", "id");
        }
    }
}
