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
    public partial class yuyue_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        private string type;
        BLL.wx_wq_yuyue bll = new BLL.wx_wq_yuyue();
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = MXRequest.GetQueryString("action");
            type = MXRequest.GetQueryString("type");
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
                else
                {
                    ckType(type);
                }
            }

        }

        void ckType(string type)
        {
            if (type == "1")
            {
                this.litType.Text = "预约保养";
            }
            else
            {
                this.litType.Text = "预约试驾";
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_yuyue model = bll.GetModel(_id);
            ckType(model.type);
            this.txtAddress.Text = model.address;
            this.txtHeadpic.Text = model.headpic;
            this.imgHeadpic.ImageUrl = model.headpic; 
            this.txtLatXPoint.Text = model.lngX.ToString();
            this.txtLngYPoint.Text = model.latY.ToString();
            this.txtName.Text = model.Name;
            this.txtNewscover.Text = model.coverpic;
            this.imgNewscover.ImageUrl = model.coverpic;
            this.txtSummary.InnerText = model.remark;
            this.txtTelephone.Text = model.telephone; 

        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_yuyue model = new Model.wx_wq_yuyue();
            bool result = false;
            model.address = this.txtAddress.Text;
            model.coverpic = this.txtNewscover.Text;
            model.headpic = this.txtHeadpic.Text;
            model.latY = MyCommFun.Str2Decimal(this.txtLngYPoint.Text);
            model.lngX = MyCommFun.Str2Decimal(this.txtLatXPoint.Text);
            model.Name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = 99;
            model.telephone = this.txtTelephone.Text; 
            model.wid = weixin.id;
            model.type = type;
            model.createdate = DateTime.Now;
            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加预约:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_yuyue model = bll.GetModel(_id);
            bool result = false;
            model.address = this.txtAddress.Text;
            model.coverpic = this.txtNewscover.Text;
            model.headpic = this.txtHeadpic.Text;
            model.latY = MyCommFun.Str2Decimal(this.txtLngYPoint.Text);
            model.lngX = MyCommFun.Str2Decimal(this.txtLatXPoint.Text);
            model.Name = this.txtName.Text;
            model.remark = this.txtSummary.InnerText;
            model.sort_id = 99;
            model.telephone = this.txtTelephone.Text; 
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改预约id:" + model.Id); //记录日志
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
                JscriptMsg("修改信息成功！", "yuyueMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_yuyue", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "yuyueMgr.aspx", "Success");
            }
        }
    }
}