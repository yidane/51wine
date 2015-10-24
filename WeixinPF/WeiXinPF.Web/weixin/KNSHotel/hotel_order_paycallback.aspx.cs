using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.Model;
using WeiXinPF.Web.weixin.WeChatPay;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    public partial class hotel_order_paycallback : System.Web.UI.Page
    {
        private string dingdanidnum;
        private string openid ; 
        private string hotelid  ;
        private string roomid  ;
        protected void Page_Load(object sender, EventArgs e)
        {
            var payData = Request.QueryString["payData"];
            var ticket = Request.QueryString["ticket"];
            var payStatus = Request.QueryString["payStatus"];
            if (!string.IsNullOrEmpty(payData) && !string.IsNullOrEmpty(ticket))
            {
                payData = EncryptionManager.AESDecrypt(payData, ticket);

                var payDataModel = JSONHelper.Deserialize<UnifiedOrderEntity>(payData);

                if (payDataModel != null)
                {

                    dingdanidnum = payDataModel.Extra["dingdanidnum"];
                    openid = payDataModel.Extra["openid"];
                    hotelid = payDataModel.Extra["hotelid"];
                    roomid = payDataModel.Extra["roomid"];
                    JumpUrl(payStatus);
                }
                else
                {
                    dingdanidnum = Request.QueryString["dingdanidnum"];
                    openid = Request.QueryString["openid"];
                    hotelid = Request.QueryString["hotelid"];
                    roomid = Request.QueryString["roomid"];
                    JumpUrl(payStatus);
                }
            }
            else
            {
                Response.Clear();
                Response.Write("不完整的参数");
            }
        }

        /// <summary>
        /// 根据状态跳转不同页面
        /// </summary>
        /// <param name="payStatus"></param>
        private void JumpUrl(string payStatus)
        {
            var url = string.Empty;
            switch (payStatus)
            {
                case "success":
                    if (!string.IsNullOrEmpty(hotelid))
                    {
                     var id=    WeiXinPF.Common.Utils.StrToInt(hotelid, 0);
                        var wxHotelsInfo = new BLL.wx_hotels_info().GetModel(id);
                        url = "hotel_userOrder.aspx?type=all&openid=" + openid +
               "&wid=" + wxHotelsInfo.wid + "&roomid=" + roomid;
                    }
                    
                    break;
                case "fail":
                case "cancel":
                      url = "hotel_order_xianshi.aspx?dingdanid=" + dingdanidnum + "&openid=" + openid +
                              "&hotelid=" + hotelid + "&roomid=" + roomid;
                    break;
            }
            Response.Redirect(url);
        }

         
    }
}