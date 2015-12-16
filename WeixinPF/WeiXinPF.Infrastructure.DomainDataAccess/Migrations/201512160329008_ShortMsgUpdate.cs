namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortMsgUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dt_common_ShortMsg", "DetailType", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.dt_common_ShortMsg", "DetailType");
        }
    }
}
