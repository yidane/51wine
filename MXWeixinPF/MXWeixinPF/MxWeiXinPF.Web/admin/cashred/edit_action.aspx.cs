using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using MxWeiXinPF.BLL;
/**************************************
 *
 * author: 李 朴
 * company:上海 沐 雪 网络科 技有限公 司
 * qq:23002807
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2015-8-2
 * update:2015-8-2
 * 
 ***********************************/

namespace MxWeiXinPF.Web.admin.cashred
{
    public partial class edit_action : Web.UI.ManagePage
    {
        private string action = MXEnums.ActionEnum.Add.ToString(); //操作类型

        wx_xjhongbao_base xjbaseBll = new wx_xjhongbao_base();
        wx_xjhongbao_action xjactBll = new wx_xjhongbao_action();
        wx_requestRule rBll = new wx_requestRule();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = MXRequest.GetQueryString("action");
            int id = 0;
            Model.wx_userweixin weixin = GetWeiXinCode();
            if (!string.IsNullOrEmpty(_action) && _action == MXEnums.ActionEnum.Edit.ToString())
            {

                this.action = MXEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out  id))
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!xjactBll.Exists(id, weixin.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
                if (weixin.wxType != 4)
                {
                    //必须是认证过的服务号才可以开通该功能
                    JscriptMsg("必须是认证过的服务号(并且开通微信支付)才可以开通该功能！", "back", "Error");
                    return;
                }

            }
            if (!Page.IsPostBack)
            {
                //创建基本表
                AddBaseInfo(weixin.id);
                if (action == MXEnums.ActionEnum.Edit.ToString()) //修改
                {

                    Show(id);
                }
                else
                {
                    txtbeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txtendDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="id">活动的id</param>
        protected void Show(int id)
        {
            ddlhbType.Enabled = false; //不启用
            Model.wx_userweixin weixin = GetWeiXinCode();
            Model.wx_xjhongbao_action xjModel = xjactBll.GetModel(id);
            //活动基本信息
            txtact_name.Text = xjModel.act_name;
            hidid.Value = xjModel.id.ToString();
            ddlhbType.SelectedValue = xjModel.hbType.Value.ToString();
            lblLink.Text = MyCommFun.getWebSite() + "/weixin/cashred/index.aspx?wid=" + weixin.id + "&aid=" + id;
           txtactPic.Text = xjModel.actPic;
           imgactPic.ImageUrl = xjModel.actPic;
            txtbeginDate.Text = xjModel.beginDate.ToString();
            txtendDate.Text = xjModel.endDate.ToString();
            radlqType.SelectedValue = xjModel.lqType.ToString();
            txttotalMoney.Text = (xjModel.totalMoney / 100.00).ToString();
            lblLqMoney.Text = (xjModel.totalLqMoney / 100.00).ToString();

            radmoneyType.SelectedValue = xjModel.moneyType.ToString();
            txtmin_value.Text = (xjModel.min_value / 100.00).ToString();
            txtmax_value.Text = (xjModel.max_value / 100.00).ToString();
            txtremark.Value = xjModel.remark;
            txtKW.Text = xjModel.keywords;
            //if (xjModel.hbType == 1)
            //{
            //    Model.wx_requestRule rule = rBll.GetModelList("modelFunctionName='现金红包' and modelFunctionId=" + id)[0];
            //    txtKW.Text = rule.reqKeywords;

            //}
            //红包相关的参数
            txtwishing.Text = xjModel.wishing;
            txtnick_name.Text = xjModel.nick_name;
            txtsend_name.Text = xjModel.send_name;
            imglogo_imgurl.ImageUrl = xjModel.logo_imgurl;
            txtlogo_imgurl.Text = xjModel.logo_imgurl;
            txtclient_ip.Text = xjModel.client_ip;

            //分享
            txtshare_content.Value = xjModel.share_content;
            txtshare_url.Value = xjModel.share_url;
            imgshare_imgurl.ImageUrl = xjModel.share_imgurl;
            txtshare_imgurl.Text = xjModel.share_imgurl;

        }


        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Model.wx_userweixin weixin = GetWeiXinCode();
            int id = MyCommFun.Str2Int(hidid.Value);
            #region  //先判断
            string strErr = "";

            if (this.txtact_name.Text.Trim().Length == 0)
            {
                strErr += "活动名称不能为空！";
            }
            if (ddlhbType.SelectedItem.Value == "")
            {
                strErr += "活动类型不能为空！";
            }
            if (ddlhbType.SelectedItem.Value == "1")
            {

                if (this.txtKW.Text.Trim().Length == 0)
                {
                    strErr += "关键词不能为空！";
                }
                
            }

            if (this.txtbeginDate.Text.Trim().Length == 0 || !MyCommFun.isDateTime(txtbeginDate.Text))
            {
                strErr += "开始时间不能为空！";
            }
            if (this.txtendDate.Text.Trim().Length == 0 || !MyCommFun.isDateTime(txtendDate.Text))
            {
                strErr += "结束时间不能为空！";
            }

            if (this.txtwishing.Text.Trim().Length == 0)
            {
                strErr += "祝福语不能为空！";
            }

            if (this.txtnick_name.Text.Trim().Length == 0)
            {
                strErr += "提供方名称不能为空！";
            }

            if (this.txtsend_name.Text.Trim().Length == 0)
            {
                strErr += "商户名称不能为空！";
            }

            if (this.txtclient_ip.Text.Trim().Length == 0)
            {
                strErr += "调用接口的机器 Ip 地址不能为空！";
            }

            if (this.txttotalMoney.Text.Trim().Length == 0)
            {
                strErr += "红包总金额不能为空！";
            }

            if (strErr != "")
            {
                JscriptMsg(strErr, "back", "Error");
                return;
            }
            DateTime begin = DateTime.Parse(txtbeginDate.Text.Trim());
            DateTime end = DateTime.Parse(txtendDate.Text.Trim());
            if (begin >= end)
            {
                JscriptMsg("开始时间必须小于结束时间", "back", "Error");
                return;
            }
            if (ddlhbType.SelectedItem.Value == "0")
            {
                //如果是关注红包，则要判断该微账号同一时间段是否已经包含了关注红包
                IList<Model.wx_xjhongbao_action> actlist = xjactBll.GetModelList("wid=" + weixin.id + " and id!="+id+" and hbType=0 and  endDate>='" + begin.ToString() + "' and beginDate<='" + end.ToString() + "' ");
               
                if (actlist != null && actlist.Count > 0)
                {
                    JscriptMsg("该时间段内不能有2个关注红包", "back", "Error");
                    return;
                }
            }

            #endregion

            #region 赋值
            Model.wx_xjhongbao_action xjActionModel = new Model.wx_xjhongbao_action();
            if (id > 0)
            {   //修改
                xjActionModel = xjactBll.GetModel(id);
            }

            xjActionModel.act_name = txtact_name.Text.Trim();

            xjActionModel.actPic = txtactPic.Text.Trim();
            xjActionModel.beginDate = MyCommFun.Obj2DateTime(txtbeginDate.Text);
            xjActionModel.endDate = MyCommFun.Obj2DateTime(txtendDate.Text);
            xjActionModel.lqType = MyCommFun.Str2Int(radlqType.Text);
            xjActionModel.totalMoney = (int)(MyCommFun.Str2Decimal(txttotalMoney.Text) * 100);
            xjActionModel.moneyType = MyCommFun.Str2Int(radmoneyType.SelectedItem.Value);
            xjActionModel.min_value = (int)(MyCommFun.Str2Decimal(txtmin_value.Text) * 100);
            xjActionModel.keywords = txtKW.Text.Trim();

            if (ddlhbType.SelectedItem.Value == "0")
            {  //如果红包为关注时红包，则领取方式为：一次性
                xjActionModel.lqType = 0;
            }
            if (xjActionModel.moneyType == 0)
            {
                xjActionModel.max_value = xjActionModel.min_value;
            }
            else
            {
                xjActionModel.max_value = (int)(MyCommFun.Str2Decimal(txtmax_value.Text) * 100);
            }
            xjActionModel.remark = txtremark.Value;

            //红包参数
            xjActionModel.wishing = txtwishing.Text.Trim();
            xjActionModel.nick_name = txtnick_name.Text.Trim();
            xjActionModel.send_name = txtsend_name.Text.Trim();
            xjActionModel.logo_imgurl = txtlogo_imgurl.Text.Trim();
            xjActionModel.client_ip = txtclient_ip.Text.Trim();

            //分享的参数
            xjActionModel.share_content = txtshare_content.Value.Trim();
            xjActionModel.share_url = txtshare_url.Value.Trim();
            xjActionModel.share_imgurl = txtshare_imgurl.Text.Trim();

            if (id > 0)
            {
                //修改
                bool updateOK = xjactBll.Update(xjActionModel);
                //if (xjActionModel.hbType == 1)
                //{
                //    //添加关键词
                //    IList<Model.wx_requestRule> rlist = rBll.GetModelList("modelFunctionName = '现金红包' and modelFunctionId=" + id);

                //    if (rlist != null && rlist.Count > 0)
                //    {
                //        Model.wx_requestRule rule = new Model.wx_requestRule();

                //        rule = rlist[0];
                //        rule.reqKeywords = txtKW.Text.Trim();
                //        rBll.Update(rule);
                //    }
                //    else
                //    {
                //        AddRule(weixin.id, id);
                //    }
                //}
                AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改现金红包活动，主键为" + id); //记录日志//1e2124dd04e11d01b9df2865f85944be
                JscriptMsg("修改现金红包活动成功！", "actionmgr.aspx", "Success");


            }
            else
            {
                //新增
                xjActionModel.hbType = MyCommFun.Str2Int(ddlhbType.Text.Trim());
                xjActionModel.totalLqMoney = 0;
                xjActionModel.wid = weixin.id;
                xjActionModel.createDate = DateTime.Now;
                int addId = xjactBll.Add(xjActionModel);
                //if (xjActionModel.hbType == 1)
                //{
                //    //添加关键词
                //    AddRule(weixin.id, addId);
                //}

                AddAdminLog(MXEnums.ActionEnum.Add.ToString(), "添加现金红包活动，主键为" + addId); //记录日志//1e2124dd04e11d01b9df2865f85944be
                JscriptMsg("添加现金红包活动成功！", "actionmgr.aspx", "Success");


            }



            #endregion

        }

      


        private void AddBaseInfo(int wid)
        {
            BLL.wx_xjhongbao_base xjBll = new wx_xjhongbao_base();
            Model.wx_xjhongbao_base baseModel = xjBll.GetModelByWid(wid);
            if (baseModel == null || baseModel.id == 0)
            {
                baseModel = new Model.wx_xjhongbao_base();
                baseModel.wid = wid;
                baseModel.createDate = DateTime.Now;
                baseModel.totalLQMoney = 0;
                baseModel.accountBalance = 0;
                baseModel.remark = "";
                baseModel.warningInfo = "";
                int retNum = xjBll.Add(baseModel);

            }
        }



    }
}
//1_e2124dd04e11d01b9df2865f85944be