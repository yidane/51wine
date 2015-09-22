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
   public class MapWebService:BaseWebService
    {
        /// <summary>
        /// 获取地图基本信息
        /// </summary>
       [WebMethod]
       public void GetMapInfo(int id)
       {
            try
            {
                var service=new MapService();
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
    }
}
