using OneGulp.WeChat.MP.TenPayLibV3;
using System;
using System.Net;
using System.Net.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using OneGulp.WeChat.MP.AdvancedAPIs;
using WeiXinPF.BLL;
using System.Web;
using System.Xml;
using WeiXinPF.Common;


namespace WeiXinPF.WeiXinComm.threeInterface
{
    public class xjHongBao
    {
        BLL.wx_xjhongbao_lqinfo lqBll = new BLL.wx_xjhongbao_lqinfo();

        /// <summary>
        /// 关注时红包
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        public void SubscribeHongBao(string openid, int wid)
        {
            BLL.wx_xjhongbao_action actBll = new BLL.wx_xjhongbao_action();
            bool has = actBll.ExistsSubscribeAction(wid);//这个时间段有红包
        
            if (has)
            { //如果有关注时红包，则给人家发红包
                Model.wx_xjhongbao_action actModel = actBll.GetGZHBModelByWid(wid);
                proecssSendHB(openid, wid, actModel);

            }

        }

        /// <summary>
        /// 处理关键词红包
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        public string  KeyWordsHongBao(string keywords,string openid, int wid)
        {
            BLL.wx_xjhongbao_action actBll = new BLL.wx_xjhongbao_action();
            bool has = actBll.ExistsKeyWordsAction(wid,keywords);//这个时间段有红包

            if (has)
            { //如果有关注时红包，则给人家发红包
                Model.wx_xjhongbao_action actModel = actBll.GetKeyWordsModelByWid(wid, keywords);
                try
                {
                    proecssSendHB(openid, wid, actModel);
                }
                catch (Exception ex)
                {
                    WXLogs.AddLog(wid, "微信红包", actModel.act_name, "KeyWordsHongBao() 现金红包领取失败:" + ex.Message);
                }
            }

            return "";

        }



        /// <summary>
        /// 处理发关注红包的逻辑
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        /// <param name="actModel"></param>
        public int  proecssSendHB(string openid, int wid, Model.wx_xjhongbao_action actModel)
        {
            BLL.wx_xjhongbao_action actBll = new BLL.wx_xjhongbao_action();
            BLL.wx_xjhongbao_base baseBll = new BLL.wx_xjhongbao_base();
            Model.wx_xjhongbao_base baseModel = baseBll.GetModelByWid(wid);

             BLL.wx_payment_wxpay weixinPayBLL = new BLL.wx_payment_wxpay();
             Model.wx_payment_wxpay weixinPayModel = weixinPayBLL.GetModelByWid(wid);

              if ( baseModel == null || weixinPayModel == null || actModel==null)
              {
                    return 0;
              }

            #region 取用户昵称--注释掉了
            //string error = "";
            //string accessToken=WeiXinCRMComm.getAccessToken(wid, out error);
            //if (error.Trim().Length > 0)
            //{
            //    WXLogs.AddErrLog(wid, "微信红包", actModel.act_name, "accessToken取值有错误error:"+error);
            //    return;
            //}
            //string nick = UserApi.Info(accessToken, openid).nickname;
            #endregion

            //用户此次获得的红包（单位分）
            int hongbaoFen = 0;
            if (actModel.totalLqMoney >= actModel.totalMoney)
            {
                //1 红包金额已经领取完了,记录到日志
                InsertToLQinfo(wid, actModel.id, openid, 0, DateTime.Parse("1970-1-1"), "", "", "", "", "红包余额不足，需要调整活动的金额");
                return 0;
            }
            //2 判断该用户是否已经领取了
            bool hasLQ=  lqBll.ExistsOpenid(actModel.id, openid, actModel.lqType == 0 ? true : false);
            if (hasLQ)
            { //已经领取了
                return 0;
            }
            int leftMoney = actModel.totalMoney.Value - actModel.totalLqMoney.Value;//剩余金额
            //3 计算本次的红包
            if (actModel.moneyType == 0)
            { //定额
                hongbaoFen = actModel.min_value.Value;
            }
            else
            {  //随机
                 Random rd = new Random();
                 hongbaoFen = rd.Next(actModel.min_value.Value, actModel.max_value.Value);
            }

            if (hongbaoFen >= leftMoney)
            {
                hongbaoFen = leftMoney;
            }

            //4 给用户发红包
            string mchbillno = weixinPayModel.mch_id + DateTime.Now.ToString("yyyymmddHHmmss") + TenPayV3Util.BuildRandomStr(4);


            Model.wx_xjhongbao_lqinfo lqinfoEntity = new Model.wx_xjhongbao_lqinfo();
            lqinfoEntity.wid = wid;
            lqinfoEntity.actionId = actModel.id;
            lqinfoEntity.openid = openid;
            lqinfoEntity.total_amount = hongbaoFen;
            lqinfoEntity.createDate = DateTime.Now;

            XmlDocument doc = new XmlDocument();
            if (!SendHBBase(weixinPayModel, actModel, wid, openid, hongbaoFen, mchbillno, doc))
            { 
                //发红包的方法出现问题
                WXLogs.AddErrLog(wid, "微信红包", actModel.act_name, "发现金红包底层方法报错。");

                lqinfoEntity.Send_time = DateTime.Now;
                lqinfoEntity.mch_billno = mchbillno;
                lqinfoEntity.mch_id = weixinPayModel.mch_id;
                lqinfoEntity.hbstatus = "FAILED";
                lqinfoEntity.send_type = "API";
                lqinfoEntity.hb_type = "NORMAL";
                lqinfoEntity.reason = "result_code:" + doc.InnerXml.ToString();
               
                lqBll.Add(lqinfoEntity);  //记录领取日志
                WXLogs.AddLog(wid, "微信红包", actModel.act_name, "现金红包领取失败:" + doc.InnerXml.ToString());
                return 0;
            }
            //5修改现金：1活动的领取金额增加；2用户的中奖记录增加
            actModel = actBll.GetModel(actModel.id);
            actModel.totalLqMoney += hongbaoFen;
            actBll.Update(actModel);
            //6修改配置文件的领取金额；
            baseModel.totalLQMoney += hongbaoFen;
            baseBll.Update(baseModel);

            WXLogs.AddLog(wid, "微信红包", actModel.act_name, "现金红包领取成功2:" + doc.InnerXml.ToString());

            lqinfoEntity.Send_time = MyCommFun.Obj2DateTime(MyCommFun.GetXmlNode("send_time", doc),DateTime.Now);
            lqinfoEntity.mch_billno = mchbillno;
            lqinfoEntity.mch_id = weixinPayModel.mch_id;
            lqinfoEntity.detail_id =MyCommFun.GetXmlNode("send_listid", doc);
            lqinfoEntity.hbstatus = "SUCCESS";
            lqinfoEntity.send_type = "API";
            lqinfoEntity.hb_type = "NORMAL";
            lqinfoEntity.reason = "";
            lqinfoEntity.remark = doc.InnerXml.ToString();
            lqBll.Add(lqinfoEntity);

            WXLogs.AddLog(wid, "微信红包", actModel.act_name, "现金红包成功领取3:" + doc.InnerXml.ToString());
            return hongbaoFen;

        }

        /// <summary>
        /// 发红包的基础表
        /// </summary>
        /// <param name="baseModel"></param>
        /// <param name="actModel"></param>
        /// <param name="wid"></param>
        /// <param name="openid"></param>
        /// <param name="hbMoney">红包金额（分）</param>
        /// <returns></returns>
        private bool SendHBBase(Model.wx_payment_wxpay weixinPayModel, Model.wx_xjhongbao_action actModel, int wid, string openid, int hbMoney, string mchbillno, XmlDocument doc)
        {
            try
            {
                BLL.wx_userweixin bll = new BLL.wx_userweixin();
                Model.wx_userweixin weixinModel = bll.GetModel(wid);

                if (actModel == null || weixinModel == null || weixinPayModel == null)
                {
                    return false;
                }

                string nonceStr = TenPayV3Util.GetNoncestr();
                RequestHandler packageReqHandler = new RequestHandler(null);

                //设置package订单参数
                packageReqHandler.SetParameter("nonce_str", nonceStr);              //随机字符串
                packageReqHandler.SetParameter("wxappid", weixinModel.AppId);		  //公众账号ID(Appid)
                packageReqHandler.SetParameter("mch_id", weixinPayModel.mch_id);		  //商户号(mch_id)
                packageReqHandler.SetParameter("mch_billno", mchbillno);                 //填入商家订单号
                packageReqHandler.SetParameter("nick_name", actModel.nick_name);                 //提供方名称
                packageReqHandler.SetParameter("send_name", actModel.send_name);                 //红包发送者名称
                packageReqHandler.SetParameter("re_openid", openid);                 //接受收红包的用户的openId
                packageReqHandler.SetParameter("total_amount", hbMoney.ToString());                //付款金额，单位分
                packageReqHandler.SetParameter("min_value", hbMoney.ToString());                //最小红包金额，单位分
                packageReqHandler.SetParameter("max_value", hbMoney.ToString());                //最大红包金额，单位分
                packageReqHandler.SetParameter("total_num", "1");               //红包发放总人数
                packageReqHandler.SetParameter("wishing", actModel.wishing);               //红包祝福语
                packageReqHandler.SetParameter("client_ip", "1.0.0.1");               //调用接口的机器Ip地址.1.0.0.1 为无法获取客户端ip
                packageReqHandler.SetParameter("act_name", actModel.act_name);   //活动名称
                packageReqHandler.SetParameter("remark", actModel.remark);   //备注信息
                string sign = packageReqHandler.CreateMd5Sign("key", weixinPayModel.paykey);
                packageReqHandler.SetParameter("sign", sign);	                    //签名
                //发红包需要post的数据
                string data = packageReqHandler.ParseXML();

                //发红包接口地址
                string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
                //本地或者服务器的证书位置（证书在微信支付申请成功发来的通知邮件中）
                string cert = HttpContext.Current.Server.MapPath(weixinPayModel.certInfoPath); // @"F:\apiclient_cert.p12";
                //私钥（在安装证书时设置）
                string password = weixinPayModel.cerInfoPwd;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //调用证书
                X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

                #region 发起post请求
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.ClientCertificates.Add(cer);
                webrequest.Method = "post";

                byte[] postdatabyte = Encoding.UTF8.GetBytes(data);
                webrequest.ContentLength = postdatabyte.Length;
                Stream stream;
                stream = webrequest.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                HttpWebResponse httpWebResponse = (HttpWebResponse)webrequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseContent = streamReader.ReadToEnd();
                 
                doc.LoadXml(responseContent);
                WXLogs.AddErrLog(wid, "微信红包", actModel.act_name, "发现金红包底层方法,:xml内容："+doc.InnerXml.ToString());
                if (MyCommFun.GetXmlNode("return_code", doc).ToLower() == "success" && MyCommFun.GetXmlNode("result_code",doc).ToLower() == "success")
                {
                    //发红包成功
                    return true;
                }
                else
                {
                    return false;
                }
                #endregion

               
            }
            catch (Exception ex)
            {
                WXLogs.AddErrLog(wid, "微信红包", actModel.act_name, "发现金红包底层方法报错:" + ex.Message);

                return false;
            }


        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }


        protected void InsertToLQinfo(int wid, int aid, string openid, int total_amount, DateTime Send_time, string mch_billno, string mch_id, string detail_id, string hbstatus, string reason)
        {

            Model.wx_xjhongbao_lqinfo lqModel = new Model.wx_xjhongbao_lqinfo();
            lqModel.wid = wid;
            lqModel.actionId = aid;
            lqModel.openid = openid;
            lqModel.userNick = "";//用户昵称
            lqModel.total_amount = total_amount;
            if (Send_time != DateTime.Parse("1970-1-1"))
            {
                lqModel.Send_time = Send_time;
            }
            lqModel.mch_billno = mch_billno;
            lqModel.mch_id = mch_id;
            lqModel.detail_id = detail_id;
            lqModel.hbstatus = hbstatus;
            lqModel.send_type = "API";
            lqModel.hb_type = "NORMAL";
            lqModel.reason = reason;
            lqModel.createDate = DateTime.Now;
            lqBll.Add(lqModel);


        }
    }
}
