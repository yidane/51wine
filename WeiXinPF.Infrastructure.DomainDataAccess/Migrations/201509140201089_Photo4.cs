namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo4 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.wx_travel_photoActionInfo", "createTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.wx_travel_photoActionInfo", "createTime", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.wx_travel_photoActionInfo", "createTime", c => c.DateTime(nullable: false));
        }
    }
}
