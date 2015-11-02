using System;
using WeiXinPF.Common;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class getopenId : WeiXinPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var thisUrl = MyCommFun.getWebSite() + "/weixin/WeChatPay/getopenId.aspx" + Request.Url.Query;
            var widInt = MyCommFun.RequestWid();
            var bll = new BLL.wx_userweixin();
            var uWeiXinModel = bll.GetModel(widInt);
            if (uWeiXinModel == null)
                throw new Exception("不合法的参数wid");
            OAuth2BaseProc(uWeiXinModel, "MyOrderCenter", thisUrl);
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            var entity = new UnifiedOrderEntity
            {
                OrderId = "1234455678",
                wid = 1,
                total_fee = 1,
                out_trade_no = "DD123456789",
                openid = openid,
                body = "测试不关注支付",
                PayModuleID = (int)PayModuleEnum.Restaurant,
                PayComplete = "../restaurant/index.aspx"
            };

            entity.Extra.Add("content", "");
            //entity.Extra.Add("shopname", new BLL.wx_diancai_shopinfo().GetModel(this.shopid).hotelName);
            //entity.Extra.Add("shopid", shopid.ToString());
            entity.Extra.Add("wid", "1");

            var ticket = EncryptionManager.CreateIV();
            var payData = EncryptionManager.AESEncrypt(entity.ToJson(), ticket);

            Response.Redirect(PayHelper.GetPayUrl(payData, ticket));

            //Response.Redirect("http://weixin.zhiyoubao.com/ticket/scenicTicket.htm?scenicId=1021");
        }
    }
}