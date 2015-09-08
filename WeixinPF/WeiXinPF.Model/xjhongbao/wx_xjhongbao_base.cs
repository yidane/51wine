using System;
namespace WeiXinPF.Model
{
	/// <summary>
	/// 红包基本设置
	/// </summary>
	[Serializable]
	public partial class wx_xjhongbao_base
	{
		public wx_xjhongbao_base()
		{}
		#region Model
		private int _id;
		private int? _wid;
		private int? _accountbalance;
		private int? _totallqmoney;
		private string _warninginfo;
		private string _remark;
		private DateTime? _createdate;
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
		/// 账号余额（分）
		/// </summary>
		public int? accountBalance
		{
			set{ _accountbalance=value;}
			get{return _accountbalance;}
		}
		/// <summary>
		/// 领取的金额总计（分）
		/// </summary>
		public int? totalLQMoney
		{
			set{ _totallqmoney=value;}
			get{return _totallqmoney;}
		}
		/// <summary>
		/// 警告信息
		/// </summary>
		public string warningInfo
		{
			set{ _warninginfo=value;}
			get{return _warninginfo;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? createDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}

