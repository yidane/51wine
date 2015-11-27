namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortMsg2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dt_common_ShortMsg", "MenuType", c => c.String(maxLength: 128));
            AlterColumn("dbo.dt_common_ShortMsg", "Type", c => c.String(maxLength: 128));
            AlterColumn("dbo.dt_common_ShortMsg", "FromUserId", c => c.String(nullable: false));
            AlterColumn("dbo.dt_common_ShortMsg", "ToUserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.dt_common_ShortMsg", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.dt_common_ShortMsg", "FromUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.dt_common_ShortMsg", "Type", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.dt_common_ShortMsg", "MenuType");
        }
    }
}
