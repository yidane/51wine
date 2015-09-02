using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.admin.wqiche
{
    public partial class chexi_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_chexi bll = new BLL.wx_wq_chexi();
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
                ChkAdminLevel("qc_chexi", MXEnums.ActionEnum.View.ToString()); //检查权限

                TreeBind(); //绑定类别
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }

        }

        #region 绑定类别=================================
        private void TreeBind()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            BLL.wx_wq_pinpai bll = new BLL.wx_wq_pinpai();
            DataTable dt = bll.GetList(" wid=" + weixin.id).Tables[0];

            this.ddlPinpai.Items.Clear();
            this.ddlPinpai.Items.Add(new ListItem("请选择品牌...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["Name"].ToString().Trim();
                this.ddlPinpai.Items.Add(new ListItem(Title, Id));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_chexi model = bll.GetModel(_id);
            this.txtCximg.Text = model.pic;
            this.imgCximg.ImageUrl = model.pic;
            this.txtJname.Text = model.jName;
            this.txtName.Text = model.Name;
            this.txtSort_id.Text = model.sort_id.ToString();
            this.txtSummary.InnerHtml = model.remark;
            ddlPinpai.SelectedValue = model.pid.ToString();

        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_chexi model = new Model.wx_wq_chexi();
            bool result = false;
            model.jName = this.txtJname.Text;
            model.Name = this.txtName.Text;
            model.pic = this.txtCximg.Text;
            model.pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            model.wid = weixin.id;
            model.createdate = DateTime.Now;
            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加车系信息:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_chexi model = bll.GetModel(_id);
            bool result = false;
            model.jName = this.txtJname.Text;
            model.Name = this.txtName.Text;
            model.pic = this.txtCximg.Text;
            model.pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            model.remark = this.txtSummary.InnerText;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改车系内容id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_chexi", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "chexiMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_chexi", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "chexiMgr.aspx", "Success");
            }
        }
    }
}