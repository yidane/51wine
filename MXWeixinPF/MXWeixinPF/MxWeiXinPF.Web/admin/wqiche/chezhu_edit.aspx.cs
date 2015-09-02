using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using System.Data;

namespace MxWeiXinPF.Web.admin.wqiche
{
    public partial class chezhu_edit : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_chezhu bll = new BLL.wx_wq_chezhu();
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
                ChkAdminLevel("wq_chezhu", MXEnums.ActionEnum.View.ToString()); //检查权限

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

            this.ddlChexi.Items.Clear();
            this.ddlChexi.Items.Add(new ListItem("请选择车系...", ""));

            this.ddlChexing.Items.Clear();
            this.ddlChexing.Items.Add(new ListItem("请选择车型...", ""));
        }
        #endregion

        #region 获得车系
        void getChexi(int id)
        {
            BLL.wx_wq_chexi ciBll = new BLL.wx_wq_chexi();
            Model.wx_userweixin weixin = GetWeiXinCode();
            DataTable dt = ciBll.GetList(" wid=" + weixin.id + " and pid=" + id).Tables[0];
            this.ddlChexi.Items.Clear();
            this.ddlChexi.Items.Add(new ListItem("请选择车系...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["Name"].ToString().Trim();
                this.ddlChexi.Items.Add(new ListItem(Title, Id));
            }
        }
        #endregion

        #region 获得车型
        void getChexing(int id, int pid)
        {
            BLL.wx_wq_chexing cgBll = new BLL.wx_wq_chexing();
            Model.wx_userweixin weixin = GetWeiXinCode();
            DataTable dt = cgBll.GetList(" wid=" + weixin.id + " and xid=" + id + " and pid=" + pid).Tables[0];
            this.ddlChexing.Items.Clear();
            this.ddlChexing.Items.Add(new ListItem("请选择车型...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["Name"].ToString().Trim();
                this.ddlChexing.Items.Add(new ListItem(Title, Id));
            }
        }
        #endregion


        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_chezhu model = bll.GetModel(_id);
            //获得品牌，车系，车型
            string pid = model.ppid.ToString();
            this.ddlPinpai.SelectedValue = pid;
            string cid = model.cxid.ToString();
            getChexi(MyCommFun.Str2Int(pid));
            this.ddlChexi.SelectedValue = cid;
            string cgid = model.cxingid.ToString();
            getChexing(MyCommFun.Str2Int(pid), MyCommFun.Str2Int(cgid));
            this.ddlChexing.SelectedValue = cgid;

            this.txtCpNum.Text = model.cpNum;
            this.txtGcTime.Text = model.gcdate.ToString();
            this.txtName.Text = model.Name;
            this.txtPrevByDate.Text = model.prevBydate.ToString();
            this.txtPrevByLicheng.Text = model.prevBylicheng.ToString();
            this.txtPrevByMoney.Text = model.prevBymoney.ToString();
            this.txtPrevBxdate.Text = model.prevNjdate.ToString();
            this.txtPrevBxMoney.Text = model.prevBxmoney.ToString();
            this.txtPrevNjtime.Text = model.prevBxdate.ToString();

            this.txtGcTime.Text = model.gcdate.ToString();
            this.txtSpTime.Text = model.spdate.ToString();
            this.txtTel.Text = model.teltephone;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_chezhu model = bll.GetModel(_id);
            bool result = false;

            model.cpNum = this.txtCpNum.Text;
            model.cxid = MyCommFun.Str2Int(this.ddlChexi.SelectedItem.Value);
            model.cxingid = MyCommFun.Str2Int(this.ddlChexing.SelectedItem.Value);
            model.gcdate = MyCommFun.Obj2DateTime(this.txtGcTime.Text);
            model.Name = this.txtName.Text;
            model.ppid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            model.prevBxdate = MyCommFun.Obj2DateTime(this.txtPrevBxdate.Text);
            model.prevBxmoney = MyCommFun.Str2Decimal(this.txtPrevBxMoney.Text);
            model.prevBydate = MyCommFun.Obj2DateTime(this.txtPrevByDate.Text);
            model.prevBylicheng = MyCommFun.Str2Decimal(this.txtPrevByLicheng.Text);
            model.prevBymoney = MyCommFun.Str2Decimal(this.txtPrevByMoney.Text);
            model.prevNjdate = MyCommFun.Obj2DateTime(this.txtPrevNjtime.Text);
            model.spdate = MyCommFun.Obj2DateTime(this.txtSpTime.Text);
            model.teltephone = this.txtTel.Text;
            model.sort_id = MyCommFun.Str2Int(this.txtSort_id.Text);
 

            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改车主id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("qc_chezhu", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "chezhuMgr.aspx", "Success");
            }
        }

        protected void ddlPinpai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            getChexi(pid);
        }

        protected void ddlChexi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pid = MyCommFun.Str2Int(this.ddlPinpai.SelectedItem.Value);
            int xid = MyCommFun.Str2Int(this.ddlChexi.SelectedItem.Value);
            getChexing(xid, pid);
        }
    }
}