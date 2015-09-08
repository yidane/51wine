using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class cxXinshang : WeiXinPage
    {
        protected int wid;
        protected string openid;

        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MyCommFun.RequestOpenid();
            if (!IsPostBack)
            {
                BLL.wx_wq_wzlx wzBll = new BLL.wx_wq_wzlx();
                BLL.wx_albums_info aiBll = new BLL.wx_albums_info();
                BLL.wx_albums_photo apBll = new BLL.wx_albums_photo();
                if (wzBll.GetRecordCount("wid=" + wid) > 0)
                {
                    Model.wx_wq_wzlx wzModel = wzBll.GetModelList("wid=" + wid)[0];
                    Model.wx_albums_info aiModel = aiBll.GetModel(MyCommFun.Obj2Int(wzModel.pxcid));
                    this.Title = aiModel.aName;
                    this.rptList.DataSource = apBll.GetList(" aid=" + aiModel.id + " and wid=" + wid);
                    this.rptList.DataBind();
                }
                
            }
        }
    }
}