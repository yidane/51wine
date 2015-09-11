using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.BLL;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.scenic
{
    /// <summary>
    /// secnic 的摘要说明
    /// </summary>
    public class secnic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];
            AjaxResponse response;
            switch (action)
            {
                case "GetScenic":
                    response = GetScenic(context);
                    break;
                default:
                    response = new AjaxResponse(new ErrorInfo("请求的方法不存在！"));
                    break;
            }

            context.Response.ContentType = "json/application";
            context.Response.Write(response);
        }

        private static AjaxResponse GetScenic(HttpContext context)
        {
            wx_travel_scenic scenicBll = new wx_travel_scenic();
            wx_travel_scenicDetail scenicDetailBll = new wx_travel_scenicDetail();

            int id = MyCommFun.Str2Int(context.Request["id"]);
            Model.wx_travel_scenic scenic = scenicBll.GetModel(id);

            List<Model.wx_travel_scenicDetail> details = scenicDetailBll.GetModelByScenicId(id);

            var result = new
            {
                scenic = scenic,
                details = details
            };

            return new AjaxResponse(result);
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