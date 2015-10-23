using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace WeiXinPF.Web.admin.hotel
{
    public partial class hotel_room : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keywords = string.Empty;
        public int hotelid = 0;

        BLL.wx_hotel_room roomBll = new BLL.wx_hotel_room();
        BLL.wx_hotel_room_manage manageBll = new BLL.wx_hotel_room_manage();

        protected string action = MXEnums.ActionEnum.Edit.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = MyCommFun.QueryString("action");
            keywords = MyCommFun.QueryString("keywords");
            hotelid = MyCommFun.RequestInt("hotelid");
            page = MXRequest.GetQueryInt("page", 1);
            pageSize = GetPageSize(10); //每页数量

            if (!Page.IsPostBack)
            {
                SetControl();
                RptBind(CombSqlTxt(keywords), "createDate desc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            _strWhere = "hotelid=" + hotelid + " " + _strWhere;

            txtKeywords.Text = this.keywords;
            DataSet ds = roomBll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr;

                ds.Tables[0].Columns.Add(new DataColumn("StatusStr", typeof(string)));
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    if (dr.Field<int>("Status") == 1)
                    {
                        dr["StatusStr"] = "<span class=\"badge sbumit\">提交审核</span>";
                    }
                    else if (dr.Field<int>("Status") == 2)
                    {
                        dr["StatusStr"] = "<span class=\"badge agree\">审核通过</span>";
                    }
                    else if (dr.Field<int>("Status") == 3)
                    {
                        dr["StatusStr"] = "<span class=\"badge refuse\">审核不通过</span>";
                    }
                }
                ds.AcceptChanges();
            }
            this.rptList.DataSource = ds;
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}&page={3}", action, hotelid.ToString(), this.keywords, "__id__");
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
                strTemp.Append(" and  roomType like  '%" + _keywords + "%' ");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("hotel_room_page_size"), out _pagesize))
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
            Response.Redirect(Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), txtKeywords.Text));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("hotel_room_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (roomBll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Delete.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords), "Success");
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgree_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.wx_hotel_room model = roomBll.GetModel(id);
                    model.Status = Model.RoomStatus.Agree;

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            roomBll.Update(model);

                            Model.wx_hotel_room_manage manageInfo = new Model.wx_hotel_room_manage();
                            manageInfo.RoomId = model.id;
                            manageInfo.Operator = GetAdminInfo().id;
                            manageInfo.OperateName = "审核通过";
                            manageInfo.OperateTime = DateTime.Now;
                            manageInfo.Comment = "通过啊啊啊啊啊啊啊";
                            manageBll.Add(manageInfo);

                            scope.Complete();
                            sucCount += 1;
                        }
                    }
                    catch(Exception ex)
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Audit.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("审核通过，成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords), "Success");
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.wx_hotel_room model = roomBll.GetModel(id);
                    model.Status = Model.RoomStatus.Refuse;

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            roomBll.Update(model);

                            Model.wx_hotel_room_manage manageInfo = new Model.wx_hotel_room_manage();
                            manageInfo.RoomId = model.id;
                            manageInfo.Operator = GetAdminInfo().id;
                            manageInfo.OperateName = "审核不通过";
                            manageInfo.OperateTime = DateTime.Now;
                            manageInfo.Comment = "不通过啊啊啊啊啊";
                            manageBll.Add(manageInfo);

                            scope.Complete();
                            sucCount += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(MXEnums.ActionEnum.Audit.ToString(), "信息" + sucCount + "条，失败" + errorCount + "条"); //记录日志

            JscriptMsg("审核不通过，成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords), "Success");
        }

        private void SetControl()
        {
            if (action == MXEnums.ActionEnum.View.ToString())
            {
                barAdd.Visible = true;
                barDelete.Visible = true;

                barAgree.Visible = false;
                barRefuse.Visible = false;
            }
            else if (action == MXEnums.ActionEnum.Audit.ToString())
            {
                barAdd.Visible = false;
                barDelete.Visible = false;

                barAgree.Visible = true;
                barRefuse.Visible = true;
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var checkbox = e.Item.FindControl("chkId") as CheckBox;
            if (checkbox != null)
            {
                int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
                Model.wx_hotel_room room = roomBll.GetModel(id);
                if (room.Status != Model.RoomStatus.Submit)
                {
                    checkbox.Enabled = false;
                }
            }
        }

        protected void rptList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            var ss = e.Item.DataItem;
        }
    }
}