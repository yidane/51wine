using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.OTAWebService.Response
{
    public class GetAllOrderStatusResponse
    {
        public string OrderCode { get; set; }
        public string OrderStatus { get; set; }
        public string OrderStatusDesc
        {
            get
            {
                if (string.IsNullOrEmpty(OrderStatus))
                    return string.Empty;
                switch (OrderStatus.ToUpper())
                {
                    case "F":
                        return "已释放订单";
                    case "B":
                        return "作废";
                    case "R":
                        return "取消";
                    case "N":
                        return "未付款";
                    case "S":
                        return "已付款";
                    case "G":
                        return "已改签";
                    case "H":
                        return "已取纸质票";
                    case "T":
                        return "线上已检";
                    case "O":
                        return "线下已检";
                    case "M":
                        return "已退款";
                    case "E":
                        return "退款审核中";
                    case "A":
                        return "全部退票";
                    case "P":
                        return "部分退票";
                    default:
                        return string.Format("未知状态{0}", OrderStatus.ToUpper());
                }
            }
        }
        public int InCount { get; set; }
        public int RefundCount { get; set; }
    }
}