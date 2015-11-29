using System;
namespace WeiXinPF.Model
{
    /// <summary>
    /// 房间设置
    /// </summary>
    [Serializable]
    public partial class wx_hotel_room
    {
        public wx_hotel_room()
        { }
        #region Model
        private int _id;
        private int? _hotelid;
        private string _roomtype;
        private string _indroduce;
        private decimal? _roomprice;
        private decimal? _saleprice;
        private string _facilities;
        private DateTime? _createdate;
        private int? _sortid;

        private string _roomCode;
        private string _useInstruction;
        private string _refundRule;
        private RoomStatus _status;
        private int? _createUser;

        /// <summary>
        /// 编号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 商家编号
        /// </summary>
        public int? hotelid
        {
            set { _hotelid = value; }
            get { return _hotelid; }
        }
        /// <summary>
        /// 房间类型
        /// </summary>
        public string roomType
        {
            set { _roomtype = value; }
            get { return _roomtype; }
        }
        /// <summary>
        /// 简要说明
        /// </summary>
        public string indroduce
        {
            set { _indroduce = value; }
            get { return _indroduce; }
        }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal? roomPrice
        {
            set { _roomprice = value; }
            get { return _roomprice; }
        }
        /// <summary>
        /// 优惠价
        /// </summary>
        public decimal? salePrice
        {
            set { _saleprice = value; }
            get { return _saleprice; }
        }
        /// <summary>
        /// 配套设施
        /// </summary>
        public string facilities
        {
            set { _facilities = value; }
            get { return _facilities; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }


        //        public int? createUser
        //        {
        //            get { return _createUser; }
        //            set { _createUser = value; }
        //        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? sortid
        {
            set { _sortid = value; }
            get { return _sortid; }
        }

        /// <summary>
        /// 商户编号
        /// </summary>
        public string RoomCode
        {
            get { return _roomCode; }
            set { _roomCode = value; }
        }

        /// <summary>
        /// 使用说明
        /// </summary>
        public string UseInstruction
        {
            get { return _useInstruction; }
            set { _useInstruction = value; }
        }

        /// <summary>
        /// 退单规则
        /// </summary>
        public string RefundRule
        {
            get { return _refundRule; }
            set { _refundRule = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public RoomStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DateTime? ExpiryDate_Begin { get; set; }
        public DateTime? ExpiryDate_End { get; set; }
        #endregion Model

    }

    public enum RoomStatus
    {
        /// <summary>
        /// 状态不确定
        /// </summary>
        None = 0,
        /// <summary>
        /// 提交审核
        /// </summary>
        Submit = 1,
        /// <summary>
        /// 审核通过
        /// </summary>
        Agree = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        Refuse = 3,

        /// <summary>
        ///  发布
        /// </summary>
        Publish = 4,

        /// <summary>
        /// 下架
        /// </summary>
        SoldOut = 5
    }
}

