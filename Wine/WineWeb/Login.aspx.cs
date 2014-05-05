using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WineWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginIn_Click(object sender, EventArgs e)
        {
            var loginNameSys = System.Configuration.ConfigurationManager.AppSettings["LoginName"];
            var passwordSys = System.Configuration.ConfigurationManager.AppSettings["Password"];

            var loginName = this.txtLoginName.Text.Trim();
            var password = this.txtPassword.Text.Trim();

            if (string.Equals(loginName, loginNameSys) && string.Equals(password, passwordSys))
            {
                Session["LoginUser"] = true;
                Response.Redirect("Index.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "loginFail", "<script type=\"text/javascript\">alert(\"登录失败\");</script>");
            }
        }
    }
}