using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.BLL;
using System.Configuration;

namespace WeiXinPF.Web.admin.cashred
{
    public partial class basesetting : Web.UI.ManagePage
    {
        BLL.wx_userweixin bll = new BLL.wx_userweixin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                ShowInfo();

            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            if (weixin.wxType != 4)
            { 
                //必须是认证过的服务号才可以开通该功能
                JscriptMsg("必须是认证过的服务号(并且开通微信支付)才可以开通该功能！", "back", "Error");
                return;
            }
            BLL.wx_xjhongbao_base xjBll = new wx_xjhongbao_base();
            Model.wx_xjhongbao_base model = xjBll.GetModelByWid(weixin.id);
            if (model == null || model.id == 0)
            {
                //新增记录
            }
            else
            {
                //修改
                hidId.Value = model.id.ToString();
                this.txtaccountBalance.Text = (model.accountBalance/100.00).ToString();
                this.lbltotalLQMoney.Text = (model.totalLQMoney/100.00).ToString();
                this.txtremark.Value = model.remark;

                if (MyCommFun.ObjToStr(model.warningInfo, "") != "")
                {
                    litWarning.Text = model.warningInfo;
                    mytips.Style.Add("display", "");
                }
                else {
                    mytips.Style.Add("display", "none");
                    }

            }

        }

        #endregion


        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            decimal accb = MyCommFun.Obj2Decimal(txtaccountBalance.Text.Trim(), -1);
            if (accb == -1)
            {
                JscriptMsg("余额输入有错误！", "", "Error");
                return;
            }
            BLL.wx_xjhongbao_base wxpayBll = new wx_xjhongbao_base();
            int id = MyCommFun.Obj2Int(hidId.Value, 0);
            Model.wx_xjhongbao_base baseModel = new Model.wx_xjhongbao_base();
            if (id == 0)
            {
                //新增
                Model.wx_userweixin weixin = GetWeiXinCode();

                baseModel.wid = weixin.id;
                baseModel.createDate = DateTime.Now;
                baseModel.totalLQMoney = 0;
            }
            else
            {
                //修改
                baseModel = wxpayBll.GetModel(id);
            }

            baseModel.accountBalance = (int)(accb * 100);
            baseModel.remark = txtremark.Value.Trim();

            bool ret = false;
            if (id == 0)
            {
                int retNum = wxpayBll.Add(baseModel);
                if (retNum > 0)
                {
                    ret = true;
                }
            }
            else
            {
                ret = wxpayBll.Update(baseModel);
            }

            if (ret)
            {
                JscriptMsg("修改信息成功！", "basesetting.aspx", "Success");
            }
            else
            {
                JscriptMsg("保存过程中发生错误！", "", "Error");
                return;
            }

        }
    }
}