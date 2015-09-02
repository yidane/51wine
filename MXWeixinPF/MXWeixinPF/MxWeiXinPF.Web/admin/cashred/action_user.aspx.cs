/**************************************
 *
 * Author:李朴
 * Compan~y:沐*雪
 * ｑｑ:２３００２８０７
 * createDate:2015-8-2
 * update:2015-8-2
 * 
 ***********************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using System.Text;
using System.Data;
using MxWeiXinPF.BLL;

namespace MxWeiXinPF.Web.admin.cashred
{
    //ｑｑ:２３００２８０７
    public partial class action_user : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected int actid = 0;
        BLL.wx_xjhongbao_lqinfo gbll = new BLL.wx_xjhongbao_lqinfo();
        wx_xjhongbao_action xjactBll = new wx_xjhongbao_action();
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = MXRequest.GetQueryString("keywords");
            actid = MyCommFun.RequestInt("id");
            Model.wx_userweixin weixin = GetWeiXinCode();
            if (!xjactBll.Exists(actid, weixin.id))
            {
                JscriptMsg("记录不存在或已被删除！", "back", "Error");
                return;
            }
            Model.wx_xjhongbao_action actModel = xjactBll.GetModel(actid);
            lblActName.Text = actModel.act_name;

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {

                RptBind(actid, CombSqlTxt(keywords), "Send_time desc,id desc");
            }
        }

        #region 数据绑定=================================
        /// <summary>
        /// 现金红包--获得用户
        /// datetime:2015-8-2
        /// auth:li pu
        /// </summary>
        /// <param name="_strWhere"></param>
        /// <param name="_orderby"></param>
        private void RptBind(int actid,string _strWhere, string _orderby)
        {

            Model.wx_userweixin weixin = GetWeiXinCode();
            _strWhere = "actionId=" + actid + " " + _strWhere;
            this.page = MXRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            DataSet ds = gbll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
             
            this.rptList.DataSource = ds;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("action_user.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and  ( openid like  '%" + _keywords + "%' or userNick like  '%" + _keywords + "%') ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("action_user_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("action_user.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("action_user_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("action_user.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限 李~朴
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (gbll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除用户信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("action_user.aspx", "keywords={0}", this.keywords), "Success");
        }

        protected string hdTypeStr(object obj)
        {
            string ret = "";
            if (obj == null)
            {
                return "";
            }
            switch (obj.ToString())
            {
                case "0":
                    ret = "关注时红包";
                    break;
                case "1":
                    ret = "关键词红包";
                    break;
                case "2":
                    ret = "网页红包";
                    break;

            }
            return ret;
        }
        public string jine(object m)
        {
            string ret = "";
            if (m == null)
            {
                return "";
            }
            int money = MyCommFun.Obj2Int(m);
            ret = (money / 100.0).ToString();
            return ret;

        }
    }
}