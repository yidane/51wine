namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ShortMsg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dt_common_ShortMsg",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 128),
                    Content = c.String(nullable: false, maxLength: 2000),
                    Type = c.String(nullable: false, maxLength: 128),
                    IsShowButton = c.Boolean(nullable: false),
                    ButtonText = c.String(),
                    ButtonUrl = c.String(),
                    ButtonMutipleUrl = c.String(),
                    CreateTime = c.DateTime(nullable: false),
                    IsRead = c.Boolean(nullable: false),
                    FromUserId = c.Int(nullable: false),
                    MsgToUserType = c.Int(nullable: false), 
                    ToUserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.dt_common_ShortMsg");
        }
    }
}
