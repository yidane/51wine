namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tuidan1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.wx_hotel_tuidan_manage", "refundCode", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.wx_hotel_tuidan_manage", "refundCode");
        }
    }
}
