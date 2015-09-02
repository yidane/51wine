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
    public partial class chexing_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_chexing bll = new BLL.wx_wq_chexing();
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
                ChkAdminLevel("qc_chexing", MXEnums.ActionEnum.View.ToString()); //检查权限

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
            //品牌
            DataTable dt = bll.GetList(" wid=" + weixin.id).Tables[0];
            this.ddlPinpai.Items.Clear();
            this.ddlPinpai.Items.Add(new ListItem("请选择品牌...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["Name"].ToString().Trim();
                this.ddlPinpai.Items.Add(new ListItem(Title, Id));
            }

            //全景相册
            BLL.wx_wq_panorama pbll = new BLL.wx_wq_panorama();
            DataTable xcdt = pbll.GetList(" wid=" + weixin.id).Tables[0];
            this.ddlQjxc.Items.Clear();
            this.ddlQjxc.Items.Add(new ListItem("请选择全景图", ""));
            foreach (DataRow dr in xcdt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["jdname"].ToString().Trim();
                this.ddlQjxc.Items.Add(new ListItem(Title, Id));
            }

            //初始化车系
            this.ddlChexi.Items.Clear();
            this.ddlChexi.Items.Add(new ListItem("请选择车系....", ""));
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_chexing model = bll.GetModel(_id);
            this.txtDwnum.Text = model.dwNum.ToString();
            string[] pl = model.pailiang.Split('-');
            this.txtName.Text = model.Name;
            this.txtPic.Text = model.pic;
            this.imgPic.ImageUrl = model.pic;
            this.txtSort_id.Text = model.sort_id.ToString();
            this.txtZdprice.Text = model.zdPrice.ToString();
            this.txtJxsPrice.Text = model.jxsPrice.ToString();
            this.ddlQjxc.SelectedValue = model.qjid.ToString();
            this.txtPailiang.Text = pl[0];
            this.ddlPltype.SelectedValue = pl[1];
            this.ddlBsx.SelectedValue = model.biansuxiang;
            this.ddlNiankuan.SelectedValue = model.niankuan;

            this.ddlPinpai.SelectedValue = model.pid.ToString();
            if (model.pid != null && model.pid > 0)
            {
                bindChexi(MyCommFun.Obj2Int(model.pid));
                this.ddlChexi.SelectedValue = model.xid.ToString();
            }
        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_chexing model = new Model.wx_wq_chexing();
            bool result = false;
            model.biansuxiang = this.ddlBsx.SelectedItem.Value;
            model.dwNum = MyCommFun.Str2Int(this.txtDwnum.Text);
            model.jxsPrice = this.txtJxsPrice.Text;
            model.zdPrice = this.txtZdprice.Text;
            model.pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            model.xid = MyCommFun.Str2Int(this.ddlChexi.SelectedItem.Value);
            model.qjid = MyCommFun.Str2Int(this.ddlQjxc.SelectedItem.Value);
            model.pic = this.txtPic.Text;
            model.niankuan = this.ddlNiankuan.SelectedItem.Value;
            model.Name = this.txtName.Text;
            model.pailiang = this.txtPailiang.Text + "-" + this.ddlPltype.SelectedItem.Value;
            model.wid = weixin.id;
            model.createdate = DateTime.Now;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);

            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加产品库信息:" + model.Name); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_chexing model = bll.GetModel(_id);
            bool result = false;
            model.biansuxiang = this.ddlBsx.SelectedItem.Value;
            model.dwNum = MyCommFun.Str2Int(this.txtDwnum.Text);
            model.jxsPrice = this.txtJxsPrice.Text;
            model.zdPrice = this.txtZdprice.Text;
            model.xid = MyCommFun.Str2Int(this.ddlChexi.SelectedItem.Value);
            model.pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            model.qjid = MyCommFun.Str2Int(this.ddlQjxc.SelectedItem.Value);
            model.pic = this.txtPic.Text;
            model.niankuan = this.ddlNiankuan.SelectedItem.Value;
            model.Name = this.txtName.Text;
            model.pailiang = this.txtPailiang.Text + "-" + this.ddlPltype.SelectedItem.Value;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改车型内容id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_chexing", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "chexingMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_chexing", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "chexingMgr.aspx", "Success");
            }
        }

        protected void ddlPinpai_SelectedIndexChanged(object sender, EventArgs e)
        {
            //相应品牌的所有车系
            int pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            if (pid > 0)
            {
                bindChexi(pid);
            }
        }

        void bindChexi(int pid)
        {
            this.ddlChexi.Items.Clear();
            BLL.wx_wq_chexi cxBll = new BLL.wx_wq_chexi();
            Model.wx_userweixin weixin = GetWeiXinCode();
            DataTable cxdt = cxBll.GetList(" wid=" + weixin.id + " and pid=" + pid).Tables[0];
            foreach (DataRow dr in cxdt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["name"].ToString().Trim();
                this.ddlChexi.Items.Add(new ListItem(Title, Id));
            }
        }

    }
}