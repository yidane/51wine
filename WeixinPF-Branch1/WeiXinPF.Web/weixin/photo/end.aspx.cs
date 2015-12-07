using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Application.DomainModules.Photo;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.photo
{
    public partial class end :WeiXinPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OnlyWeiXinLook();

            if (!IsPostBack)
            {
//                int id = MyCommFun.RequestInt("aid");
//                if (id == 0)
//                {
//                    return;
//                }
//                var aBll = new PhotoService();
//                var action = aBll.GetModel(id);
//                if (action == null)
//                {
//                    return;
//                }
//                litEndNotice.Text = "";
            }
        }
    }
}