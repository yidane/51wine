namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortMsg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dt_common_ShortMsg", "MsgFromUserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.dt_common_ShortMsg", "MsgFromUserType");
        }
    }
}
