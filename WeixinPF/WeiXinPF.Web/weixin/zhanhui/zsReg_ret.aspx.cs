using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.zhanhui
{
    public partial class zsReg_ret : WeiXinPage
    {
        protected int id;
        protected string name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.wx_zh_zs zsBll = new BLL.wx_zh_zs();
                id = MyCommFun.RequestInt("zsid");
                Model.wx_zh_zs zsModel = zsBll.GetModel(id);
                if (zsModel != null)
                    name = zsModel.linkman;
            }
        }
    }
}