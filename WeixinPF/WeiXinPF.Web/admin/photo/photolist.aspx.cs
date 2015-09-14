using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using WeiXinPF.Application.DomainModules.Common;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.Application.DomainModules.Photo.DTOS;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.photo
{
    public partial class photolist : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        BLL.wx_dzpActionInfo gbll = new BLL.wx_dzpActionInfo();
        private readonly PhotoService _service;
        protected string keywords = string.Empty;

        public photolist()
        {
            _service = new PhotoService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = MXRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                RptBind();
            }
        }

        #region 数据绑定=================================
        private void RptBind()
        {

            Model.wx_userweixin weixin = GetWeiXinCode();
            this.page = MXRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;

            //获取分页的列表
            var pagingDto = _service.GetList(this.page, this.pageSize, this.keywords, weixin.id);
            if (pagingDto != null)
            {
                if (pagingDto.list.Any())
                {
                    var nowDate = DateTime.Now;
                    var list = pagingDto.list.Select(p =>
                    {
                        //获取状态
                        var begin = DateTime.Parse(p.beginDate);
                        var end = DateTime.Parse(p.endDate);
                        if (begin > nowDate)
                        {
                            p.status_s = "<span class=\"act_before\">未开始</span>";
                        }
                        else if (end <= DateTime.Now)
                        {
                            p.status_s = "<span class=\"act_end\">已结束</span>";
                        }
                        else
                        {
                            p.status_s = "<span class=\"act_in\">进行中</span>";
                        }
                        //获取地址
                        p.url = string.Format("{0}/weixin/photo/MakePhoto.html?wid={1}&aid={2}",
                            MyCommFun.getWebSite(), p.wid, p.id);
                        return p;
                    });
                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("photolist.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
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
                strTemp.Append(" and  actName like  '%" + _keywords + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("photolist_page_size"), out _pagesize))
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
            Response.Redirect(Utils.CombUrlTxt("photolist.aspx", "keywords={0}", txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("photolist_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("photolist.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // ChkAdminLevel("manager_list", MXEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    try
                    {
                        _service.Delete(id);
                        sucCount += 1;
                    }
                    catch (Exception)
                    {

                        errorCount += 1;
                    }

                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除湖怪活动信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("photolist.aspx", "keywords={0}", this.keywords), "Success");
        }


    }
}