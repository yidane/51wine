namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gbc1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_StatusEntity",
                c => new
                    {
                        StatusId = c.Guid(nullable: false),
                        Module = c.String(nullable: false, maxLength: 50),
                        StatusCNName = c.String(nullable: false, maxLength: 50),
                        StatusENName = c.String(nullable: false, maxLength: 50),
                        StatusCode = c.String(nullable: false, maxLength: 50),
                        StatusDescription = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        Sort = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_StatusEntity");
        }
    }
}
