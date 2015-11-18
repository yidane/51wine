using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin
{
    public partial class center : Web.UI.ManagePage
    {
        private string _userType = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Model.manager admin_info = GetAdminInfo(); //管理员信息
                //登录信息
                if (admin_info != null)
                {
                    BLL.manager_log bll = new BLL.manager_log();
                    Model.manager_log model1 = bll.GetModel(admin_info.user_name, 1, MXEnums.ActionEnum.Login.ToString());
                    if (model1 != null)
                    {
                        //本次登录
                        litIP.Text = model1.user_ip;
                    }
                    Model.manager_log model2 = bll.GetModel(admin_info.user_name, 2, MXEnums.ActionEnum.Login.ToString());
                    if (model2 != null)
                    {
                        //上一次登录
                        litBackIP.Text = model2.user_ip;
                        litBackTime.Text = model2.add_time.ToString();
                    }

                    if (IsWeiXinCode())
                    {
                        _userType = "ScenicAdmin";//景区管理员
                    }
                    else if (IsShopAdmin(admin_info.id))
                    {
                        _userType = "ShopAdmin";//餐饮管理员
                    }
                    else if (IsHotelAdmin(admin_info.id))
                    {
                        _userType = "HotelAdmin";//酒店管理员
                    }
                }

                //LitUpgrade.Text = Utils.GetDomainStr(MXKeys.CACHE_OFFICIAL_UPGRADE, DESEncrypt.Decrypt(MXKeys.FILE_URL_UPGRADE_CODE));
                //LitNotice.Text = Utils.GetDomainStr(MXKeys.CACHE_OFFICIAL_NOTICE, DESEncrypt.Decrypt(MXKeys.FILE_URL_NOTICE_CODE));
                //Utils.GetDomainStr("dt_cache_domain_info", "http://www.WeiXinPF.net/upgrade.ashx?u=" + Request.Url.DnsSafeHost + "&i=" + Request.ServerVariables["LOCAL_ADDR"]);

              
            }
        }

        private bool IsShopAdmin(int id)
        {
            BLL.wx_diancai_admin dBll = new BLL.wx_diancai_admin();
            Model.wx_diancai_admin shopAdmin = dBll.GetModel(id);

            //餐饮 商铺管理员
            if (shopAdmin != null)
            {
                //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopAdmin.ShopId;
                //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopAdmin.ShopId.ToString());

                return true;
            }

            BLL.wx_diancai_shop_user suBll = new BLL.wx_diancai_shop_user();
            Model.wx_diancai_shop_user shopUser = suBll.GetModel(id);

            if (shopUser != null)
            {
                //Session[MXKeys.WEIXIN_DIANCAI_SHOPID] = shopUser.ShopId;
                //Utils.WriteCookie(MXKeys.WEIXIN_DIANCAI_SHOPID, "WeiXinPF", shopUser.ShopId.ToString());

                return true;
            }

            return false;
        }
        private bool IsHotelAdmin(int id)
        {
            BLL.wx_hotel_admin dBll = new BLL.wx_hotel_admin();
            Model.wx_hotel_admin hotelAdmin = dBll.GetModel(id);

            //酒店管理员
            if (hotelAdmin != null)
            {
                return true;
            }

            BLL.wx_hotel_user suBll = new BLL.wx_hotel_user();
            Model.wx_hotel_user hotelUser = suBll.GetModel(id);

            if (hotelUser != null)
            {
                return true;
            }

            return false;
        }
    }
}