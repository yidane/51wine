using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinPF.Web.weixin.restaurant
{
    public class StatusManager
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public class PayStatus
        {
            /// <summary>
            /// 待支付
            /// </summary>
            public static StatusDict Prepay = new StatusDict() { StatusID = 0, StatusName = "等待支付" };

            /// <summary>
            /// 已支付
            /// </summary>
            public static StatusDict Payed = new StatusDict() { StatusID = 1, StatusName = "等待使用" };

            /// <summary>
            /// 部分使用
            /// </summary>
            public static StatusDict PartUsed = new StatusDict() { StatusID = 2, StatusName = "部分使用" };

            /// <summary>
            /// 全部使用
            /// </summary>
            public static StatusDict AllUsed = new StatusDict() { StatusID = 3, StatusName = "全部使用" };

            /// <summary>
            /// 部分退款
            /// </summary>
            public static StatusDict PartRefund = new StatusDict() { StatusID = 4, StatusName = "部分退款" };

            /// <summary>
            /// 全部退款
            /// </summary>
            public static StatusDict AllRefund = new StatusDict() { StatusID = 5, StatusName = "全部退款" };

            public static StatusDict GetStatusDict(int statusId)
            {
                switch (statusId)
                {
                    case 0:
                        return Prepay;
                    case 1:
                        return Payed;
                    case 2:
                        return PartUsed;
                    case 3:
                        return AllUsed;
                    case 4:
                        return PartRefund;
                    case 5:
                        return AllRefund;
                    default:
                        throw new Exception("未知订单状态");
                }
            }
        }

        /// <summary>
        /// 订单中菜状态
        /// </summary>
        public class DishStatus
        {
            /// <summary>
            /// 未使用
            /// </summary>
            public static StatusDict NoUsed = new StatusDict() { StatusID = 0, StatusName = "未使用" };

            /// <summary>
            /// 已使用
            /// </summary>
            public static StatusDict Used = new StatusDict() { StatusID = 1, StatusName = "已使用" };

            /// <summary>
            /// 退款审核中
            /// </summary>
            public static StatusDict PreRefund = new StatusDict() { StatusID = 2, StatusName = "退款审核中" };

            /// <summary>
            /// 已退款
            /// </summary>
            public static StatusDict Refund = new StatusDict() { StatusID = 3, StatusName = "已退款" };

            /// <summary>
            /// 退款失败
            /// </summary>
            public static StatusDict RefundFaild = new StatusDict() { StatusID = 4, StatusName = "退款失败" };

            public static StatusDict GetStatusDict(int statusId)
            {
                switch (statusId)
                {
                    case 0:
                        return NoUsed;
                    case 1:
                        return Used;
                    case 2:
                        return PreRefund;
                    case 3:
                        return Refund;
                    case 4:
                        return RefundFaild;
                    default:
                        throw new Exception("未知状态");
                }
            }
        }
    }

    public struct StatusDict
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}