using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.admin.wqiche
{
    public partial class logo_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_pinpai bll = new BLL.wx_wq_pinpai();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                ChkAdminLevel("qc_pinpai", MXEnums.ActionEnum.View.ToString()); //检查权限 
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }

        }
        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_pinpai model = bll.GetModel(_id);
            this.txtLogo.Text = model.logo;
            this.imgLogo.ImageUrl = model.logo;
            this.txtName.Text = model.name;
            this.txtSort_id.Text = model.sort_id.ToString();
            this.txtWebsite.Text = model.website;
            this.txtSummary.InnerText = model.remark;

        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_pinpai model = new Model.wx_wq_pinpai();
            bool result = false;
            model.logo = this.txtLogo.Text;
            model.name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.website = this.txtWebsite.Text;
            model.wid = weixin.id;
            model.cratedate = DateTime.Now;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加产品库信息:" + model.name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_pinpai model = bll.GetModel(_id);
            bool result = false;
            model.logo = this.txtLogo.Text;
            model.name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.website = this.txtWebsite.Text;
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改汽车品牌内容id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_pinpai", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "logoMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_pinpai", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "logoMgr.aspx", "Success");
            }
        }
    }
}