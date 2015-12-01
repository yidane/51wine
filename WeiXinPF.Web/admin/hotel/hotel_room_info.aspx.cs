using WeiXinPF.BLL;
using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using WeiXinPF.Application.DomainModules.Message;
using WeiXinPF.Application.DomainModules.Message.Dtos;
using WeiXinPF.Model.message;
using wx_hotels_info = WeiXinPF.Model.wx_hotels_info;
using wx_userweixin = WeiXinPF.Model.wx_userweixin;

namespace WeiXinPF.Web.admin.hotel
{
    public partial class hotel_room_info : Web.UI.ManagePage
    {
        TextBox title;
        TextBox sortpicid;
        TextBox roomPic;
        TextBox roomPictz;

        protected static int roomid = 0;
        protected int hotelid = 0;
        BLL.wx_hotel_room roomBll = new BLL.wx_hotel_room();
        IShortMsgService _shortMsgService = new ShortMsgService();

        BLL.wx_hotel_roompic picBll = new BLL.wx_hotel_roompic();
        Model.wx_hotel_roompic pic = new Model.wx_hotel_roompic();
        wx_hotel_roompic iBll = new wx_hotel_roompic();

        private string action = MXEnums.ActionEnum.Add.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid");
            roomid = MyCommFun.RequestInt("roomid");
            action = MyCommFun.QueryString("action");

            if (!IsPostBack)
            {
                SetLocation();
                SetControl();
                if (action == MXEnums.ActionEnum.Edit.ToString()
                    || action == MXEnums.ActionEnum.Audit.ToString()
                    || action == MXEnums.ActionEnum.View.ToString())
                {
                    ShowInfo(roomid);
                }
            }
        }

        public void ShowInfo(int roomid)
        {
            Model.wx_hotel_room room = roomBll.GetModel(roomid);
            if (room != null)
            {
                this.lblRoomCode.Text = room.RoomCode;
                this.roomType.Text = room.roomType;
                this.indroduce.InnerText = room.indroduce;
                this.roomPrice.Text = room.roomPrice.ToString();
                this.salePrice.Text = room.salePrice.ToString();
                this.facilities.Value = room.facilities;
                this.txtUsueIntroduction.Value = room.UseInstruction;
                this.txtRefundRule.Value = room.RefundRule;

                //this.txtExpiryDate_Begin.Text = room.ExpiryDate_Begin.HasValue ? room.ExpiryDate_Begin.Value.ToString("yyyy-MM-dd") : "";
                //this.txtExpiryDate_End.Text = room.ExpiryDate_End.HasValue ? room.ExpiryDate_End.Value.ToString("yyyy-MM-dd") : "";
                if (room.Status != Model.RoomStatus.Submit)
                {
                    //获取审核意见
                    var manageBll = new BLL.wx_hotel_room_manage();
                    txtComment.Text = manageBll.GetComment(roomid);
                }
            }

            IList<Model.wx_hotel_roompic> itemlist = iBll.GetModelList("roomid=" + roomid + " order by id asc");
            if (itemlist != null && itemlist.Count > 0)
            {
                int count = itemlist.Count;


                Model.wx_hotel_roompic itemEntity = new Model.wx_hotel_roompic();
                for (int i = 1; i <= count; i++)
                {
                    itemEntity = itemlist[(i - 1)];
                    title = this.FindControl("title" + i) as TextBox;
                    sortpicid = this.FindControl("sortpicid" + i) as TextBox;
                    roomPic = this.FindControl("roomPic" + i) as TextBox;
                    roomPictz = this.FindControl("roomPictz" + i) as TextBox;

                    title.Text = itemEntity.title;
                    sortpicid.Text = itemEntity.sortpicid.ToString();
                    roomPic.Text = itemEntity.roomPic.ToString();
                    roomPictz.Text = itemEntity.roomPictz.ToString();

                }

            }
        }

        protected void save_room_Click(object sender, EventArgs e)
        {
            Model.wx_hotel_room room = new Model.wx_hotel_room();
            if (action == MXEnums.ActionEnum.Add.ToString())
            {
                room.hotelid = hotelid;
                room.roomType = this.roomType.Text;
                room.RoomCode = roomBll.GetRoomCode(hotelid);
                room.indroduce = this.indroduce.InnerText;
                room.roomPrice = Convert.ToDecimal(this.roomPrice.Text);
                room.salePrice = Convert.ToDecimal(this.salePrice.Text);
                room.facilities = this.facilities.Value;
                room.UseInstruction = txtUsueIntroduction.Value;
                room.RefundRule = this.txtRefundRule.Value;
                room.createDate = DateTime.Now;
                room.Status = Model.RoomStatus.Submit;

                //room.ExpiryDate_Begin = DateTime.Parse(txtExpiryDate_Begin.Text);
                //room.ExpiryDate_End = DateTime.Parse(txtExpiryDate_End.Text);

                //if (DateTime.Compare(room.ExpiryDate_Begin.Value, room.ExpiryDate_End.Value) > 0)
                //{
                //    JscriptMsg("使用有效期开始时间不能大于结束时间。", "", "error");
                //}

                int id = roomBll.Add(room);


                for (int i = 1; i <= 6; i++)
                {
                    title = this.FindControl("title" + i) as TextBox;
                    sortpicid = this.FindControl("sortpicid" + i) as TextBox;
                    roomPic = this.FindControl("roomPic" + i) as TextBox;
                    roomPictz = this.FindControl("roomPictz" + i) as TextBox;

                    if (title.Text.Trim() != "" && sortpicid.Text.Trim() != "")
                    {

                        pic.roomid = id;
                        pic.title = title.Text.ToString();
                        pic.sortpicid = MyCommFun.Str2Int(sortpicid.Text.ToString());
                        pic.roomPic = roomPic.Text.ToString();
                        pic.roomPictz = roomPictz.Text.ToString();
                        pic.createDate = DateTime.Now;
                        pic.hotelid = hotelid;
                        picBll.Add(pic);

                    }
                }
                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加房间类型，主键为" + id); //记录日志
                JscriptMsg("添加成功！", "hotel_room.aspx?action=" + MXEnums.ActionEnum.Edit.ToString() + "&hotelid=" + hotelid + "", "Success");

                //发送消息：提交审核

                var scenicUser = GetWeiXinCode();
                if (scenicUser != null)
                {
                    var manager = GetAdminInfo();
                    var hotelsInfo = new BLL.wx_hotels_info().GetModel(hotelid);

                    var msg = new ShortMsgDto()
                    {
                        Title = hotelsInfo.hotelName,
                        Content = String.Format("编号为[{0}]的商品[{1}]请您审核！", room.RoomCode, room.roomType),
                        Type = "HotelRoom",
                        MenuType = "wHotel",
                        IsShowButton = true,
                        ButtonText = "马上去审核",
                        ButtonUrl = String.Format("/admin/hotel/hotel_room_info.aspx?action=Audit&hotelid={0}&roomid={1}",
                   hotelid, id),
                        ButtonMutipleUrl = String.Format("/admin/hotel/hotel_room.aspx?hotelid={0}&action=Audit", hotelid),
                        FromUserId = manager.id.ToString(),
                        ToUserId = scenicUser.uId.ToString(),
                        MsgToUserType = MsgUserType.Scenic,
                        MsgFromUserType = MsgUserType.Hotel
                    };
                    _shortMsgService.SendMsg(msg);
                }

            }

            else if (action == MXEnums.ActionEnum.Edit.ToString())
            {
                if (roomid == 0)
                {
                    return;
                }

                room = roomBll.GetModel(roomid);

                room.hotelid = hotelid;
                room.roomType = this.roomType.Text;
                room.indroduce = this.indroduce.InnerText;
                room.roomPrice = Convert.ToDecimal(this.roomPrice.Text);
                room.salePrice = Convert.ToDecimal(this.salePrice.Text);
                room.facilities = this.facilities.Value;
                room.Status = Model.RoomStatus.Submit;

                //room.ExpiryDate_Begin = DateTime.Parse(txtExpiryDate_Begin.Text);
                //room.ExpiryDate_End = DateTime.Parse(txtExpiryDate_End.Text);

                //if (DateTime.Compare(room.ExpiryDate_Begin.Value, room.ExpiryDate_End.Value) > 0)
                //{
                //    JscriptMsg("使用有效期开始时间不能大于结束时间。", "", "error");
                //}

                roomBll.Update(room);

                picBll.Deletepic(roomid);

                for (int i = 1; i <= 6; i++)
                {
                    title = this.FindControl("title" + i) as TextBox;
                    sortpicid = this.FindControl("sortpicid" + i) as TextBox;
                    roomPic = this.FindControl("roomPic" + i) as TextBox;
                    roomPictz = this.FindControl("roomPictz" + i) as TextBox;

                    if (title.Text.Trim() != "" && sortpicid.Text.Trim() != "")
                    {
                        pic.hotelid = hotelid;
                        pic.roomid = roomid;
                        pic.title = title.Text.ToString();
                        pic.sortpicid = MyCommFun.Str2Int(sortpicid.Text.ToString());
                        pic.roomPic = roomPic.Text.ToString();
                        pic.roomPictz = roomPictz.Text.ToString();
                        pic.createDate = DateTime.Now;
                        picBll.Add(pic);

                    }
                }
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改房间类型设置，主键为" + hotelid); //记录日志
                JscriptMsg("修改成功！", "hotel_room.aspx?action=" + MXEnums.ActionEnum.Edit.ToString() + "&hotelid=" + hotelid + "", "Success");
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            try
            {
                var manager = GetAdminInfo();
                new BLL.wx_hotel_room_manage().ManageRoom(roomid, Model.RoomStatus.Agree, manager.id, "审核通过", txtComment.Text.Trim());

                //发送消息：审核通过
                BLL.wx_hotel_admin dBll = new BLL.wx_hotel_admin();
                Model.wx_hotel_admin hotelAdmin = null;
                var users = dBll.GetModelList(String.Format(" HotelId={0}", hotelid));
                hotelAdmin = users.FirstOrDefault();
                if (hotelAdmin != null)
                {
                    var wxUserweixin = GetWeiXinCode();
                    //                    var role = new BLL.manager_role().GetModel(manager.role_id);
                    //                    var hotelsInfo = new BLL.wx_hotels_info().GetModel(hotelid);
                    var msg = new ShortMsgDto()
                    {
                        Title = wxUserweixin.wxName,
                        Content = String.Format("编号为[{0}]的商品[{1}]已审核通过，可以发布啦！",
                        this.lblRoomCode.Text, this.roomType.Text),
                        Type = "HotelRoom",
                        MenuType = "hotel_room",
                        IsShowButton = true,
                        ButtonText = "马上去发布",
                        ButtonUrl = String.Format("/admin/hotel/hotel_room_info.aspx?action=View&hotelid={0}&roomid={1}",
                   hotelid, roomid),
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
                JscriptMsg("操作失败！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Audit.ToString(), hotelid.ToString()), "Error");
            }
            AddAdminLog(MXEnums.ActionEnum.Audit.ToString(), string.Format("房间【id={0}】审核通过。", roomid)); //记录日志

            JscriptMsg("操作成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Audit.ToString(), hotelid.ToString()), "Success");
        }

        protected void btnRefuse_Click(object sender, EventArgs e)
        {
            try
            {
                new BLL.wx_hotel_room_manage().ManageRoom(roomid, Model.RoomStatus.Refuse, GetAdminInfo().id, "审核不通过", txtComment.Text.Trim());
            }
            catch (Exception ex)
            {
                JscriptMsg("操作失败！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", action, hotelid.ToString()), "Error");
            }
            AddAdminLog(MXEnums.ActionEnum.Audit.ToString(), string.Format("房间【id={0}】审核不通过。", roomid)); //记录日志

            JscriptMsg("操作成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", action, hotelid.ToString()), "Success");
        }

        private void SetControl()
        {
            save_room.Visible = false;
            btnAgree.Visible = false;
            btnRefuse.Visible = false;

            btnPublish.Visible = false;

            lblComment.Visible = false;
            txtComment.Visible = false;

            if (action == MXEnums.ActionEnum.Audit.ToString())
            {
                lblComment.Visible = true;
                txtComment.Visible = true;
                btnAgree.Visible = true;
                btnRefuse.Visible = true;
            }

            if (action == MXEnums.ActionEnum.Add.ToString() || action == MXEnums.ActionEnum.Edit.ToString())
            {
                save_room.Visible = true;
            }

            if (roomid != 0)
            {
                Model.wx_hotel_room room = roomBll.GetModel(roomid);
                if (room.Status != Model.RoomStatus.Submit)
                {
                    lblComment.Visible = true;
                    txtComment.Visible = true;
                    txtComment.Enabled = false;
                }

                if (room.Status == Model.RoomStatus.Agree&& GetHotelId()!=0)
                {
                    btnPublish.Visible = true;
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
                location += "<a href = \"hotel_room.aspx?action=" + MXEnums.ActionEnum.Audit.ToString() + "&hotelid=" + hotelid + "\" ><i></i><span>商品信息审核</span></a>";
                location += "<i class=\"arrow\"></i>";
                location += "<span>商品信息</span>";
            }
            else
            {
                location += "<a class=\"home\" href = \"hotel_room.aspx?action=" + MXEnums.ActionEnum.Edit.ToString() + "&hotelid=" + hotelid + "\" ><i></i><span>商品管理</span></a>";
                location += "<i class=\"arrow\"></i>";
                location += "<span>商品信息</span>";
            }

            divLocation.InnerHtml = location;
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            try
            {
                new BLL.wx_hotel_room_manage().ManageRoom(roomid, Model.RoomStatus.Publish, GetAdminInfo().id, "发布", "");
                JscriptMsg("发布成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Edit.ToString(), hotelid.ToString()), "Success");
            }
            catch
            {
                JscriptMsg("发布失败！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Edit.ToString(), hotelid.ToString()), "Error");
            }
        }
    }
}