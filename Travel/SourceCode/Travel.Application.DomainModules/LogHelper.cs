using System;
using Travel.Infrastructure.DomainDataAccess.Order;

namespace Travel.Application.DomainModules
{
    public class LogHelper
    {
        public static void WriteOperateLog(string moduleName, OrderEntity order, string operateName, bool isOperateSuccess, string errorCOde, string errorDescription)
        {
            WriteOperateLog(moduleName,order.OrderCode, order.OrderId.ToString(), operateName, isOperateSuccess, errorCOde, errorDescription);         
        }

        public static void WriteOperateLog(string moduleName, string orderCode, string orderId, string operateName, bool isOperateSuccess, string errorCOde, string errorDescription)
        {
            try
            {
                var log = new InterfaceOperationLogEntity()
                {
                    InterfaceOperationLogId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Module = moduleName,
                    OrderCode = orderCode,
                    OperateObjectId = orderId,
                    OperateName = operateName,
                    IsOperateSuccess = isOperateSuccess,
                    ErrorCode = errorCOde,
                    ErrorDescription = errorDescription
                };
                log.Add();
            }
            catch (Exception)
            {

            }
        }

        public static void WriteExceptionLog()
        {

        }
    }
}
