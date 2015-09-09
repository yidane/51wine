using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Travel.Presentation.WebPlugin
{
    public partial class OrderStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        #region 初始化控件
        private void InitControls()
        {
            //初始化日期范围
            InitDateRange();
        }

        private void InitDateRange()
        {
            var currentDate = DateTime.Now;
            this.txtbeginDate.Text = currentDate.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtendDate.Text = currentDate.ToString("yyyy-MM-dd");
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
                    //Utils.WriteCookie("wx_article_page_size", _pagesize.ToString(), 43200);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
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


            }
            catch (Exception)
            {

            }
        }

        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}