using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.wqiche
{
    public partial class orderDetail : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        protected int type;
        BLL.wx_wq_yyOrder bll = new BLL.wx_wq_yyOrder();
        protected void Page_Load(object sender, EventArgs e)
        {
            type = MXRequest.GetQueryInt("type");
            string _action = MXRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == MXEnums.ActionEnum.Edit.ToString())
            {
                this.action = MXEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = MXRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!bll.Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("qc_yuyue", MXEnums.ActionEnum.View.ToString()); //检查权限 
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.wx_wq_pinpai ppBll = new BLL.wx_wq_pinpai();
            BLL.wx_wq_chexi cxBll = new BLL.wx_wq_chexi();
            Model.wx_wq_yyOrder model = bll.GetModel(_id);
            if (type == 1)//保养
            {
                this.pelShow.Visible = true;
                this.lblCarnum.Text = model.carnum;
                this.lblKm.Text = model.km.ToString();
            }
            else       //试驾
            {
                this.pelShow.Visible = false;
            }
            string pinpai = ppBll.GetModel(MyCommFun.Obj2Int(model.pid)).name;
            string chexi = cxBll.GetModel(MyCommFun.Obj2Int(model.xid)).Name;

            this.lblObj.Text = pinpai + "/" + chexi;
            this.lblremark.Text = model.remark;
            this.lblTelephone.Text = model.telephone.ToString();
            this.lblYYPerson.Text = model.Name;
            this.lblYytime.Text = MyCommFun.Obj2DateTime(model.yydate).ToString("yyyy-MM-dd") + "/" + model.yytime;
            this.lblXdtime.Text = MyCommFun.Obj2DateTime(model.createdate).ToString("yyyy年MM月dd日 hh:mm:ss点");
            this.txtfSummary.InnerText = model.kfremark;
            this.ddlStatus.SelectedValue = model.ddstatus;

        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_yyOrder model = bll.GetModel(_id);
            bool result = false;
            model.ddstatus = this.ddlStatus.SelectedItem.Value;
            model.kfremark = this.txtfSummary.InnerText;
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改预约订单内容id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_yuyue", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "orderMgr.aspx?type=" + type, "Success");
            }
        }
    }
}