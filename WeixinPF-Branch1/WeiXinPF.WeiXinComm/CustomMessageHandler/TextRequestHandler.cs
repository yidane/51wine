

using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Configuration;
using OneGulp.WeChat.MP.Agent;
using OneGulp.WeChat.Context;
using OneGulp.WeChat.MP.Entities;
using OneGulp.WeChat.MP.MessageHandlers;
using OneGulp.WeChat.MP.Helpers;
using System.Xml;
using System.Xml.Linq;
using WeiXinPF.DAL;
using WeiXinPF.Common;
using WeiXinPF.BLL;
using WeiXinPF.WeiXinComm.threeInterface;

namespace WeiXinPF.WeiXinComm.CustomMessageHandler
{
    public partial class CustomMessageHandler
    {
        /// <summary>
        ///  处理文字请求 autor:lipu
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            IResponseMessageBase IR = null;
            int apiid = 0;
            try
            {

              //  var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
                string keywords = Utils.FilterSql(requestMessage.Content); //发送了文字信息
                apiid = wxcomm.getApiid();//这里的appiid即为微帐号主键Id(wid)

                if (!wxcomm.ExistApiidAndWxId(apiid, requestMessage.ToUserName))
                {  //验证接收方是否为我们系统配置的帐号，即验证微帐号与微信原始帐号id是否一致，如果不一致，说明【1】配置错误，【2】数据来源有问题
                    wxResponseBaseMgr.Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "none", "验证微帐号与微信原始帐号id不一致，说明【1】配置错误，【2】数据来源有问题", requestMessage.ToUserName);
                    return wxcomm.GetResponseMessageTxtByContent(requestMessage, "验证微帐号与微信原始帐号id不一致，可能原因【1】系统配置错误，【2】非法的数据来源", apiid);
                }
                bool isExist = wxcomm.wxCodeLegal(apiid);
                if (!isExist)
                {
                    wxResponseBaseMgr.Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "none", "账号已过期或已被禁用", requestMessage.ToUserName);
                    return wxcomm.GetResponseMessageTxtByContent(requestMessage, "账号已过期或已被禁用！", apiid);
                }
                //如果自动回复已经关闭，则不返回内容，start
                if (!wxcomm.wxCloseKW(apiid))
                {
                    wxResponseBaseMgr.Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "none", "等待客服", requestMessage.ToUserName);

                    return wxcomm.GetResponseNoneContent(requestMessage);
                }
                //如果自动回复已经关闭，则不返回内容，end
               //人工客服的关键词（写死）
                if (requestMessage.Content == "人工客服" || requestMessage.Content == "员工客服")
                {
                    return this.CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
                }

              
                int responseType = 0;
                string modelFunctionName = "";
                int  modelFunctionId = 0;
                int ruleId = rBll.GetRuleIdByKeyWords(apiid, keywords, out responseType, out modelFunctionName, out modelFunctionId);
                if (ruleId <= 0 || responseType<=0)
                {
                    wxResponseBaseMgr.Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "text", "未取到关键词对应的数据", requestMessage.ToUserName);
                    return  wxcomm.GetResponseMessageTxtByContent(requestMessage,"未找到匹配的关键词",apiid);
                }
                if (modelFunctionId > 0)
                {  //模块功能回复
                    return wxcomm.GetModuleResponse(requestMessage, modelFunctionName, modelFunctionId, apiid);
                }
                switch (responseType)
                {
                    case 1:
                        //发送纯文字
                        IR = wxcomm.GetResponseMessageTxt(requestMessage, ruleId, apiid);
                        break;
                    case 2:
                        //发送多图文
                        IR = wxcomm.GetResponseMessageNews(requestMessage, ruleId, apiid);
                        break;
                    case 3:
                        //发送语音
                        IR = wxcomm.GetResponseMessageeVoice(requestMessage, ruleId, apiid);
                        break;
                    default:
                        break;
                }
               //  wxRequestBaseMgr.Add(apiid, requestMessage.MsgType.ToString(), requestMessage.FromUserName, requestMessage.CreateTime.ToString(), requestMessage.Content, requestMessage.ToString());

                string fStr = FilterTxtRequest(apiid, requestMessage.Content, requestMessage.FromUserName);
                if (fStr.Trim() != "")
                {
                    wxResponseBaseMgr.Add(apiid, requestMessage.FromUserName, requestMessage.MsgType.ToString(), requestMessage.Content, "none", fStr, requestMessage.ToUserName);
                    return wxcomm.GetResponseMessageTxtByContent(requestMessage, fStr, apiid);
                }

            }
            catch (Exception ex)
            {

                WXLogs.AddErrLog(apiid, "用户请求文字", "CustomMessageHandler.OnTextRequest", "错误：" + ex.Message);
                
            }

            return IR;
        }


        /// <summary>
        /// 对客户发送的文字请求，扩展的方法===》截取
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns>如果返回值不为空字符串,则在微信里提示该文字。如果返回值为空字符串,则继续往下执行</returns>
        public string FilterTxtRequest(int apiid, string keywords, string openid)
        {
            //现金红包
            xjHongBao hbPorc = new xjHongBao();
            return hbPorc.KeyWordsHongBao(keywords, openid, apiid);

        }

        


    }
}
