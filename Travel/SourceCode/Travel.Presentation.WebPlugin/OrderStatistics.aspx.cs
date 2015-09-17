using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Presentation.WebPlugin.Order;

namespace Travel.Presentation.WebPlugin
{
    public partial class OrderStatistics : System.Web.UI.Page
    {
        protected int m_PageIndex;
        protected int m_PageSize;
        protected string m_TicketCategoryId;
        protected string m_TicketStatusId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();

                var searchParameter = CreateSearchParameter();
                searchParameter.PageIndex = 0;
                searchParameter.PageSize = 10;

                txtPageNum.Text = "10";
                Search(searchParameter);
            }
        }

        #region 初始化控件
        private void InitControls()
        {
            //初始化参数
            InitQueryString();

            //初始化日期范围
            InitDateRange();

            //初始化票类型
            InitTicketCategory();

            //初始化票状态
            InitTicketStatus();
        }

        private void InitQueryString()
        {
            m_TicketCategoryId = Request.QueryString["TicketCategoryID"];
            m_TicketStatusId = Request.QueryString["TicketStatus"];
        }

        private void InitDateRange()
        {
            var currentDate = DateTime.Now;
            this.txtbeginDate.Text = currentDate.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtendDate.Text = currentDate.ToString("yyyy-MM-dd");
        }

        private void InitTicketCategory()
        {
            var result = new OrderStatisticsManager().GetTicketCategoryEntityList();
            this.ddlTicketCategory.DataTextField = "TicketName";
            this.ddlTicketCategory.DataValueField = "TicketCategoryID";
            this.ddlTicketCategory.DataSource = result;
            this.ddlTicketCategory.DataBind();
        }

        private void InitTicketStatus()
        {
            var result = new OrderStatisticsManager().GetTicketStatusEntityList();
            this.ddlCategoryStatus.DataTextField = "StatusDesc";
            this.ddlCategoryStatus.DataValueField = "StatusCode";
            this.ddlCategoryStatus.DataSource = result;
            this.ddlCategoryStatus.DataBind();
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
                    m_PageSize = _pagesize;
                    m_PageIndex = 0;
                    var searchParameter = CreateSearchParameter();
                    searchParameter.PageSize = m_PageSize;
                    Search(searchParameter);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchParameter = CreateSearchParameter();
                searchParameter.PageIndex = 0;
                searchParameter.PageSize = 10;
                Search(searchParameter);
            }
            catch (Exception)
            {

            }
        }

        private SearchParameter CreateSearchParameter()
        {
            var rtnSearchParameter = new SearchParameter();
            try
            {
                var beginDate = new DateTime(1900, 1, 1);
                var endDate = new DateTime(1900, 1, 1);
                var minAmount = 0;
                var maxAmount = 0;

                if (!string.IsNullOrEmpty(this.txtMaxAmount.Text))
                    int.TryParse(this.txtMaxAmount.Text.Trim(), out maxAmount);

                if (!string.IsNullOrEmpty(this.txtMinAmount.Text))
                    int.TryParse(this.txtMinAmount.Text.Trim(), out minAmount);

                if (!string.IsNullOrEmpty(this.txtbeginDate.Text))
                    DateTime.TryParse(this.txtbeginDate.Text.Trim(), out beginDate);

                if (!string.IsNullOrEmpty(this.txtendDate.Text))
                    DateTime.TryParse(this.txtendDate.Text.Trim(), out endDate);

                if (this.ddlTicketCategory.SelectedIndex != 0)
                    rtnSearchParameter.TicketCategoryID = new Guid(this.ddlTicketCategory.SelectedValue);

                if (this.ddlCategoryStatus.SelectedIndex != 0)
                    rtnSearchParameter.OrderStatus = ddlCategoryStatus.SelectedValue;

                rtnSearchParameter.BeginDate = beginDate;
                rtnSearchParameter.BeginTotalPrice = minAmount;
                rtnSearchParameter.EndDate = endDate;
                rtnSearchParameter.EndTotalPrice = maxAmount;

                return rtnSearchParameter;
            }
            catch (Exception)
            {
                rtnSearchParameter.PageSize = 10;
                rtnSearchParameter.PageIndex = 0;

                return rtnSearchParameter;
            }
        }

        private void Search(SearchParameter searchParameter)
        {
            var result = new OrderStatisticsManager().Search(searchParameter);
            this.rptList.DataSource = result.OrderList;
            this.rptList.DataBind();

            string pageUrl = string.Format("OrderStatistics.aspx?ticketName={0}&ticketStatus={1}&page={2}", searchParameter.TicketCategoryID.ToString(), searchParameter.OrderStatus, this.m_PageIndex);
            PageContent.InnerHtml = OutPageList(this.m_PageSize, this.m_PageIndex, result.Total, pageUrl, 8);
        }

        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1)
            {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }

        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.Contains("&") && _url.LastIndexOf("&") < (_url.Length - 1))
                {
                    _url += "&";
                }
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }
    }
}