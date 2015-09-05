﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using System.Security.Cryptography;
using System.Text;
namespace MxWeiXinPF.Web.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                Session[MXKeys.SESSION_ADMIN_INFO] = null;
                Utils.WriteCookie("AdminName","MxWeiXinPF", -14400);
                Utils.WriteCookie("AdminPwd","MxWeiXinPF", -14400);

                Session["uweixinId"] = null;
                Utils.WriteCookie("uweixinId", "MxWeiXinPF", -14400);


                msgtip.InnerHtml = "请输入用户名或密码";
                txtUserName.Text = Utils.GetCookie("DTRememberName");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userPwd = txtPassword.Text.Trim();

            if (userName.Equals("") || userPwd.Equals(""))
            {
                msgtip.InnerHtml = "请输入用户名或密码";
                return;
            }
            if (Session["AdminLoginSun"] == null)
            {
                Session["AdminLoginSun"] = 1;
            }
            else
            {
                Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
            }
            //判断登录错误次数
            if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 5)
            {
                msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
                return;
            }
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(userName, userPwd, true);
            if (model == null)
            {
                msgtip.InnerHtml = "用户名或密码有误，请重试！";
                return;
            }
            Session[MXKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            //写入登录日志
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            if (siteConfig.logstatus > 0)
            {
                new BLL.manager_log().Add(model.id, model.user_name, MXEnums.ActionEnum.Login.ToString(), "用户登录");
            }
            //写入Cookies
            Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            Utils.WriteCookie("AdminName", "MxWeiXinPF", model.user_name);
            Utils.WriteCookie("AdminPwd", "MxWeiXinPF", model.password);
            if (model.agentLevel > 0)
            {
                //说明为代理商
                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Redirect("wxIndex.aspx");
            }
        }

    }
}
//1e2124dd04e11d01b9df2865f85944be