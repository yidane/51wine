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
        public WeChatWebService webService;
        public string json_data="";
        protected void Page_Load(object sender, EventArgs e)
        {
            webService = new WeChatWebService();
            json_data=webService.GetUserAnalysis(Convert.ToDateTime("2015-09-08"),Convert.ToDateTime("2015-09-14"));
        }
    }
}