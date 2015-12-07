using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.wqiche
{
    public partial class sygj_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_sygj bll = new BLL.wx_wq_sygj();
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
                ChkAdminLevel("qc_sygj", MXEnums.ActionEnum.View.ToString()); //检查权限 
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }

        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_sygj model = bll.GetModel(_id);
            this.txtName.Text = model.Name;
            this.txtSort_id.Text = model.sort_id.ToString();
            this.txtUrl.Text = model.url;
            this.rblStatus.SelectedValue = model.gjstatus.ToString(); ;
        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_sygj model = new Model.wx_wq_sygj();
            bool result = false;
            model.gjstatus = MyCommFun.Str2Int(this.rblStatus.SelectedItem.Value);
            model.Name = this.txtName.Text;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.url = this.txtUrl.Text;
            model.wid = weixin.id;
            model.createdate = DateTime.Now;
            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加实用工具信息:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_sygj model = bll.GetModel(_id);
            bool result = false;
            model.gjstatus = MyCommFun.Str2Int(this.rblStatus.SelectedItem.Value);
            model.Name = this.txtName.Text;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.url = this.txtUrl.Text;
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改实用工具id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_sygj", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "sygjMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_sygj", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "sygjMgr.aspx", "Success");
            }
        }

    }
}