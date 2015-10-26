using WeiXinPF.BLL;
using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.hotel
{
    public partial class hotel_info : Web.UI.ManagePage
    {
        TextBox title;
        TextBox sortid;
        TextBox picUrl;
        TextBox picTiaozhuan;
        protected string editetype = "";
        protected static int hotelid = 0;
        BLL.wx_hotels_info hotelBll = new BLL.wx_hotels_info();

        BLL.wx_hotel_pic picBll = new BLL.wx_hotel_pic();
        Model.wx_hotel_pic pic = new Model.wx_hotel_pic();
        wx_hotel_pic iBll = new wx_hotel_pic();

        private string action;
        protected void Page_Load(object sender, EventArgs e)
        {
            hotelid = MyCommFun.RequestInt("hotelid", GetHotelId());
            action = MyCommFun.QueryString("action");
            if (!IsPostBack)
            {
                SetLocation();
                ShowInfo(hotelid);
                if (action == MXEnums.ActionEnum.View.ToString())
                {
                    this.save_hotel.Visible = false;
                }
            }
        }

        public void ShowInfo(int hotelid)
        {
            Model.wx_hotels_info hotel = hotelBll.GetModel(hotelid);
            if (hotel != null)
            {
                this.lblHotelCode.Text = hotel.HotelCode;
                this.lblHotelName.Text = hotel.hotelName;
                this.lblOperator.Text = hotel.Operator;
                this.hotelAddress.Text = hotel.hotelAddress;
                this.lblHotelPhone.Text = hotel.hotelPhone;
                this.mobilPhone.Text = hotel.mobilPhone;
                this.coverPic.Text = hotel.coverPic;
                this.topPic.Text = hotel.topPic;
                this.hotelIntroduct.Value = hotel.hotelIntroduct;
                this.txtLatXPoint.Text = hotel.xplace.ToString();
                this.txtLngYPoint.Text = hotel.yplace.ToString();
                if (hotel.xplace.HasValue && hotel.yplace.HasValue)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message",
                        "<script language='javascript'> $(\"#baiduframe\").attr(\"src\", \"../../weixin/map/qqmap/qqmap_getLocation.html?lng=" + hotel.yplace.Value.ToString() + "&lat=" + hotel.xplace.Value.ToString() + "\");</script>");
                }
            }

            IList<Model.wx_hotel_pic> itemlist = iBll.GetModelList("hotelid=" + hotelid + " order by id asc");
            if (itemlist != null && itemlist.Count > 0)
            {
                int count = itemlist.Count;


                Model.wx_hotel_pic itemEntity = new Model.wx_hotel_pic();
                for (int i = 1; i <= count; i++)
                {
                    itemEntity = itemlist[(i - 1)];
                    title = this.FindControl("title" + i) as TextBox;
                    sortid = this.FindControl("sortid" + i) as TextBox;
                    picUrl = this.FindControl("picUrl" + i) as TextBox;
                    picTiaozhuan = this.FindControl("picTiaozhuan" + i) as TextBox;

                    title.Text = itemEntity.title;
                    sortid.Text = itemEntity.sortid.ToString();
                    picUrl.Text = itemEntity.picUrl.ToString();
                    picTiaozhuan.Text = itemEntity.picTiaozhuan.ToString();

                }
            }
        }

        protected void save_hotel_Click(object sender, EventArgs e)
        {
            Model.wx_hotels_info hotel = hotelBll.GetModel(hotelid);

            //hotel.hotelName = this.hotelName.Text;
            hotel.hotelAddress = this.hotelAddress.Text;
            //hotel.hotelPhone = this.hotelPhone.Text;
            hotel.mobilPhone = this.mobilPhone.Text;
            //hotel.noticeEmail = this.noticeEmail.Text;
            hotel.coverPic = this.coverPic.Text;
            hotel.topPic = this.topPic.Text;
            //hotel.orderLimit = MyCommFun.Str2Int(this.orderLimit.Text);
            //hotel.listMode = Convert.ToBoolean(this.listMode.SelectedValue);
            //hotel.messageNotice = MyCommFun.Str2Int(this.messageNotice.Text);
            //hotel.pwd = this.pwd.Text;
            hotel.hotelIntroduct = this.hotelIntroduct.Value;
            //hotel.orderRemark = this.orderRemark.Value;
            hotel.xplace = MyCommFun.Str2Decimal(this.txtLatXPoint.Text);
            hotel.yplace = MyCommFun.Str2Decimal(this.txtLngYPoint.Text);


            hotelBll.Update(hotel);

            picBll.Deletepic(hotelid);

            for (int i = 1; i <= 6; i++)
            {
                title = this.FindControl("title" + i) as TextBox;
                sortid = this.FindControl("sortid" + i) as TextBox;
                picUrl = this.FindControl("picUrl" + i) as TextBox;
                picTiaozhuan = this.FindControl("picTiaozhuan" + i) as TextBox;

                if (title.Text.Trim() != "" && sortid.Text.Trim() != "")
                {

                    pic.hotelid = hotelid;
                    pic.title = title.Text.ToString();
                    pic.sortid = MyCommFun.Str2Int(sortid.Text.ToString());
                    pic.picUrl = picUrl.Text.ToString();
                    pic.picTiaozhuan = picTiaozhuan.Text.ToString();
                    pic.createDate = DateTime.Now;
                    picBll.Add(pic);

                }
            }
            AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改商家设置，主键为" + hotelid); //记录日志
           // JscriptMsg("修改成功！", "hotel_list.aspx", "Success");
        }

        private void SetLocation()
        {
            string location = string.Empty;
            if (action == MXEnums.ActionEnum.View.ToString())
            {
                location += "<a href = \"hotel_list.aspx\" class=\"home\"><i></i><span>商户或门店列表</span></a>";
                location += "<i class=\"arrow\"></i>";
                location += "<span>商户或门店信息查看</span>";
            }
            else
            {
                location += "<a class=\"home\"><i></i><span>商户或门店信息设置</span></a>";
            }

            divLocation.InnerHtml = location;
        }
    }
}