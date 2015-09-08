using WeiXinPF.Common;
using WeiXinPF.WeiXinComm.threeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.cashred
{
    /// <summary>
    /// 现金红包网页版
    /// </summary>
    public partial class index : WeiXinPage
    {
        Model.wx_xjhongbao_action act;
        protected void Page_Load(object sender, EventArgs e)
        {

            int wid = MyCommFun.RequestWid();
            int actid = MyCommFun.RequestInt("aid");
            string thisUrl = MyCommFun.getWebSite() + "/weixin/cashred/index.aspx?wid="+wid+"&aid="+actid;
            //授权
            BLL.wx_userweixin bll = new BLL.wx_userweixin();
            Model.wx_userweixin uWeiXinModel = bll.GetModel(wid);
           OAuth2BaseProc(uWeiXinModel, "index", thisUrl);
            //授权 end

            BLL.wx_xjhongbao_action actBll = new BLL.wx_xjhongbao_action();
            act = actBll.GetPageHongBaoModel(actid,wid);
            if (act == null)
            {
                litActionRemark.Text = "该时间段没有该活动";
                return;
            }
            //jssdk
            this.Title = act.act_name;
            fxModel.fxImg = MyCommFun.getWebSite() + "" + act.share_imgurl;
            fxModel.fxTitle = act.act_name;
            fxModel.fxContent = act.share_content;
            jssdkInit(uWeiXinModel);
            //jssdk end
            ActionBaseInfo(wid);
        }


       private void ActionBaseInfo(int wid)
        {
            zjpic.ImageUrl = act.actPic;
            litActionRemark.Text = act.remark;
            xjHongBao xjMgr = new xjHongBao();
           int money= xjMgr.proecssSendHB(openid, wid, act);
           if (money > 0)
           {
               litTitle.Text = "恭喜你中奖了";
               litMoney.Text = (money / 100.00).ToString() + "元";
           }
        }


    }
}