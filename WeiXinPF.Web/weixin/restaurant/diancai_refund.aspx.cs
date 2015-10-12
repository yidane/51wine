using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class diancai_refund : System.Web.UI.Page
    {
        public string RestruantName = string.Empty;
        public string OrderNumber = string.Empty;
        public string CaiPinName = string.Empty;
        public decimal Price = 0;
        public int NoUseCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            RestruantName = "旺德福";
            OrderNumber = "9310849202720";
            CaiPinName = "照烧煎鸡饭";
            Price = 15;
            NoUseCount = 5;
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            var refundCount = Convert.ToInt32(this.txtRefundCount.Text);

            Response.Write(string.Format("<script type=\"text/javascript\">alert(\"退单数量{0}\");</script>", refundCount));
        }
    }
}