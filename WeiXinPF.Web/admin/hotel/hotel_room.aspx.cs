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
using WeiXinPF.Application.DomainModules.Message;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Model;
using WeiXinPF.Model.message;
using manager_role = WeiXinPF.BLL.manager_role;

namespace WeiXinPF.Web.admin.hotel
{
    public partial class hotel_room : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected string keywords = string.Empty;
        public int hotelid = 0;
        IShortMsgService _shortMsgService = new ShortMsgService();
        BLL.wx_hotel_room roomBll = new BLL.wx_hotel_room();
        BLL.wx_hotel_room_manage manageBll = new BLL.wx_hotel_room_manage();

        protected string action = MXEnums.ActionEnum.Edit.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid", GetHotelId());

            action = MyCommFun.QueryString("action");
            keywords = MyCommFun.QueryString("keywords");
            page = MXRequest.GetQueryInt("page", 1);
            pageSize = GetPageSize(10); //每页数量

            if (!Page.IsPostBack)
            {
                SetLocation();
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
                Model.RoomStatus status = Model.RoomStatus.None;
                for (int i = 0; i < count; i++)
                {
                    dr = ds.Tables[0].Rows[i];
                    status = (Model.RoomStatus)Enum.Parse(typeof(Model.RoomStatus), dr.Field<int>("Status").ToString());

                    switch (status)
                    {
                        case Model.RoomStatus.Submit:
                            dr["StatusStr"] = "<span class=\"badge sbumit\">提交审核</span>";
                            break;
                        case Model.RoomStatus.Agree:
                            dr["StatusStr"] = "<span class=\"badge agree\">审核通过</span>";
                            break;
                        case Model.RoomStatus.Refuse:
                            dr["StatusStr"] = "<span class=\"badge refuse\">审核不通过</span>";
                            break;
                        case Model.RoomStatus.Publish:
                            dr["StatusStr"] = "<span class=\"badge publish\">已发布</span>";
                            break;
                        case Model.RoomStatus.SoldOut:
                            dr["StatusStr"] = "<span class=\"badge\">已下架</span>";
                            break;
                    }
                }
                ds.AcceptChanges();
            }
            this.rptList.DataSource = ds;
            this.rptList.DataBind();
            EnableOperate();

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
            var manager = GetAdminInfo();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //Model.wx_hotel_room model = roomBll.GetModel(id);
                    //model.Status = Model.RoomStatus.Agree;

                    try
                    {
                        manageBll.ManageRoom(id, Model.RoomStatus.Agree, manager.id, "审核通过", "");
                        //using (TransactionScope scope = new TransactionScope())
                        //{
                        //    roomBll.Update(model);

                        //    Model.wx_hotel_room_manage manageInfo = new Model.wx_hotel_room_manage();
                        //    manageInfo.RoomId = model.id;
                        //    manageInfo.Operator = GetAdminInfo().id;
                        //    manageInfo.OperateName = "审核通过";
                        //    manageInfo.OperateTime = DateTime.Now;
                        //    manageInfo.Comment = "通过啊啊啊啊啊啊啊";
                        //    manageBll.Add(manageInfo);

                        //scope.Complete();
                        sucCount += 1;



                        //发送消息：审核后发送消息
                        Model.wx_hotel_room room = roomBll.GetModel(id);
                        BLL.wx_hotel_admin dBll = new BLL.wx_hotel_admin();
                        Model.wx_hotel_admin hotelAdmin = null;
                        var users = dBll.GetModelList(String.Format(" HotelId={0}", hotelid));
                        hotelAdmin = users.FirstOrDefault();
                        if (hotelAdmin != null)
                        {
                            var wxUserweixin = GetWeiXinCode();
//                            var role = new BLL.manager_role().GetModel(manager.role_id);
                            //                            var hotelsInfo = new BLL.wx_hotels_info().GetModel(hotelid);
                            var msg = new ShortMsgDto()
                            {
                                Title = wxUserweixin.wxName,
                                Content = String.Format("编号为[{0}]的商品[{1}]已审核通过，可以发布啦！",
                        room.RoomCode, room.roomType),
                                Type = "HotelRoom",
                                DetailType = "Agree",
                                MenuType = "hotel_room",
                                IsShowButton = true,
                                ButtonText = "马上去发布",
                                ButtonUrl = String.Format(
                                    "/admin/hotel/hotel_room_info.aspx?action=View&hotelid={0}&roomid={1}",
                                hotelid, id),
                                ButtonMutipleUrl = "/admin/hotel/hotel_room.aspx?action=Edit",
                                FromUserId = manager.id.ToString(),
                                ToUserId = hotelAdmin.ManagerId.ToString(),
                                MsgToUserType = MsgUserType.Hotel,
                                MsgFromUserType = MsgUserType.Scenic
                            };
                            _shortMsgService.SendMsg(msg);
                        }
                        //}
                    }
                    catch (Exception ex)
                    {
                        errorCount += 1;
                    }
                }
            }

            //            if (sucCount > 0)
            //            {
            //                int id = Convert.ToInt32(((HiddenField)rptList.Items[0].FindControl("hidId")).Value);
            //                Model.wx_hotel_room room = roomBll.GetModel(id);
            //                //发送消息：审核后发送消息
            //
            //                BLL.wx_hotel_admin dBll = new BLL.wx_hotel_admin();
            //                Model.wx_hotel_admin hotelAdmin = null;
            //                var users = dBll.GetModelList(String.Format(" HotelId={0}", hotelid));
            //                hotelAdmin = users.FirstOrDefault();
            //                if (hotelAdmin != null)
            //                {
            //                    var hotelsInfo = new BLL.wx_hotels_info().GetModel(hotelid);
            //                    var msg = new ShortMsgDto()
            //                    {
            //                        Title = hotelsInfo.hotelName,
            //                        Content = String.Format("编号为[{0}]的[{1}]等{2}件商品已审核通过，可以发布啦！",
            //                        room.RoomCode, room.roomType, sucCount),
            //                        Type = "hotel",
            //                        IsShowButton = true,
            //                        ButtonText = "马上去发布",
            //                        ButtonUrl = "/admin/hotel/hotel_room.aspx?action=Edit",
            //                        FromUserId = manager.id,
            //                        ToUserId = hotelAdmin.ManagerId,
            //                        MsgToUserType = MsgUserType.Hotel,
            //                        MsgFromUserType = MsgUserType.Scenic
            //                    };
            //                    _shortMsgService.SendMsg(msg);
            //                }
            //            }


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
            var manager = GetAdminInfo();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //Model.wx_hotel_room model = roomBll.GetModel(id);
                    //model.Status = Model.RoomStatus.Refuse;

                    try
                    {
                        manageBll.ManageRoom(id, Model.RoomStatus.Refuse, manager.id, "审核不通过", "");
                        sucCount += 1;



                        //发送消息：审核后发送消息
                        Model.wx_hotel_room room = roomBll.GetModel(id);
                        BLL.wx_hotel_admin dBll = new BLL.wx_hotel_admin();
                        Model.wx_hotel_admin hotelAdmin = null;
                        var users = dBll.GetModelList(String.Format(" HotelId={0}", hotelid));
                        hotelAdmin = users.FirstOrDefault();
                        if (hotelAdmin != null)
                        {
                            var wxUserweixin = GetWeiXinCode();
                            //                            var role = new BLL.manager_role().GetModel(manager.role_id);
                            //                            var hotelsInfo = new BLL.wx_hotels_info().GetModel(hotelid);
                            var msg = new ShortMsgDto()
                            {
                                Title = wxUserweixin.wxName,
                                Content = String.Format("编号为[{0}]的商品[{1}]审核不通过，请修改后发布！",
                        room.RoomCode, room.roomType),
                                Type = "HotelRoom",
                                DetailType = "Refuse",
                                MenuType = "hotel_room",
                                IsShowButton = true,
                                ButtonText = "马上去修改",
                                ButtonUrl = String.Format("/admin/hotel/hotel_room_info.aspx?action=Edit&hotelid={0}&roomid={1}",
                   hotelid, id),
                                ButtonMutipleUrl = "/admin/hotel/hotel_room.aspx?action=Edit",
                               
                                FromUserId = manager.id.ToString(),
                                ToUserId = hotelAdmin.ManagerId.ToString(),
                                MsgToUserType = MsgUserType.Hotel,
                                MsgFromUserType = MsgUserType.Scenic
                            };
                            _shortMsgService.SendMsg(msg);
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

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = MyCommFun.Obj2Int(e.CommandArgument);
            switch (e.CommandName)
            {
                case "View":
                    Response.Redirect(string.Format("hotel_room_info.aspx?action={0}&hotelid={1}&roomid={2}", MXEnums.ActionEnum.View.ToString(), hotelid, id));
                    break;
                case "Edit":
                    Response.Redirect(string.Format("hotel_room_info.aspx?action={0}&hotelid={1}&roomid={2}", MXEnums.ActionEnum.Edit.ToString(), hotelid, id));
                    break;
                case "Audit":
                    Response.Redirect(string.Format("hotel_room_info.aspx?action={0}&hotelid={1}&roomid={2}", MXEnums.ActionEnum.Audit.ToString(), hotelid, id));
                    break;
                case "Publish":
                    manageBll.ManageRoom(id, Model.RoomStatus.Publish, GetAdminInfo().id, "发布", "");
                    AddAdminLog("Publish", string.Format("酒店商品【id={0}】发布。", id));

                    JscriptMsg("发布成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords), "Success");
                    break;
                case "SoldOut":
                    manageBll.ManageRoom(id, Model.RoomStatus.SoldOut, GetAdminInfo().id, "下架", "");
                    AddAdminLog("SoldOut", string.Format("酒店商品【id={0}】下架。", id));

                    JscriptMsg("下架成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}&keywords={2}", action, hotelid.ToString(), this.keywords), "Success");
                    break;
            }
        }

        private void SetControl()
        {
            if (action == MXEnums.ActionEnum.Edit.ToString())
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

                divTips.Visible = false;
            }
        }

        private void EnableOperate()
        {
            for (int index = 0; index < rptList.Items.Count; index++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[index].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[index].FindControl("chkId");
                //LinkButton lbtView = rptList.Items[index].FindControl("lbtView") as LinkButton;
                LinkButton lbtEidt = rptList.Items[index].FindControl("lbtEdit") as LinkButton;
                LinkButton lbtAudit = rptList.Items[index].FindControl("lbtAudit") as LinkButton;
                LinkButton lbtPublish = rptList.Items[index].FindControl("lbtPublish") as LinkButton;
                LinkButton lbtSoldOut = rptList.Items[index].FindControl("lbtSoldOut") as LinkButton;
                Model.wx_hotel_room room = roomBll.GetModel(id);

                lbtPublish.Visible = false;
                lbtSoldOut.Visible = false;
                lbtAudit.Visible = false;
                lbtEidt.Visible = false;
                cb.Enabled = false;
                switch (room.Status)
                {
                    case Model.RoomStatus.Submit:
                        if (action == MXEnums.ActionEnum.Audit.ToString())
                        {
                            cb.Enabled = true;
                            lbtAudit.Visible = true;
                        }
                        else if (action == MXEnums.ActionEnum.Edit.ToString())
                        {
                            lbtEidt.Visible = true;
                        }
                        break;
                    case Model.RoomStatus.Agree:
                        if (action == MXEnums.ActionEnum.Edit.ToString())
                        {
                            lbtPublish.Visible = true;
                        }
                        break;
                    case Model.RoomStatus.Refuse:
                        if (action == MXEnums.ActionEnum.Edit.ToString())
                        {
                            lbtEidt.Visible = true;
                        }
                        break;
                    case Model.RoomStatus.Publish:
                        if (action == MXEnums.ActionEnum.Edit.ToString())
                        {
                            lbtSoldOut.Visible = true;
                        }
                        break;
                    case Model.RoomStatus.SoldOut:
                        break;
                }
            }
        }

        private void SetLocation()
        {
            string location = string.Empty;
            if (GetHotelId() == 0)
            {

                location += "<a href = \"hotel_list.aspx\" class=\"home\"><i></i><span>商户或门店列表</span></a>";
                location += "<i class=\"arrow\"></i>";
                location += "<span>商品信息审核</span>";
            }
            else
            {
                location += "<a class=\"home\"><i></i><span>商品管理</span></a>";
            }

            divLocation.InnerHtml = location;
        }
    }
}