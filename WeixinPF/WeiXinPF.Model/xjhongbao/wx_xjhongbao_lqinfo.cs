using System;
namespace WeiXinPF.Model
{
	/// <summary>
	/// 现金红包领取记录表
	/// </summary>
	[Serializable]
	public partial class wx_xjhongbao_lqinfo
	{
		public wx_xjhongbao_lqinfo()
		{}
		#region Model
		private int _id;
		private int? _wid;
		private int? _actionid;
		private string _openid;
		private string _usernick;
		private int? _total_amount;
		private DateTime? _send_time;
		private string _mch_billno;
		private string _mch_id;
		private string _detail_id;
		private string _hbstatus;
		private string _send_type;
		private string _hb_type;
		private string _reason;
		private DateTime? _refund_time;
		private DateTime? _createdate;
		private string _remark;
		/// <summary>
		/// 编号
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 微账号id
		/// </summary>
		public int? wid
		{
			set{ _wid=value;}
			get{return _wid;}
		}
		/// <summary>
		/// 活动id
		/// </summary>
		public int? actionId
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 粉丝用户的openid
		/// </summary>
		public string openid
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 用户昵称
		/// </summary>
		public string userNick
		{
			set{ _usernick=value;}
			get{return _usernick;}
		}
		/// <summary>
		/// 获得红包金额（分）
		/// </summary>
		public int? total_amount
		{
			set{ _total_amount=value;}
			get{return _total_amount;}
		}
		/// <summary>
		/// 红包发送时间
		/// </summary>
		public DateTime? Send_time
		{
			set{ _send_time=value;}
			get{return _send_time;}
		}
		/// <summary>
		/// 商户订单号
		/// </summary>
		public string mch_billno
		{
			set{ _mch_billno=value;}
			get{return _mch_billno;}
		}
		/// <summary>
		/// 商户号
		/// </summary>
		public string mch_id
		{
			set{ _mch_id=value;}
			get{return _mch_id;}
		}
		/// <summary>
		/// 红包单号
		/// </summary>
		public string detail_id
		{
			set{ _detail_id=value;}
			get{return _detail_id;}
		}
		/// <summary>
		/// 红包状态 SENDING:发放中 SENT:已发放待领取 FAILED：发放失败 RECEIVED:已领取 REFUND:已退款
		/// </summary>
		public string hbstatus
		{
			set{ _hbstatus=value;}
			get{return _hbstatus;}
		}
		/// <summary>
		/// 发放类型 API:通过API接口发放 UPLOAD:通过上传文件方式发放 ACTIVITY:通过活动方式发放
		/// </summary>
		public string send_type
		{
			set{ _send_type=value;}
			get{return _send_type;}
		}
		/// <summary>
		/// 红包类型 GROUP:裂变红包 NORMAL:普通红包
		/// </summary>
		public string hb_type
		{
			set{ _hb_type=value;}
			get{return _hb_type;}
		}
		/// <summary>
		/// 失败原因
		/// </summary>
		public string reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 红包退款时间 红包的退款时间（如果其未领取的退款）
		/// </summary>
		public DateTime? Refund_time
		{
			set{ _refund_time=value;}
			get{return _refund_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

