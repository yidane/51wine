namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yidaneforpay2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.wx_Payment_PayNotifyInfo", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.wx_Payment_PayNotifyInfo", "CreateTime");
        }
    }
}
