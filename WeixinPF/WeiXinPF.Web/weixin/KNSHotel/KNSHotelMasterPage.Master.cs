using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.KNSHotel
{
    using System.Data;

    using WeiXinPF.Common;
    using WeiXinPF.WeiXinComm;

    public partial class KNSHotelMasterPage : System.Web.UI.MasterPage
    {
        public int hotelid { get; set; }
        public string openid { get; set; }
        public string title { get; set; }

        public int wid { get; set; }

        public int dingdannum { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            openid = MyCommFun.QueryString("openid");
            this.title = this.GetTitle();

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.openid) && this.hotelid >0)
                {
                    var dingdanbll = new BLL.wx_hotel_dingdan();
                    var hotelInfo = new BLL.wx_hotels_info().GetModel(this.hotelid);

                    if (hotelInfo != null)
                    {
                        this.wid = hotelInfo.wid.Value;
                    }

                    DataSet dr = dingdanbll.GetList(this.openid, this.hotelid);
                    if (dr.Tables[0].Rows.Count > 0)
                    {
                        this.dingdannum = dr.Tables[0].Rows.Count;
                    }
                    else
                    {
                        this.dingdannum = 0;
                    } 
                }
                               
            }
        }

        private string GetTitle()
        {
            var pageName = WebHelper.GetPageName();

            switch (pageName)
            {
                case "hotel_detail.aspx":
                    return "关于";
                case "index.aspx":
                    return "房型选择";
                case "hotel_order.aspx":
                    return "订单";
                default:
                    return "订单";
            }
        }        
    }
}