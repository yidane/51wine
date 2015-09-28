namespace Travel.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exceptionlogentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_ExceptionLogEntity",
                c => new
                    {
                        ExceptionLogId = c.Guid(nullable: false),
                        Module = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(nullable: false),
                        ExceptionType = c.String(nullable: false, maxLength: 214),
                        ExceptionMessage = c.String(nullable: false),
                        TrackMessage = c.String(nullable: false),
                        HasExceptionProcessing = c.Boolean(nullable: false),
                        NeedProcess = c.Boolean(nullable: false),
                        ProcessFunction = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.ExceptionLogId);
            
            CreateTable(
                "dbo.tb_InterfaceOperationLogEntity",
                c => new
                    {
                        InterfaceOperationLogId = c.Guid(nullable: false),
                        Module = c.String(nullable: false, maxLength: 128),
                        OrderCode = c.String(nullable: false, maxLength: 50),
                        OperateObjectId = c.String(maxLength: 512),
                        OperateName = c.String(nullable: false, maxLength: 100),
                        IsOperateSuccess = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ErrorCode = c.String(maxLength: 100),
                        ErrorDescription = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.InterfaceOperationLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_InterfaceOperationLogEntity");
            DropTable("dbo.tb_ExceptionLogEntity");
        }
    }
}
