using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.scenic
{
    public partial class scenic_picture_list : Web.UI.ManagePage
    {

        protected int TotalCount;
        protected int CurrentPage;
        protected int PageSize;
        protected int DetailId = 0;  //微相册主键id
        BLL.wx_travel_picture _bll = new BLL.wx_travel_picture();
        protected void Page_Load(object sender, EventArgs e)
        {
            DetailId = MyCommFun.RequestInt("detailId");

            this.PageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                RptBind(string.Empty, "OrderNo asc,CreateDate desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string strWhere, string orderBy)
        {
            strWhere = "detailId=" + DetailId + " " + strWhere;
            this.CurrentPage = MXRequest.GetQueryInt("page", 1);
            DataSet ds = _bll.GetPageList(this.PageSize, this.CurrentPage, strWhere, orderBy, out this.TotalCount);

            this.rptList2.DataSource = ds;
            this.rptList2.DataBind();

            //绑定页码
            txtPageNum.Text = this.PageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("scenic_picture_list.aspx", "detailId={0}&page={1}", DetailId.ToString(), "__id__");
            PageContent.InnerHtml = Utils.OutPageList(PageSize, CurrentPage, TotalCount, pageUrl, 8);
        }
        #endregion

        #region 返回每页数量=============================
        private static int GetPageSize(int defaultSize)
        {
            int pagesize;
            if (int.TryParse(Utils.GetCookie("picturelist_page_size"), out pagesize))
            {
                if (pagesize > 0)
                {
                    return pagesize;
                }
            }
            return defaultSize;
        }
        #endregion


        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out pagesize))
            {
                if (pagesize > 0)
                {
                    Utils.WriteCookie("photolist_page_size", pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("scenic_picture_list.aspx", "detailId={0}", DetailId.ToString()));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList2.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList2.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList2.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (_bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除景点-图片信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("scenic_picture_list.aspx", "detailId={0}", DetailId.ToString()), "Success");
        }
    }
}