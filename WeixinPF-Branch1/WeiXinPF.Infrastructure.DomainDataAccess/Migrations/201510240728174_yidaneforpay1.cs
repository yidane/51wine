namespace WeiXinPF.Infrastructure.DomainDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yidaneforpay1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.wx_Payment_PayNotifyInfo",
                c => new
                    {
                        NotifyID = c.Guid(nullable: false),
                        ModuleName = c.String(nullable: false, maxLength: 20),
                        out_trade_no = c.String(maxLength: 50),
                        appid = c.String(maxLength: 50),
                        attach = c.String(maxLength: 500),
                        bank_type = c.String(nullable: false),
                        cash_fee = c.Int(nullable: false),
                        cash_fee_type = c.String(maxLength: 50),
                        coupon_count = c.Int(nullable: false),
                        coupon_fee = c.Int(nullable: false),
                        device_info = c.String(maxLength: 50),
                        err_code = c.String(maxLength: 50),
                        err_code_des = c.String(maxLength: 50),
                        fee_type = c.String(maxLength: 50),
                        is_subscribe = c.String(maxLength: 50),
                        mch_id = c.String(maxLength: 50),
                        nonce_str = c.String(maxLength: 50),
                        openid = c.String(maxLength: 50),
                        result_code = c.String(maxLength: 50),
                        return_code = c.String(maxLength: 50),
                        return_msg = c.String(maxLength: 50),
                        sign = c.String(maxLength: 50),
                        time_end = c.String(maxLength: 50),
                        total_fee = c.Int(nullable: false),
                        trade_type = c.String(maxLength: 50),
                        transaction_id = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.NotifyID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.wx_Payment_PayNotifyInfo");
        }
    }
}
