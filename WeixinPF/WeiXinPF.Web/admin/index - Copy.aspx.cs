using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin
{
    /// <summary>
    /// 为了消息提醒加的
    /// </summary>
    public partial class index_copy : Web.UI.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
                if (admin_info.agentLevel > 0)
                {
                    //说明为代理商
                    mygzh.Style.Add("display", "none");
                  //  Response.Redirect("index.aspx");
                }
                else
                {
                    indexUrl.HRef = "wxIndex.aspx";
                  //  Response.Redirect("wxIndex.aspx");
                }
            }
        }

        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[MXKeys.SESSION_ADMIN_INFO] = null;
            Utils.WriteCookie("AdminName", "WeiXinPF", -14400);
            Utils.WriteCookie("AdminPwd", "WeiXinPF", -14400);

            Session["uweixinId"] = null;
            Utils.WriteCookie("uweixinId", "WeiXinPF", -14400);

            Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = null;
            Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", -14400);

            Response.Redirect("login.aspx");
        }

    }
}