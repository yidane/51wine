﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using System.Text;
using System.Data;

namespace WeiXinPF.Web.admin.qiangpiao
{
    public partial class film_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected int category_id;
        BLL.wx_qp_film gbll = new BLL.wx_qp_film();
        BLL.wx_qp_base bbll = new BLL.wx_qp_base();
        protected string keywords = string.Empty;
        protected string actName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = MXRequest.GetQueryString("keywords");
            this.category_id = Utils.StrToInt(MXRequest.GetQueryString("id"), 0);
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                actName = bbll.GetModel(category_id).bName;
                RptBind(category_id, "id>0" + CombSqlTxt(keywords), "sort_id asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(int category_id, string _strWhere, string _orderby)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            this.page = MXRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            DataSet ds = gbll.GetList(category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataSource = ds;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("film_list.aspx", "id={0}&keywords={1}&page={2}",this.category_id.ToString(), this.keywords, "__id__");
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
                strTemp.Append(" and fName like  '%" + _keywords + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("wx_qp_film_page_size"), out _pagesize))
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
            Response.Redirect(Utils.CombUrlTxt("film_list.aspx", "id={0}&keywords={1}",this.category_id.ToString(), txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("wx_qp_film_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("film_list.aspx", "id={0}&keywords={1}", this.category_id.ToString(), this.keywords));
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
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "删除电影信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("film_list.aspx", "id={0}&keywords={1}", this.category_id.ToString(), this.keywords), "Success");
        }

        //根据名称查询
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("film_list.aspx", "id={0}&keywords={1}", this.category_id.ToString(), txtKeywords.Text));
        }



    }
}