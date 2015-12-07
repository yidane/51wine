using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.Common;
using ThoughtWorks.QRCode.Codec;

namespace WeiXinPF.Web.weixin.zhanhui
{
    /// <summary>
    /// getdata 的摘要说明
    /// </summary>
    public class getdata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string _action = MyCommFun.QueryString("myact");
            Dictionary<string, string> jsondict = new Dictionary<string, string>();

            #region 注册观众信息
            if (_action == "addGz")
            {
                BLL.wx_zh_gz gzBll = new BLL.wx_zh_gz();
                string tel = MyCommFun.QueryString("tel");
                string fax = MyCommFun.QueryString("fax");
                string mobile = MyCommFun.QueryString("mobile");
                string qq = MyCommFun.QueryString("qq");
                string linkman = MyCommFun.QueryString("linkman");
                string email = MyCommFun.QueryString("email");
                string company = MyCommFun.QueryString("company");

                try
                {
                    Model.wx_zh_gz gzModel = new Model.wx_zh_gz
                    {
                        company_name = company,
                        tel = tel,
                        createdate = DateTime.Now,
                        fax = fax,
                        linkman = linkman,
                        mobile = mobile,
                        qq = qq,
                        email = email
                    };

                    string gzid = gzBll.Add(gzModel).ToString();
                    MyCommFun.setCookie("gz_id", gzid, 30);
                    jsondict.Add("res", "1");
                    jsondict.Add("content", gzid);
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }
                catch (Exception)
                {
                    jsondict.Add("res", "0");
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }

            }
            #endregion

            #region 注册展商信息
            if (_action == "addZs")
            {
                BLL.wx_zh_zs zsBll = new BLL.wx_zh_zs();
                string tel = MyCommFun.QueryString("tel");
                string fax = MyCommFun.QueryString("fax");
                string mobile = MyCommFun.QueryString("mobile");
                string qq = MyCommFun.QueryString("qq");
                string linkman = MyCommFun.QueryString("linkman");
                string email = MyCommFun.QueryString("email");
                string company = MyCommFun.QueryString("company");
                string type = MyCommFun.QueryString("type");
                string area = MyCommFun.QueryString("area");

                try
                {
                    Model.wx_zh_zs zsModel = new Model.wx_zh_zs
                    {
                        company_name = company,
                        tel = tel,
                        createdate = DateTime.Now,
                        fax = fax,
                        linkman = linkman,
                        mobile = mobile,
                        qq = qq,
                        email = email,
                        sq_area = area,
                        type = type
                    };

                    string zsid = zsBll.Add(zsModel).ToString();
                    MyCommFun.setCookie("zs_id", zsid, 30);
                    jsondict.Add("res", "1");
                    jsondict.Add("content", zsid);
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }
                catch (Exception)
                {
                    jsondict.Add("res", "0");
                    context.Response.Write(MyCommFun.getJsonStr(jsondict));
                    return;
                }

            }
            #endregion

            if (_action == "getqrcode")
            {
                #region 生成二维码
                //if (Request["chl"] == null) { return; }
                //string url = "http://" + HttpContext.Current.Request.Url.Authority + "/weixin/ucard/checkFX.aspx?" + Request["chl"].ToString().Replace("_", "&");
                string url = MyCommFun.getWebSite() + "/weixin/zhanhui/gzReg_ret.aspx";

                //创建二维码生成类  
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                try
                {
                    qrCodeEncoder.QRCodeScale = 4;
                }
                catch { }
                String data = url;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White; //背景颜色
                // qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;//图像颜色
                //设置编码版本  
                qrCodeEncoder.QRCodeVersion = 0;

                System.Drawing.Image myimg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8); //kedee 增加utf-8编码，可支持中文汉字  
                myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Gif";
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.End();
                #endregion
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}