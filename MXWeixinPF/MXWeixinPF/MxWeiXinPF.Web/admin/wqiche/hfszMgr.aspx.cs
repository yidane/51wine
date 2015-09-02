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
    public partial class hfszMgr : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型 
        private int id = 0;
        BLL.wx_requestRule rBll = new BLL.wx_requestRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("qc_hfsz", MXEnums.ActionEnum.View.ToString()); //检查权限
                Model.wx_userweixin weixin = GetWeiXinCode(); 
                ShowInfo(this.id);
            }

        }
        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            Model.wx_userweixin weixin = GetWeiXinCode(); 
            this.lblCxxs.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "cxXinshang.aspx?wid=" + weixin.id;
            this.lblCzgh.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "czGuanhuai.aspx?wid=" + weixin.id;
            this.lblJxcx.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "ppList.aspx?wid=" + weixin.id;
            this.lblSygj.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "syGongju.aspx?wid=" + weixin.id;
            this.lblXsgw.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "xiaoshouMgr.aspx?wid=" + weixin.id;
            this.lblZxyy.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "yyBaoyang.aspx?wid=" + weixin.id + "&type=2";
            this.lblQjkc.Text = MyCommFun.getWebSite() + "/weixin/wqiche/" + "qjtList.aspx?wid=" + weixin.id;

        }
        #endregion



    }
}