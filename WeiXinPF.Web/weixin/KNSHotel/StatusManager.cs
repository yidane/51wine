using System;

namespace WeiXinPF.Web.weixin.KNSHotel
{



    public class StatusManager
    {

        /// <summary>
        /// 订单中菜状态
        /// </summary>
        public class OrderStatus
        {
            /// <summary>
            /// 待确认
            /// </summary>
            public static StatusDict Pending = new StatusDict() { StatusId = 0, StatusName = "待确认", CssClass= "no" };

            /// <summary>
            /// 已拒绝
            /// </summary>
            public static StatusDict Refused = new StatusDict() { StatusId = 2, StatusName = "已拒绝", CssClass = "error" };

            /// <summary>
            /// 已接收
            /// </summary>
            public static StatusDict Accepted = new StatusDict() { StatusId = 1, StatusName = "已接受", CssClass = "ok" };

            /// <summary>
            /// 已支付
            /// </summary>
            public static StatusDict Payed = new StatusDict() { StatusId = 3, StatusName = "已支付", CssClass = "ok" };

            /// <summary>
            /// 已取消
            /// </summary>
            public static StatusDict Cancelled = new StatusDict() { StatusId = 4, StatusName = "已取消", CssClass = "no" };

            /// <summary>
            /// 拒绝退款
            /// </summary>
//            public static StatusDict RefusedRefund = new StatusDict() { StatusId = 5, StatusName = "拒绝退款", CssClass = "error" };
            /// <summary>
            /// 退款中
            /// </summary>
            public static StatusDict Refunding = new StatusDict() { StatusId = 6, StatusName = "退款中", CssClass = "no" };

            /// <summary>
            /// 退款完成
            /// </summary>
            public static StatusDict Refunded = new StatusDict() { StatusId = 7, StatusName = "退款完成", CssClass = "ok" };

            /// <summary>
            /// 订单完成
            /// </summary>
            public static StatusDict Completed = new StatusDict() { StatusId = 8, StatusName = "订单完成", CssClass = "ok" };



            public static StatusDict GetStatusDict(int statusId)
            {
                switch (statusId)
                {
                    case 0:
                        return Pending;
                    case 1:
                        return Refused;
                    case 2:
                        return Accepted;
                    case 3:
                        return Payed;
                    case 4:
                        return Cancelled;
                    case 5:
//                        return RefusedRefund;
                    case 6:
                        return Refunding;
                    case 7:
                        return Refunded;
                    case 8:
                        return Completed;
                    default:
                        throw new Exception("未知状态");
                }
            }
        }
    }

    public struct StatusDict
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string CssClass { get; set; }
    }
}