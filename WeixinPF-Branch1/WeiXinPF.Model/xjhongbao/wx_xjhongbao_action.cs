using System;
namespace WeiXinPF.Model
{
	/// <summary>
	/// 现金红包活动表
	/// </summary>
	[Serializable]
	public partial class wx_xjhongbao_action
	{
		public wx_xjhongbao_action()
		{}
        #region Model
        private int _id;
        private int? _wid;
        private int? _hbtype;
        private string _act_name;
        private int? _moneytype;
        private int? _min_value;
        private int? _max_value;
        private int? _totalmoney;
        private string _wishing;
        private string _nick_name;
        private string _send_name;
        private string _logo_imgurl;
        private string _client_ip;
        private string _share_content;
        private string _share_imgurl;
        private string _share_url;
        private DateTime? _begindate;
        private DateTime? _enddate;
        private string _actpic;
        private int? _totallqmoney;
        private int? _sort_id;
        private string _remark;
        private DateTime? _createdate;
        private int? _lqtype;
        private string _keywords;
        /// <summary>
        /// 编号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 微账号id
        /// </summary>
        public int? wid
        {
            set { _wid = value; }
            get { return _wid; }
        }
        /// <summary>
        /// 红包类型（0关注时红包，1关键词红包，2网页红包）
        /// </summary>
        public int? hbType
        {
            set { _hbtype = value; }
            get { return _hbtype; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string act_name
        {
            set { _act_name = value; }
            get { return _act_name; }
        }
        /// <summary>
        /// 金额类型（0定额红包，1随机红包）
        /// </summary>
        public int? moneyType
        {
            set { _moneytype = value; }
            get { return _moneytype; }
        }
        /// <summary>
        /// 红包最小金额（分）
        /// </summary>
        public int? min_value
        {
            set { _min_value = value; }
            get { return _min_value; }
        }
        /// <summary>
        /// 红包最大金额（分）
        /// </summary>
        public int? max_value
        {
            set { _max_value = value; }
            get { return _max_value; }
        }
        /// <summary>
        /// 活动总金额
        /// </summary>
        public int? totalMoney
        {
            set { _totalmoney = value; }
            get { return _totalmoney; }
        }
        /// <summary>
        /// 祝福语
        /// </summary>
        public string wishing
        {
            set { _wishing = value; }
            get { return _wishing; }
        }
        /// <summary>
        /// 提供方名称
        /// </summary>
        public string nick_name
        {
            set { _nick_name = value; }
            get { return _nick_name; }
        }
        /// <summary>
        /// 商户名称
        /// </summary>
        public string send_name
        {
            set { _send_name = value; }
            get { return _send_name; }
        }
        /// <summary>
        /// 商户logo的url
        /// </summary>
        public string logo_imgurl
        {
            set { _logo_imgurl = value; }
            get { return _logo_imgurl; }
        }
        /// <summary>
        /// 调用接口的机器 Ip 地址
        /// </summary>
        public string client_ip
        {
            set { _client_ip = value; }
            get { return _client_ip; }
        }
        /// <summary>
        /// 分享文案
        /// </summary>
        public string share_content
        {
            set { _share_content = value; }
            get { return _share_content; }
        }
        /// <summary>
        /// 分享的图片url
        /// </summary>
        public string share_imgurl
        {
            set { _share_imgurl = value; }
            get { return _share_imgurl; }
        }
        /// <summary>
        /// 分享链接
        /// </summary>
        public string share_url
        {
            set { _share_url = value; }
            get { return _share_url; }
        }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? beginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? endDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 活动的图片
        /// </summary>
        public string actPic
        {
            set { _actpic = value; }
            get { return _actpic; }
        }
        /// <summary>
        /// 总的领取金额（分）
        /// </summary>
        public int? totalLqMoney
        {
            set { _totallqmoney = value; }
            get { return _totallqmoney; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 领取的类型（0为只能领取一次，1为每天可领取一次）
        /// </summary>
        public int? lqType
        {
            set { _lqtype = value; }
            get { return _lqtype; }
        }
        /// <summary>
        /// 关键词
        /// </summary>
        public string keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        #endregion Model

	}
}

