using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using ThoughtWorks.QRCode.Codec;

namespace WeiXinPF.Web.weixin.zhanhui
{
    public partial class gzReg_ret : WeiXinPage
    {
        protected int id;
        protected string name;
        protected string pampany_name;
        protected string email;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.wx_zh_gz gzBll = new BLL.wx_zh_gz();
                id = MyCommFun.RequestInt("gzid");
                Model.wx_zh_gz gzModel = gzBll.GetModel(id);
                if (gzModel != null)
                {
                    name = gzModel.linkman;
                    email = gzModel.email;
                    pampany_name = gzModel.company_name; 
                }

            }
        }
         

    }
}