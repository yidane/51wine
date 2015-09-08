using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.wqiche
{
    public partial class yuyueMgr : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize; 
        protected string keywords = "";
        BLL.wx_wq_yuyue bll = new BLL.wx_wq_yuyue();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = MXRequest.GetQueryString("keywords");
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                RptBind(CombSqlTxt(keywords), "sort_id asc,id asc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            _strWhere = "wId=" + weixin.id + " " + _strWhere;
            this.page = MXRequest.GetQueryInt("page", 1); 
            ckType();//根据类型限制可以添加的预约设置
            this.rptList.DataSource = bll.GetList(this.pageSize, page, _strWhere, _orderby, out totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("yuyueMgr.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }

        void ckType()
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            IList<Model.wx_wq_yuyue> yyList = bll.GetModelList(" wid=" + weixin.id);
            if (yyList != null)
            {
                int len = yyList.Count;
                if (len == 0)
                {
                    this.litType.Text = "<li><a class=\"add\" href=\"yuyue_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&type=2\"><i></i><span>新增预约试驾</span></a></li>" +
                        "<li><a class=\"add\" href=\"yuyue_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&type=1\"><i></i><span>新增预约保养</span></a></li>";
                }
                else if (len == 2)
                {
                    this.litType.Text = "";
                }
                else if (len == 1)
                {
                    Model.wx_wq_yuyue yyModel = yyList[0];
                    if (yyModel.type == "1")//预约保养
                    {
                        this.litType.Text = "<li><a class=\"add\" href=\"yuyue_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&type=2\"><i></i><span>新增预约试驾</span></a></li>";
                    }
                    else//预约试驾
                    {
                        this.litType.Text = "<li><a class=\"add\" href=\"yuyue_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&type=1\"><i></i><span>新增预约保养</span></a></li>";
                    }
                }
            }

        }

        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();

            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and Name like '%" + _keywords + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("yuyueMgr_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion 

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("yuyueMgr_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("yuyueMgr.aspx", "keywords={0}", this.keywords));
        }  
    }
}