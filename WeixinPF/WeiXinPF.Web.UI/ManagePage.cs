using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal Model.siteconfig siteConfig;

        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new BLL.siteconfig().loadConfig();
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                Response.Write("<script>parent.location.href='" + siteConfig.webpath + siteConfig.webmanagepath + "/login.aspx'</script>");
                Response.End();
            }
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[MXKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = Utils.GetCookie("AdminName", "WeiXinPF");
                string adminpwd = Utils.GetCookie("AdminPwd", "WeiXinPF");
                if (adminname != "" && adminpwd != "")
                {
                    BLL.manager bll = new BLL.manager();
                    Model.manager model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        Session[MXKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.manager model = Session[MXKeys.SESSION_ADMIN_INFO] as Model.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }



        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string nav_name, string action_type)
        {
            Model.manager model = GetAdminInfo();
            BLL.manager_role bll = new BLL.manager_role();
            bool result = bll.Exists(model.role_id, nav_name, action_type);

            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\", \"Error\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            if (siteConfig.logstatus > 0)
            {
                Model.manager model = GetAdminInfo();
                int newId = new BLL.manager_log().Add(model.id, model.user_name, action_type, remark);
                if (newId > 0)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        /// <summary>
        /// 带确认按钮的提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptConfirmMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsconfirm(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsConfirm", msbox, true);
        }


        #endregion
        public bool IsWeiXinCode()
        {

            //如果Session为Null
            if (Session["nowweixin"] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string uweixinId = Utils.GetCookie("nowweixinId", "WeiXinPF");
                if (uweixinId != "")
                {
                    BLL.wx_userweixin bll = new BLL.wx_userweixin();
                    Model.wx_userweixin model = bll.GetModel(int.Parse(uweixinId));
                    if (model != null)
                    {
                        Session["nowweixin"] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得当前微信帐号信息
        /// </summary>
        public Model.wx_userweixin GetWeiXinCode()
        {
            if (IsWeiXinCode())
            {
                Model.wx_userweixin model = Session["nowweixin"] as Model.wx_userweixin;
                if (model != null)
                {
                    return model;
                }
            }
            else
            {
                int shopid = GetShopId();
                if (shopid != 0)
                {
                    BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
                    Model.wx_diancai_shopinfo shop = shopBll.GetModel(shopid);

                    return new BLL.wx_userweixin().GetModel(shop.wid.Value);
                }

                int hotelid = GetHotelId();
                if (hotelid != 0)
                {
                    BLL.wx_hotels_info hotelBll = new BLL.wx_hotels_info();
                    Model.wx_hotels_info hotel = hotelBll.GetModel(hotelid);

                    return new BLL.wx_userweixin().GetModel(hotel.wid.Value);
                }
                Response.Write("<script>parent.location.href='http://" + HttpContext.Current.Request.Url.Authority + "/admin/weixin/myweixinlist.aspx'</script>");
                Response.End();
            }
            return null;
        }

        public int GetShopId()
        {
            if (IsAdminLogin())
            {
                Model.manager admin = GetAdminInfo();
                BLL.wx_diancai_admin shopAdminBll = new BLL.wx_diancai_admin();
                Model.wx_diancai_admin shopAdmin = shopAdminBll.GetModel(admin.id);
                if (shopAdmin != null)
                {
                    return shopAdmin.ShopId;
                }

                BLL.wx_diancai_shop_user suBll = new BLL.wx_diancai_shop_user();
                Model.wx_diancai_shop_user shopUser = suBll.GetModel(admin.id);

                if (shopUser != null)
                {
                    return shopUser.ShopId;
                }
                return 0;
            }
            return 0;
        }

        public int GetHotelId()
        {
            if (IsAdminLogin())
            {
                Model.manager admin = GetAdminInfo();
                BLL.wx_hotel_admin hotelAdminBll = new BLL.wx_hotel_admin();
                Model.wx_hotel_admin hotelAdmin = hotelAdminBll.GetModel(admin.id);
                if (hotelAdmin != null)
                {
                    return hotelAdmin.HotelId;
                }

                BLL.wx_hotel_user suBll = new BLL.wx_hotel_user();
                Model.wx_hotel_user hotelUser = suBll.GetModel(admin.id);

                if (hotelUser != null)
                {
                    return hotelUser.HotelId;
                }

                return 0;
            }
            return 0;

        }
    }
}
