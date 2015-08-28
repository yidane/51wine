namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_UserGroup", "UserInfo_OpenId", "dbo.tb_UserInfo");
            DropIndex("dbo.tb_UserGroup", new[] { "UserInfo_OpenId" });
            AddColumn("dbo.tb_UserInfo", "subscribe", c => c.String());
            AddColumn("dbo.tb_UserInfo", "language", c => c.String());
            AddColumn("dbo.tb_UserInfo", "headimgurl", c => c.String());
            AddColumn("dbo.tb_UserInfo", "subscribe_time", c => c.Long(nullable: false));
            AddColumn("dbo.tb_UserInfo", "remark", c => c.String());
            AddColumn("dbo.tb_TicketEntity", "TicketStartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_TicketEntity", "TicketEndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_UserInfo", "sex", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_TicketEntity", "TicketStatus", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.tb_UserInfo", "FakeId");
            DropColumn("dbo.tb_UserInfo", "ReMarkName");
            DropColumn("dbo.tb_UserInfo", "Signature");
            DropColumn("dbo.tb_UserInfo", "Username");
            DropColumn("dbo.tb_UserGroup", "UserInfo_OpenId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_UserGroup", "UserInfo_OpenId", c => c.String(maxLength: 128));
            AddColumn("dbo.tb_UserInfo", "Username", c => c.String());
            AddColumn("dbo.tb_UserInfo", "Signature", c => c.String());
            AddColumn("dbo.tb_UserInfo", "ReMarkName", c => c.String());
            AddColumn("dbo.tb_UserInfo", "FakeId", c => c.String());
            AlterColumn("dbo.tb_TicketEntity", "TicketStatus", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.tb_UserInfo", "sex", c => c.String());
            DropColumn("dbo.tb_TicketEntity", "TicketEndTime");
            DropColumn("dbo.tb_TicketEntity", "TicketStartTime");
            DropColumn("dbo.tb_UserInfo", "remark");
            DropColumn("dbo.tb_UserInfo", "subscribe_time");
            DropColumn("dbo.tb_UserInfo", "headimgurl");
            DropColumn("dbo.tb_UserInfo", "language");
            DropColumn("dbo.tb_UserInfo", "subscribe");
            CreateIndex("dbo.tb_UserGroup", "UserInfo_OpenId");
            AddForeignKey("dbo.tb_UserGroup", "UserInfo_OpenId", "dbo.tb_UserInfo", "OpenId");
        }
    }
}
