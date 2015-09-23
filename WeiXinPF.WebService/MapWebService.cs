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
        public void GetMarkers(int wid)
        {
            try
            {
                BLL.wx_travel_marker bll = new BLL.wx_travel_marker();
                var markers = bll.GetModelList(string.Format("wid={0}", wid));
               
                if (!markers.Any())
                {
                    Context.Response.Write(new AjaxResponse(new ErrorInfo("该微信帐号下未配置景点信息")));
                    return;
                }

                Context.Response.Write(new AjaxResponse(markers));
            }
            catch
            {
                Context.Response.Write(new AjaxResponse(new ErrorInfo("获取景点信息失败")));
            }
        }
    }
}
