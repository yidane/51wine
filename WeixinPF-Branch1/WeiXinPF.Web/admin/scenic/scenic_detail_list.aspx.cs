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
    public partial class scenic_detail_list : Web.UI.ManagePage
    {
        protected int TotalCount;
        protected int CurrentPage;
        protected int PageSize;
        protected int ScenicId;

        private const string PageSizeKey = "scenic_list_page_size";
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSize = GetPageSize(10);
            ScenicId = MXRequest.GetQueryInt("scenicId");
            if (!Page.IsPostBack)
            {
                RptBind(string.Format("id>0 And scenicId={0}",ScenicId), "Id Desc");
            }
        }

        private void RptBind(string strWhere, string orderby)
        {
            this.CurrentPage = MXRequest.GetQueryInt("page", 1);

            //列表显示
            BLL.wx_travel_scenicDetail bll = new BLL.wx_travel_scenicDetail();
            DataSet dsData = bll.GetPageList(PageSize, this.CurrentPage, strWhere, orderby, out TotalCount);
            this.rptList.DataSource = dsData;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.PageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("scenic_deatil_list.aspx", "scenicId={0}&page={1}", ScenicId.ToString(), "__id__");
            PageContent.InnerHtml = Utils.OutPageList(PageSize, CurrentPage, TotalCount, pageUrl, 8);
        }

        #region 设置列表每页得数量
        private static int GetPageSize(int defaultSize)
        {
            int pagesize;
            if (int.TryParse(Utils.GetCookie(PageSizeKey), out pagesize))
            {
                if (pagesize > 0)
                {
                    return pagesize;
                }
            }
            return defaultSize;
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out pagesize))
            {
                if (pagesize > 0)
                {
                    Utils.WriteCookie(PageSizeKey, pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("scenic_detail_list.aspx", "scenicId={0}", ScenicId.ToString()));
        }
        #endregion
       
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;
            BLL.wx_travel_scenicDetail bll = new BLL.wx_travel_scenicDetail();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除景点详情成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("scenic_detail_list.aspx", "scenicId={0}", ScenicId.ToString()), "Success", "parent.loadMenuTree");
        }
    }
}