namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.wx_travel_photoActionInfo", "createTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.wx_travel_photoActionInfo", "createTime");
        }
    }
}
