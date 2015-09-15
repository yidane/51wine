using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.scenic
{
    public partial class detail : System.Web.UI.Page
    {
        protected int Id;
        protected Model.wx_travel_scenicDetail Detail;

        protected void Page_Load(object sender, EventArgs e)
        {
            Id = MyCommFun.RequestInt("id");
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        private void ShowInfo()
        {
            BLL.wx_travel_scenicDetail dBll = new BLL.wx_travel_scenicDetail();
            BLL.wx_travel_picture pBll = new BLL.wx_travel_picture();

            Detail = dBll.GetModel(Id);
            if (Detail != null)
            {
                divContent.InnerHtml = Detail.Content;
                var pictures = pBll.GetModelList("DetailID=" + Detail.Id);

                rpList.DataSource = pictures;
                rpList.DataBind();
            }
        }
    }
}