namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tuidan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.wx_hotel_tuidan_manage",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CreateTime = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        dingdanid = c.Int(nullable: false),
                        hotelid = c.Int(nullable: false),
                        openid = c.String(),
                        wid = c.Int(nullable: false),
                        roomid = c.Int(nullable: false),
                        refundTime = c.DateTime(nullable: false),
                        refundAmount = c.Double(nullable: false),
                        operateUser = c.Int(nullable: false),
                        remarks = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.wx_hotel_tuidan_manage");
        }
    }
}