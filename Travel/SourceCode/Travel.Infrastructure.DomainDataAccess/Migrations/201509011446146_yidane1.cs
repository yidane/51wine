namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yidane1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_UserContracts",
                c => new
                    {
                        OpenID = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        IdCard = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OpenID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_UserContracts");
        }
    }
}
