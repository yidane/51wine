namespace WeiXinPF.Application.DomainModules.IdentifyingCode.Service
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    using WeiXinPF.Common;
    using WeiXinPF.Infrastructure.DomainDataAccess;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode;
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

        public static IdentifyingCodeInfo GetConfirmIdentifyingCodeInfo(int shopId, string identifyingCode)
        {
            if (shopId == 0 || string.IsNullOrEmpty(identifyingCode))
            {
                return null;
            }

            using (var context = new WXDBContext())
            {
                return
                    new IdentifyingCodeRepository(context).Get(
                        item =>
                        item.ShopId == shopId.ToString(CultureInfo.InvariantCulture)
                        && item.IdentifyingCode.Equals(identifyingCode)).FirstOrDefault();
            }
        }

        public static IdentifyingCodeInfo GetIdentifyingCodeInfoByIdentifyingCodeId(Guid identifyingCodeId)
        {
            if (identifyingCodeId == null || Guid.Empty.Equals(identifyingCodeId))
            {
                return null;
            }

            using (var context = new WXDBContext())
            {
                return
                    new IdentifyingCodeRepository(context).Get(item => item.IdentifyingCodeId.Equals(identifyingCodeId))
                    .FirstOrDefault();
            }
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
    }
}
