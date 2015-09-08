using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.Common;
using WeiXinPF.Web.UI;
using System.Data;
using System.Text;

namespace WeiXinPF.Web.admin.magazine
{
    /// <summary>
    /// getdata 的摘要说明
    /// </summary>
    public class getdata : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            Dictionary<string, string> jsonDict = new Dictionary<string, string>();
            string _action = MyCommFun.QueryString("myact");
            if (_action == "uploadImg")
            {
                #region 图片上传
                try
                {
                    string uploadFileRet = UpLoadFile(context);
                    Dictionary<string, object> dict = MyCommFun.JsonToDictionary(uploadFileRet);

                    if (dict["status"].ToString() == "0")
                    {
                        //上传失败
                        jsonDict.Add("res", "0");
                        jsonDict.Add("content", dict["msg"].ToString());
                        context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                        return;
                    }
                    else if (dict["status"].ToString() == "1")
                    {
                        //上传成功，修改用户的头像url 
                        jsonDict.Add("res", "1");
                        jsonDict.Add("thumb", dict["thumb"].ToString());
                        jsonDict.Add("newPhotoUrl", dict["path"].ToString());
                        context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                        return;
                    }
                }
                catch (Exception e)
                {
                    //上传失败
                    jsonDict.Add("res", "0");
                    jsonDict.Add("content", e.Message);
                    context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                    return;
                }
                #endregion
            }

            if (_action == "getAm")
            {
                int iid = MyCommFun.RequestInt("iid");
                BLL.wx_mz_animate amBll = new BLL.wx_mz_animate();
                BLL.wx_mz_img iBll = new BLL.wx_mz_img();
                try
                {

                    string mzimg = iBll.GetModel(iid).url;
                    DataSet ds = amBll.GetList("iid=" + iid);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder("{\"res\":1,\"content\":[");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            sb.Append("{");
                            sb.Append("\"id\":" + dr["id"] + ",");
                            sb.Append("\"width\":" + dr["width"] + ",");
                            sb.Append("\"height\":" + dr["height"] + ",");
                            sb.Append("\"x_lc\":" + dr["x_loaction"] + ",");
                            sb.Append("\"y_lc\":" + dr["y_location"] + ",");
                            sb.Append("\"img\":\"" + dr["animate_img"] + "\",");
                            sb.Append("\"type\":\"" + dr["animate_type"] + "\",");
                            sb.Append("\"s_sec\":" + dr["start_seconds"] + ",");
                            sb.Append("\"c_sec\":" + dr["continue_seconds"]);
                            sb.Append("},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("],\"img\":\"" + mzimg + "\"}");
                        context.Response.Write(sb.ToString());
                        return;
                    }
                    else
                    {
                        context.Response.Write("{\"res\":2,\"img\":\"" + mzimg + "\"}");
                        return;
                    } 
                }
                catch (Exception)
                {
                    context.Response.Write("{\"res\":0}");
                    return;
                }
            }

        }

        /// <summary>
        /// 上传图片的方法
        /// 返回缩略图的路劲
        /// </summary>
        /// <param name="context"></param>
        private string UpLoadFile(HttpContext context)
        {
            HttpPostedFile _upfile = context.Request.Files["header_img_id"];
            //  HttpFileCollection files = HttpContext.Current.Request.Files;
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = true; //默认生成缩略图
            _isthumbnail = true;
            if (_upfile == null)
            {
                return "";
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);

            //返回成功信息
            return msg;
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