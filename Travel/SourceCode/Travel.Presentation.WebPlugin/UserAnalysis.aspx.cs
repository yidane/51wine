using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Presentation.WebPlugin.Order;
using System.Web.Services;
using Travel.Presentation.WebPlugin.WebServices;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.WebPlugin
{
    public partial class UserAnalysis : System.Web.UI.Page
    {
        public WeChatWebService webService = new WeChatWebService();
        public string json_data = "";
        public DateTime endDate = DateTime.Now.AddDays(-1);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                json_data = webService.GetUserAnalysis(endDate.AddDays(-6), endDate);
            }
        }

        protected void sevendays_Click(object sender, EventArgs e)
        {
            json_data = webService.GetUserAnalysis(endDate.AddDays(-6), endDate);
            this.sevendays.CssClass = "btn btn_default selected";
            this.fourteendays.CssClass = "btn btn_default";
            this.thirtydays.CssClass = "btn btn_default";
        }

        protected void fourteendays_Click(object sender, EventArgs e)
        {
            json_data = webService.GetUserAnalysis(endDate.AddDays(-14), endDate);
            this.sevendays.CssClass = "btn btn_default";
            this.fourteendays.CssClass = "btn btn_default selected";
            this.thirtydays.CssClass = "btn btn_default";
        }

        protected void thirtydays_Click(object sender, EventArgs e)
        {
            json_data = webService.GetUserAnalysis(endDate.AddDays(-30), endDate);
            this.sevendays.CssClass = "btn btn_default";
            this.fourteendays.CssClass = "btn btn_default";
            this.thirtydays.CssClass = "btn btn_default selected";
        }
    }
}