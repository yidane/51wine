using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.admin.wqiche
{
    public partial class xiaoshou_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_xiaoshou bll = new BLL.wx_wq_xiaoshou();
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
                ChkAdminLevel("qc_xiaoshou", MXEnums.ActionEnum.View.ToString()); //检查权限
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }

        } 

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_xiaoshou model = bll.GetModel(_id);
            this.txtHeadpic.Text = model.headpic;
            this.imgHeadpic.ImageUrl = model.headpic;
            this.txtName.Text = model.Name;
            this.txtSort_id.Text = model.sort_id.ToString();
            this.txtSummary.InnerText = model.remark;
            this.txtTelephone.Text = model.telephone;
        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_xiaoshou model = new Model.wx_wq_xiaoshou();
            bool result = false;
            model.createdate = DateTime.Now;
            model.wid = weixin.id;
            model.headpic = this.txtHeadpic.Text;
            model.Name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int( this.txtSort_id.Text);
            model.telephone = this.txtTelephone.Text; 
            model.xsType = this.ddlType.SelectedItem.Value;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加销售顾问:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_xiaoshou model = bll.GetModel(_id);
            bool result = false;
            model.headpic = this.txtHeadpic.Text;
            model.Name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.telephone = this.txtTelephone.Text;
            model.xsType = this.ddlType.SelectedItem.Value;
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改销售顾问id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_xiaoshou", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "xiaoshouMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_xiaoshou", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "xiaoshouMgr.aspx", "Success");
            }
        }
    }
}