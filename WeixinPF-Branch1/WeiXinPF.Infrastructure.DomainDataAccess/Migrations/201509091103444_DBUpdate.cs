namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_UserInfo",
                c => new
                    {
                        openid = c.String(nullable: false, maxLength: 128),
                        nickname = c.String(),
                        sex = c.Int(nullable: false),
                        language = c.String(),
                        city = c.String(),
                        province = c.String(),
                        country = c.String(),
                        headimgurl = c.String(),
                        subscribe_time = c.Long(nullable: false),
                        remark = c.String(),
                        groupid = c.Int(nullable: false),
                        subscribe = c.String(),
                    })
                .PrimaryKey(t => t.openid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_UserInfo");
        }
    }
}
