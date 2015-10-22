using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

/// <summary>
/// 商家入驻登记信息
/// </summary>
namespace WeiXinPF.Web.admin.diancai
{
    public partial class shop_register : Web.UI.ManagePage
    {
        protected int id = 0;
        private string action = MXEnums.ActionEnum.Add.ToString();

        private BLL.wx_diancai_shopinfo bll = new BLL.wx_diancai_shopinfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = MyCommFun.RequestInt("shopid");
            action = MyCommFun.QueryString("action");

            if (!string.IsNullOrEmpty(action) && action == MXEnums.ActionEnum.Edit.ToString())
            {
                if (id<=0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }

                if (!new BLL.wx_diancai_shopinfo().Exists(id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
            }

            if (!IsPostBack)
            {
                if (action == MXEnums.ActionEnum.Edit.ToString())
                {
                    ShowInfo(id);
                }

            }
        }

        private void ShowInfo(int id)
        {
            Model.wx_diancai_shopinfo model = bll.GetModel(id);

            lblShopCode.Text = model.ShopCode;
            txtHotelName.Text = model.hotelName;
            ddlKcType.Value = model.kcType;
            txtOperator.Text = model.Operator;
            txtTel.Text = model.tel;
            txtEmail.Text = model.email;

            cbRecommend.Checked = model.Recommend.HasValue && model.Recommend.Value;
        }

        private bool DoAdd()
        {
            Model.wx_diancai_shopinfo model = new Model.wx_diancai_shopinfo();

            model.wid = GetWeiXinCode().id;
            model.hotelName = txtHotelName.Text.ToString();
            model.kcType = ddlKcType.Value;
            model.Operator = txtOperator.Text.ToString();
            model.tel = txtTel.Text.ToString();
            model.email = txtEmail.Text.ToString();
            model.Recommend = cbRecommend.Checked;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加商户或者门店入驻登记信息:" + model.hotelName); //记录日志
                return true;
            };
            return false;
        }

        private bool DoEdit()
        {
            Model.wx_diancai_shopinfo model = bll.GetModel(id);

            model.hotelName = txtHotelName.Text.ToString();
            model.kcType = ddlKcType.Value;
            model.Operator = txtOperator.Text.ToString();
            model.tel = txtTel.Text.ToString();
            model.email = txtEmail.Text.ToString();
            model.Recommend = cbRecommend.Checked;

            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改商户或者门店入驻登记信息:" + model.hotelName); //记录日志
                return true;
            };
            return false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("修改商户或门店信息成功！", "shop_list.aspx", "Success");
            }
            else if(action == MXEnums.ActionEnum.Add.ToString()) //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "", "Error");
                    return;
                }
                JscriptMsg("添加商户或门店信息成功！", "shop_list.aspx", "Success");
            }
        }
    }
}