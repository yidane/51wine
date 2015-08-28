using System;
using System.Collections.Generic;
using System.ServiceModel;
using Travel.Infrastructure.OTAWebService.OTAWebServiceRef;
using Travel.Infrastructure.OTAWebService.Request;
using Travel.Infrastructure.OTAWebService.Response;

namespace Travel.Infrastructure.OTAWebService
{
    public class OTAServiceManager
    {
        private readonly ServiceSoapClient m_ServiceSoapClient = null;

        public OTAServiceManager()
        {
            if (m_ServiceSoapClient == null)
            {
                m_ServiceSoapClient = new ServiceSoapClient(new BasicHttpBinding(), new EndpointAddress(OTAConfigManager.ServiceUrl));
            }
        }

        /// <summary>
        /// ota改签订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<ChangeOrderEditResponse> ChangeOrderEdit(ChangeOrderEditRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.PostOrder.ToString());
            var result = m_ServiceSoapClient.ChangeOrderEdit(OTAConfigManager.MerchantCode, request.PostOrder.ToString(), signture);

            return OTAResult<ChangeOrderEditResponse>.CreateInstance(result);
        }

        /// <summary>
        /// 合作者获取自己账户信息
        /// </summary>
        /// <returns></returns>
        public OTAResult<GetAccountInfoResponse> GetAccountInfo()
        {
            var result = m_ServiceSoapClient.GetAccountInfo(OTAConfigManager.MerchantCode, OTAConfigManager.GetSignature());
            return OTAResult<GetAccountInfoResponse>.CreateInstance(result);
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<List<GetAllOrderStatusResponse>> GetAllOrderStatus(GetAllOrderStatusRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.ToString());
            var result = m_ServiceSoapClient.GetAllOrderStatus(OTAConfigManager.MerchantCode, request.ToString(), signture, request.BusinessType, request.ParkCode);
            return OTAResult<List<GetAllOrderStatusResponse>>.CreateInstance(result);
        }

        /// <summary>
        /// ota获取某日产品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<List<GetProductsResponse>> GetProducts(GetProductRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.parameters.ToString());
            var result = m_ServiceSoapClient.GetProducts(OTAConfigManager.MerchantCode, request.parameters.ToString(), signture);
            return OTAResult<List<GetProductsResponse>>.CreateInstance(result);
        }

        /// <summary>
        /// ota查询某日某景区对自己的配额信息
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetQuotaInfo(DateTime date)
        {
            var parameters = new Parameter().ToString();
            //parameters = "[" + parameters + "]";
            var dateString = date.ToString("yyyy-MM-dd");
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.ParkCode, OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, dateString, parameters);
            var result = m_ServiceSoapClient.GetQuotaInfo(OTAConfigManager.ParkCode, OTAConfigManager.MerchantCode, dateString, parameters, signture);

            return result;
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<List<OrderFinishResponse>> OrderFinish(OrderFinishRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.OtaOrderNO, request.platformSend, request.Parameters.ToString());
            var result = m_ServiceSoapClient.OrderFinish(OTAConfigManager.MerchantCode, request.OtaOrderNO, request.platformSend, request.Parameters.ToString(), signture);
            return OTAResult<List<OrderFinishResponse>>.CreateInstance(result);
        }

        /// <summary>
        /// ota回传订单--占用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<OrderOccupiesResponse> OrderOccupies(OrderOccupiesRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.postOrder.ToString());
            var result = m_ServiceSoapClient.OrderOccupies(OTAConfigManager.MerchantCode, request.postOrder.ToString(), signture);
            return OTAResult<OrderOccupiesResponse>.CreateInstance(result);
        }

        /// <summary>
        /// OTA订单释放
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OTAResult<OrderReleaseResponse> OrderRelease(OrderReleaseRequest request)
        {
            var signture = OTAConfigManager.GetSignature(OTAConfigManager.MerchantCode, OTAConfigManager.MerchantKey, request.OtaOrderNO, request.Parameters.ToString());
            var result = m_ServiceSoapClient.OrderRelease(OTAConfigManager.MerchantCode, request.OtaOrderNO, request.Parameters.ToString(), signture);
            return OTAResult<OrderReleaseResponse>.CreateInstance(result);
        }

        /// <summary>
        /// OTA合作者进行短信重发
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string RepeatMess(RepeatMessRequest request)
        {
            return string.Empty;
        }

        #region 测试方法
        /// <summary>
        /// 后去测试回传订单json
        /// </summary>
        /// <returns></returns>
        public string testOrderOccupiesJson()
        {
            return string.Empty;
        }

        /// <summary>
        /// 测试数据签名
        /// </summary>
        /// <param name="parames"></param>
        /// <param name="parames1"></param>
        /// <param name="parames2"></param>
        /// <param name="parames3"></param>
        /// <returns></returns>
        public string TestSignture(string parames, string parames1, string parames2, string parames3)
        {
            return m_ServiceSoapClient.testSignture(OTAConfigManager.MerchantCode, parames, parames1, parames2, parames3, OTAConfigManager.MerchantKey);
        }
        #endregion
    }
}