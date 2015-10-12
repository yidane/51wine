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
        public void GetMapInfo(int id, string type)
        {
            try
            {
                var mapInfo = new MapDTO();
                var service = new MapService();
                switch (type)
                {
                    case "scenic"://类似是景区
                        mapInfo = service.GetMapInfo(id);
                        break;
                    case "food"://类似是美食

                        mapInfo = service.GetShop(id);
                        break;
                    case "hotel"://类似是酒店

                        mapInfo = service.GetHotel(id);
                        break;

                }
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

        [WebMethod]
        public void GetScenics(int wid, string keywords)
        {
            try
            {
                var service = new MapService();
                var scenics = service.GetScenics(wid, keywords);

                Context.Response.Write(new AjaxResponse(scenics));
            }
            catch
            {
                Context.Response.Write(new AjaxResponse(new ErrorInfo("获取景点信息失败")));
            }
        }

        /// <summary>
        /// 获取周边餐饮列表
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        [WebMethod]
        public void GetCateringShops(int wid, string keywords)
        {
            try
            {
                var service = new MapService();
                var shops = service.GetCateringShops(wid, keywords);

                Context.Response.Write(new AjaxResponse(shops));
            }
            catch
            {
                Context.Response.Write(new AjaxResponse(new ErrorInfo("获取周边餐饮信息失败")));
            }
        }

        /// <summary>
        /// 获取周边酒店列表
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        [WebMethod]
        public void GetHotels(int wid, string keywords)
        {
            try
            {
                var service = new MapService();
                var hotels = service.GetHotels(wid, keywords);

                Context.Response.Write(new AjaxResponse(hotels));
            }
            catch
            {
                Context.Response.Write(new AjaxResponse(new ErrorInfo("获取周边酒店信息失败")));
            }
        }
    }
}
