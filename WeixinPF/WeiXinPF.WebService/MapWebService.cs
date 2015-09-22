using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using WeiXinPF.Application.DomainModules.Map;
using WeiXinPF.Application.DomainModules.Map.DTOS;
using WeiXinPF.Common;

namespace WeiXinPF.WebService
{
    public class MapWebService : BaseWebService
    {
        /// <summary>
        /// 获取地图基本信息
        /// </summary>
        [WebMethod]
        public void GetMapInfo(int id)
        {
            try
            {
                var service = new MapService();
                var mapInfo = service.GetMapInfo(id);


                Context.Response.Write(AjaxResult.Success(mapInfo));
            }
            catch (JsonException jsEx)
            {

                Context.Response.Write(AjaxResult.Error(jsEx.Message, jsEx.ErrorType));
            }
            catch (Exception ex)
            {

                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
        }

        /// <summary>
        /// 获取所有景点信息
        /// </summary>
        [WebMethod]
        public void GetSecnicSpotList(int wid)
        {
            try
            {
                BLL.wx_travel_scenic scenicBll = new BLL.wx_travel_scenic();
                BLL.wx_travel_scenicDetail detailBll = new BLL.wx_travel_scenicDetail();
                var scenic = scenicBll.GetModelList(string.Format("wid={0}", wid)).FirstOrDefault();
                if (scenic == null)
                {
                    Context.Response.Write(new AjaxResponse(new ErrorInfo("改微信帐号下未配置景区信息")));
                    return;
                }

                var details = detailBll.GetModelByScenicId(scenic.Id);

                if (!details.Any())
                {
                    Context.Response.Write(new AjaxResponse(new ErrorInfo("改微信帐号下未配置景点信息")));
                    return;
                }

                Context.Response.Write(new AjaxResponse(details));
            }
            catch
            {
                Context.Response.Write(new AjaxResponse(new ErrorInfo("获取景点信息失败")));
            }
        }
    }
}
