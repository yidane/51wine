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
    public class scenic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];
            AjaxResult response;
            switch (action)
            {
                case "GetScenic":
                    response = GetScenic(context);
                    break;
                default:
                    response = AjaxResult.Error("请求的方法不存在！");
                    break;
            }

            context.Response.ContentType = "json/application";
            context.Response.Write(response.ToCamelString());
        }

        private static AjaxResult GetScenic(HttpContext context)
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

            return AjaxResult.Success(result);
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