namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tb_UserInfoEntity", newName: "tb_UserInfo");
            DropForeignKey("dbo.wx_travel_photoScenesInfo", "aid", "dbo.wx_travel_photoActionInfo");
            DropIndex("dbo.wx_travel_photoScenesInfo", new[] { "aid" });
            CreateTable(
                "dbo.wx_travel_photoUserParticipated",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        aid = c.Int(nullable: false),
                        openid = c.String(),
                        participatedDate = c.String(),
                        participatedImg = c.String(),
                        isShareTimeline = c.Boolean(nullable: false),
                        isShareAppMessage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.wx_travel_photoActionInfo", t => t.aid, cascadeDelete: true)
                .Index(t => t.aid);
            
            CreateIndex("dbo.wx_travel_photoActionInfo", "id");
            AddForeignKey("dbo.wx_travel_photoActionInfo", "id", "dbo.wx_travel_photoScenesInfo", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.wx_travel_photoUserParticipated", "aid", "dbo.wx_travel_photoActionInfo");
            DropForeignKey("dbo.wx_travel_photoActionInfo", "id", "dbo.wx_travel_photoScenesInfo");
            DropIndex("dbo.wx_travel_photoUserParticipated", new[] { "aid" });
            DropIndex("dbo.wx_travel_photoActionInfo", new[] { "id" });
            DropTable("dbo.wx_travel_photoUserParticipated");
            CreateIndex("dbo.wx_travel_photoScenesInfo", "aid");
            AddForeignKey("dbo.wx_travel_photoScenesInfo", "aid", "dbo.wx_travel_photoActionInfo", "id", cascadeDelete: true);
            RenameTable(name: "dbo.tb_UserInfo", newName: "tb_UserInfoEntity");
        }
    }
}
