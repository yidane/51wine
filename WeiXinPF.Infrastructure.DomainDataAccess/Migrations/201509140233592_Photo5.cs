namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.wx_travel_photoActionInfo", "actName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.wx_travel_photoActionInfo", "actName");
        }
    }
}
