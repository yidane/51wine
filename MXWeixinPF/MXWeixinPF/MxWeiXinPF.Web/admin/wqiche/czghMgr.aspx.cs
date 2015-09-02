using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.admin.wqiche
{
    public partial class czghMgr : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_czgh bll = new BLL.wx_wq_czgh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Model.wx_userweixin weixin = GetWeiXinCode();
                ChkAdminLevel("qc_czgh", MXEnums.ActionEnum.View.ToString()); //检查权限 
                IList<Model.wx_wq_czgh> ghModel = bll.GetModelList("wid=" + weixin.id);
                if (ghModel != null && ghModel.Count > 0) //修改
                {
                    id = ghModel[0].Id;
                    ShowInfo(this.id);
                }
            }

        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_czgh model = bll.GetModel(_id);
            
            this.txtNewscover.Text = model.newscover;
            this.imgNewscover.ImageUrl = model.newscover;
            this.txtSummary.InnerText = model.remark;
            this.txtTitle.Text = model.title;
        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_czgh model = new Model.wx_wq_czgh();
            bool result = false;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = 99;
            model.title = this.txtTitle.Text;
            model.wid = weixin.id;
            model.createdate = DateTime.Now;
            model.newscover = this.txtNewscover.Text;
            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加车主关怀:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_czgh model = bll.GetModel(_id);
            bool result = false;
            model.remark = this.txtSummary.InnerText;
            model.newscover = this.txtNewscover.Text;
            model.title = this.txtTitle.Text;
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改车主关怀内容id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IList<Model.wx_wq_czgh> ghList = bll.GetModelList(" id>0");
            if (ghList != null & ghList.Count > 0) //修改
            {
                ChkAdminLevel("qc_czgh", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                id = ghList[0].Id;
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "czghMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_czgh", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "czghMgr.aspx", "Success");
            }
        }
    }
}