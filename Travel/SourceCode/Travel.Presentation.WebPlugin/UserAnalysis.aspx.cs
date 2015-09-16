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
        public WeChatWebService webService=new WeChatWebService();
        public string json_data="";
        public DateTime now_date = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                json_data = webService.GetUserAnalysis(Convert.ToDateTime("2015-09-01"), Convert.ToDateTime("2015-09-14"));
            }
        }

        protected void sevendays_Click(object sender, EventArgs e)
        {
            DateTime start_date = now_date.AddDays(-8);
            json_data = webService.GetUserAnalysis(start_date, now_date.AddDays(-1));
        }

        protected void fourteendays_Click(object sender, EventArgs e)
        {
            DateTime start_date = now_date.AddDays(-15);
            json_data = webService.GetUserAnalysis(start_date, now_date.AddDays(-1));
        }

        protected void thirtydays_Click(object sender, EventArgs e)
        {
            DateTime start_date = now_date.AddDays(-31);
            json_data = webService.GetUserAnalysis(start_date, now_date.AddDays(-1));
        }
    }
}