/**************************************
 * 
 * author:李朴
 * company:上海沐雪网络科技有限公司
 * website:http://uweixin.cn
 * createDate:2013-11-1
 * update:2014-12-30
 * remark:后台页面的首页
 * 
 ***********************************/

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
namespace WeiXinPF.Web.admin
{
    public partial class wxIndex : Web.UI.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo();
            }
        }



        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session[MXKeys.SESSION_ADMIN_INFO] = null;
            Utils.WriteCookie("AdminName", "WeiXinPF", -14400);
            Utils.WriteCookie("AdminPwd", "WeiXinPF", -14400);
            Response.Redirect("login.aspx");
        }
    }
}