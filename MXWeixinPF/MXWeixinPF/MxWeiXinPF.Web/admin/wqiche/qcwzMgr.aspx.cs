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
    public partial class qcwzMgr : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_wq_wzlx bll = new BLL.wx_wq_wzlx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("productlist", MXEnums.ActionEnum.View.ToString()); //检查权限 
                Model.wx_userweixin weixin = GetWeiXinCode();
                TreeBind(); //绑定类别
                IList<Model.wx_wq_wzlx> lxModle = bll.GetModelList("wid=" + weixin.id);
                if (lxModle != null && lxModle.Count > 0) //修改
                {
                    id = lxModle[0].Id;
                    ShowInfo(this.id);
                }
            }

        }

        #region 绑定类别=================================
        private void TreeBind()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(" wid=" + weixin.id);

            //最新车讯
            this.ddlZxcx.Items.Clear();
            this.ddlZxcx.Items.Add(new ListItem("请选择最新车讯...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["title"].ToString().Trim();
                this.ddlZxcx.Items.Add(new ListItem(Title, Id));
            }
            //最新优惠
            this.ddlZxyh.Items.Clear();
            this.ddlZxyh.Items.Add(new ListItem("请选择最新优惠...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["title"].ToString().Trim();
                this.ddlZxyh.Items.Add(new ListItem(Title, Id));
            }
            //尊享二手车
            this.ddlEsc.Items.Clear();
            this.ddlEsc.Items.Add(new ListItem("请选择二手车...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["title"].ToString().Trim();
                this.ddlEsc.Items.Add(new ListItem(Title, Id));
            }

            //品牌相册
            BLL.wx_albums_info aiBll = new BLL.wx_albums_info();
            DataTable aidt = aiBll.GetList(" wid=" + weixin.id).Tables[0];
            this.ddlPpxc.Items.Clear();
            this.ddlPpxc.Items.Add(new ListItem("请选择品牌相册...", ""));
            foreach (DataRow dr in aidt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["aname"].ToString().Trim();
                this.ddlPpxc.Items.Add(new ListItem(Title, Id));
            }

            //LBS
            BLL.wx_lbs_shopInfo lbsBll = new BLL.wx_lbs_shopInfo();
            DataTable lbsdt = lbsBll.GetList(" wid=" + weixin.id).Tables[0];
            this.ddlLbs.Items.Clear();
            this.ddlLbs.Items.Add(new ListItem("请选择LBS...", ""));
            foreach (DataRow dr in lbsdt.Rows)
            {
                string Id = dr["id"].ToString();
                string Title = dr["shopName"].ToString().Trim();
                this.ddlLbs.Items.Add(new ListItem(Title, Id));
            }

        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_wq_wzlx model = bll.GetModel(_id);
            this.ddlEsc.SelectedValue = model.escid.ToString();
            this.ddlLbs.SelectedValue = model.lbsid.ToString();
            this.ddlPpxc.SelectedValue = model.pxcid.ToString();
            this.ddlZxyh.SelectedValue = model.yhid.ToString();
            this.ddlZxcx.SelectedValue = model.cxid.ToString();
        }
        #endregion


        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_wzlx model = new Model.wx_wq_wzlx();
            bool result = false;
            model.yhid = MyCommFun.Str2Int(this.ddlZxyh.SelectedItem.Value);
            model.lbsid = MyCommFun.Str2Int(this.ddlLbs.SelectedItem.Value);
            model.pxcid = MyCommFun.Str2Int(this.ddlPpxc.SelectedItem.Value);
            model.escid = MyCommFun.Str2Int(this.ddlEsc.SelectedItem.Value);
            model.cxid = MyCommFun.Str2Int(this.ddlZxcx.SelectedItem.Value);
            model.wid = weixin.id;
            model.createdate = DateTime.Now;
            model.sort_id = 99;
            if (bll.Add(model) > 0)
            {
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加汽车文章类型信息"); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_wq_wzlx model = bll.GetModel(_id);
            bool result = false;
            model.yhid = MyCommFun.Str2Int(this.ddlZxyh.SelectedItem.Value);
            model.lbsid = MyCommFun.Str2Int(this.ddlLbs.SelectedItem.Value);
            model.pxcid = MyCommFun.Str2Int(this.ddlPpxc.SelectedItem.Value);
            model.escid = MyCommFun.Str2Int(this.ddlEsc.SelectedItem.Value);
            model.cxid = MyCommFun.Str2Int(this.ddlZxcx.SelectedItem.Value);
            if (bll.Update(model))
            {
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改汽车文章类型id:" + model.Id); //记录日志
                result = true;
            }
            return result;
        }
        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (bll.GetRecordCount("id>0") > 0) //修改
            {
                ChkAdminLevel("qc_qcwz", MXEnums.ActionEnum.Edit.ToString()); //检查权限
                Model.wx_userweixin weixin = GetWeiXinCode();
                id = bll.GetModelList("wid=" + weixin.id)[0].Id;
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改信息成功！", "qcwzMgr.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("qc_qcwz", MXEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加信息成功！", "qcwzMgr.aspx", "Success");
            }
        }
    }
}