
/**************************************
 *
 * A_uthor:Li~ Pu
 * company:上—海-沐-雪-科-技-有-限-公-司-版@权-所-有
 * qq: 2_3_0_0_2_8_0_7
 * createDate:2015-8-1
 * update:2015-8-1
 * 
 ***********************************/

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.BLL;
using System.Configuration;

namespace WeiXinPF.Web.admin.weixin
{
    /// <summary>
    /// 微支付的相关配置
    /// li pu   
    /// </summary>
    public partial class baseSetting : Web.UI.ManagePage
    {

        BLL.wx_userweixin bll = new BLL.wx_userweixin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //txtapiurl.Text = MyCommFun.getWebSite() + "/api/weixin/api.aspx";
                ChkAdminLevel("wx_paysetting", MXEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();

            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            lblWeixinName.Text = weixin.wxName;
            lblAppid.Text = weixin.AppId;

            BLL.wx_payment_wxpay wxpayBll = new wx_payment_wxpay();
            Model.wx_payment_wxpay model = wxpayBll.GetModelByWid(weixin.id);
            if (model == null || model.id == 0)
            {
                //新增记录
            }
            else
            { 
                //修改
                hidId.Value = model.id.ToString();
                this.txtmch_id.Text = model.mch_id;
                this.txtpaykey.Text = model.paykey;
                this.txtcertInfoPath.Text = model.certInfoPath;
                this.txtcerInfoPwd.Text = model.cerInfoPwd;

            }
            BLL.wx_property_info propertyBll = new wx_property_info();
            Model.wx_property_info propertyEntity = propertyBll.GetModelByIName(weixin.id, MXEnums.WXPropertyKeyName.OpenOauth.ToString());
            if (propertyEntity != null)
            {
                radOpenOAuth.SelectedValue = propertyEntity.iContent;
                hidOpenOauthId.ID = propertyEntity.id.ToString();
            }
            
        }

        #endregion
 

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
             BLL.wx_payment_wxpay wxpayBll = new wx_payment_wxpay();
            int id =MyCommFun.Obj2Int( hidId.Value,0);
            Model.wx_payment_wxpay wxpayModel = new Model.wx_payment_wxpay();
            Model.wx_userweixin weixin = GetWeiXinCode();
            if (id == 0)
            {
                //新增
                
                  wxpayModel.wid = weixin.id;
                  wxpayModel.createDate = DateTime.Now;
            }
            else
            { 
                //修改
                wxpayModel = wxpayBll.GetModel(id);
            }

            wxpayModel.mch_id = txtmch_id.Text.Trim();
            wxpayModel.paykey = txtpaykey.Text.Trim();
            wxpayModel.certInfoPath = txtcertInfoPath.Text.Trim();
            wxpayModel.cerInfoPwd = txtcerInfoPwd.Text.Trim();
           
            bool ret = false;
            if (id == 0)
            {
                wxpayModel.createDate = DateTime.Now;
                int retNum=wxpayBll.Add(wxpayModel);
                if (retNum > 0)
                {
                    ret = true;
                }
            }
            else
            {
               ret= wxpayBll.Update(wxpayModel);
            }

            //OpenOAuth开启
            BLL.wx_property_info propertyBll = new wx_property_info();
            string pValue=radOpenOAuth.SelectedItem.Value;
            propertyBll.AddProperty(weixin.id, MXEnums.WXPropertyKeyName.OpenOauth.ToString(), pValue);

            if (ret)
            {
                JscriptMsg("修改信息成功！", "baseSetting.aspx", "Success");
            }
            else
            {
                JscriptMsg("修改信息成功！", "", "Error");
                return;
            }
            
        }

    }
}