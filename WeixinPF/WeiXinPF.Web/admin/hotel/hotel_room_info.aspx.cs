using WeiXinPF.BLL;
using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

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
                SetControl();
                if (action == MXEnums.ActionEnum.Edit.ToString() || action == MXEnums.ActionEnum.Audit.ToString())
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
                room.indroduce = this.indroduce.InnerText;
                room.roomPrice = Convert.ToDecimal(this.roomPrice.Text);
                room.salePrice = Convert.ToDecimal(this.salePrice.Text);
                room.facilities = this.facilities.Value;
                room.createDate = DateTime.Now;
                room.Status = Model.RoomStatus.Submit;
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
                JscriptMsg("添加成功！", "hotel_room.aspx?action="+MXEnums.ActionEnum.Edit.ToString()+"&hotelid=" + hotelid + "", "Success");
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
                //room.sortid = Convert.ToInt32(this.sortid.Text);

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
                new BLL.wx_hotel_room_manage().ManageRoom(roomid, Model.RoomStatus.Agree, GetAdminInfo().id, "审核通过");
               
            }
            catch (Exception ex)
            {
                JscriptMsg("操作失败！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Audit.ToString(), hotelid.ToString()), "Error");
            }
            AddAdminLog(MXEnums.ActionEnum.Audit.ToString(), string.Format("房间【id={0}】审核通过。",roomid)); //记录日志

            JscriptMsg("操作成功！", Utils.CombUrlTxt("hotel_room.aspx", "action={0}&hotelid={1}", MXEnums.ActionEnum.Audit.ToString(), hotelid.ToString()), "Success");
        }

        protected void btnRefuse_Click(object sender, EventArgs e)
        {
          
            try
            {
                new BLL.wx_hotel_room_manage().ManageRoom(roomid, Model.RoomStatus.Refuse, GetAdminInfo().id, "审核不通过");
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
            if (action == MXEnums.ActionEnum.Audit.ToString())
            {
                save_room.Visible = false;

                btnAgree.Visible = true;
                btnRefuse.Visible = true;
            }

            if (action == MXEnums.ActionEnum.Add.ToString() || action == MXEnums.ActionEnum.Edit.ToString())
            {
                save_room.Visible = true;

                btnAgree.Visible = false;
                btnRefuse.Visible = false;
            }
        }
    }
}