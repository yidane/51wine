using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weixin;
using Weixin.Menu;

namespace Weixin
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateMenu_Click(object sender, EventArgs e)
        {
            //MenuManager oWeixinManager = new MenuManager();
            //var result = oWeixinManager.CreateMenu();
            //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        protected void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            //MenuManager oWeixinManager = new MenuManager();
            //var result = oWeixinManager.DeleteMenu();
            //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        protected void btnSearchMenu_Click(object sender, EventArgs e)
        {
            //MenuManager oWeixinManager = new MenuManager();
            //var result = oWeixinManager.SearchMenu();
            //Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
    }
}