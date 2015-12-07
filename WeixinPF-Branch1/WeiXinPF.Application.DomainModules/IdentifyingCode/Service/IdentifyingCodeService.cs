namespace WeiXinPF.Application.DomainModules.IdentifyingCode.Service
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    using WeiXinPF.BLL;
    using WeiXinPF.Common;
    using WeiXinPF.Infrastructure.DomainDataAccess;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.Repository;

    public class IdentifyingCodeService
    {
        public static bool AddIdentifyingCode(IdentifyingCodeInfo code)
        {
            if (code != null)
            {
                var addResult = false;
                code.IdentifyingCode = Utils.Number(12);

                using (var context = new WXDBContext())
                {
                    addResult = new IdentifyingCodeRepository(context).AddIdentifyingCode(code);
                }

                // 如果IdentifyingCode在数据库中存在，则递归执行此方法重新获取编号
                if (!addResult)
                {
                    AddIdentifyingCode(code);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static IList<IdentifyingCodeInfo> GetIdentifyingCodeInfoByOrderId(int shopId, string moduleName, string orderId, int wid)
        {
            using (var db = new WXDBContext())
            {
                return new IdentifyingCodeRepository(db).Get(
                                item =>
                                    item.ModuleName.Equals(moduleName) 
                                    && item.ShopId.Equals(shopId.ToString())
                                    && item.OrderId.Equals(orderId) 
                                    && item.Wid.Equals(wid)).ToList();
            }
        }

        public static IdentifyingCodeInfo GetConfirmIdentifyingCodeInfo(int shopId, string identifyingCode, string moduleName, int wid)
        {
            if (shopId == 0 || string.IsNullOrEmpty(identifyingCode) || string.IsNullOrEmpty(moduleName))
            {
                return null;
            }

            using (var context = new WXDBContext())
            {
                return
                    new IdentifyingCodeRepository(context).Get(
                        item =>
                        item.ShopId == shopId.ToString(CultureInfo.InvariantCulture)
                        && item.IdentifyingCode.Equals(identifyingCode)
                        && item.ModuleName.Equals(moduleName)
                        && item.Wid.Equals(wid)).FirstOrDefault();
            }
        }

        public static IdentifyingCodeInfo GetIdentifyingCodeInfoByIdentifyingCodeId(Guid identifyingCodeId, string moduleName, int wid)
        {
            if (identifyingCodeId == null || Guid.Empty.Equals(identifyingCodeId))
            {
                return null;
            }

            using (var context = new WXDBContext())
            {
                return
                    new IdentifyingCodeRepository(context).Get(
                            item => item.IdentifyingCodeId.Equals(identifyingCodeId)
                                    && item.ModuleName.Equals(moduleName)
                                    && item.Wid.Equals(wid)).FirstOrDefault();
            }
        }

        public static IList<IdentifyingCodeDetailSearchDTO> GetIdentifyingCodeDetailById(string identifyingCodeId, string moduleName)
        {
            if (string.IsNullOrEmpty(identifyingCodeId))
            {
                return null;
            }

            var obj = new IdentifyingCodeInfo() { IdentifyingCodeId = Guid.Parse(identifyingCodeId), ModuleName = moduleName };

            return new IdentifyingCodeRepository(new WXDBContext()).GetIdentifyingCodeDetailById(obj);
        }

        public static IList<OrderDetailDTO> GetOrderDetail(int shopId, string moduleName, string condition)
        {
            if (string.IsNullOrEmpty(moduleName))
            {
                return null;
            }

            var obj = new IdentifyingCodeInfo() { ShopId = shopId.ToString(), ModuleName = moduleName };

            return new IdentifyingCodeRepository(new WXDBContext()).GetOrderDetail(obj, condition);
        }

        public static IList<IdentifyingCodeDTO> GetIdentifyingCodeDTO(string moduleName, int orderId)
        {
            if (string.IsNullOrEmpty(moduleName))
            {
                return null;
            }

            return new IdentifyingCodeRepository(new WXDBContext()).GetIdentifyingCodeDTO(moduleName, orderId);
        }

        public static bool ModifyIdentifyingCodeInfo(IdentifyingCodeInfo code)
        {
            if (code == null)
            {
                return false;
            }

            using (var context = new WXDBContext())
            {
                return new IdentifyingCodeRepository(context).Update(code);
            }
        }

        public static bool ModifyIdentifyingCodeInfoStatus(string orderCode, int status)
        {
            if (string.IsNullOrEmpty(orderCode))
            {
                return false;
            }

            using (var context = new WXDBContext())
            {
                var repository = new IdentifyingCodeRepository(context);

                var identifyingCode = repository.Get(item => item.OrderCode.Equals(orderCode));
                if (identifyingCode.Any())
                {
                    foreach (var item in identifyingCode)
                    {
                        item.Status = status;
                        item.ModifyTime = DateTime.Now;
                    }
                    
                    return repository.Update(identifyingCode);
                }
            }

            return false;
        }
    }
}
