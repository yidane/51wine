using System;
namespace WeiXinPF.Model
{
    /// <summary>
    /// 订单对应的菜品表
    /// </summary>
    [Serializable]
    public partial class wx_diancai_dingdan_caiping
    {
        public wx_diancai_dingdan_caiping()
        { }
        #region Model
        private int _id;
        private int? _dingid;
        private int? _caiid;
        private int? _num;
        private decimal? _price;
        private decimal? _totpric;
        /// <summary>
        /// 编号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单id
        /// </summary>
        public int? dingId
        {
            set { _dingid = value; }
            get { return _dingid; }
        }
        /// <summary>
        /// 菜品主键id
        /// </summary>
        public int? caiId
        {
            set { _caiid = value; }
            get { return _caiid; }
        }
        /// <summary>
        /// 购买份数
        /// </summary>
        public int? num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 总价格
        /// </summary>
        public decimal? totpric
        {
            set { _totpric = value; }
            get { return _totpric; }
        }
        #endregion Model

    }

    //订餐商品状态
    public enum RestaurantCommodityStatus
    {
        /// <summary>
        /// 未支付
        /// </summary>
        Unpaid = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 1,
        /// <summary>
        /// 已使用
        /// </summary>
        Used = 2,
        /// <summary>
        /// 申请退款
        /// </summary>
        ApplyRefund = 3,
        /// <summary>
        /// 已退款
        /// </summary>
        Refund = 4
    }
}

