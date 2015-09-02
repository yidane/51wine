using System;
namespace MxWeiXinPF.Model
{
	/// <summary>
	/// 微支付接口表
	/// </summary>
	[Serializable]
	public partial class wx_payment_wxpay
	{
		public wx_payment_wxpay()
		{}
		#region Model
		private int _id;
		private int? _wid;
		private string _mch_id;
		private string _paykey;
		private string _certinfopath;
		private string _cerinfopwd;
		private string _remark;
		private bool _quicklyfh;
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
		/// 微帐号id
		/// </summary>
		public int? wid
		{
			set{ _wid=value;}
			get{return _wid;}
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
		/// 商户支付密钥key
		/// </summary>
		public string paykey
		{
			set{ _paykey=value;}
			get{return _paykey;}
		}
		/// <summary>
		/// 证书地址
		/// </summary>
		public string certInfoPath
		{
			set{ _certinfopath=value;}
			get{return _certinfopath;}
		}
		/// <summary>
		/// 证书密码
		/// </summary>
		public string cerInfoPwd
		{
			set{ _cerinfopwd=value;}
			get{return _cerinfopwd;}
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
		/// 立即发货
		/// </summary>
		public bool quicklyFH
		{
			set{ _quicklyfh=value;}
			get{return _quicklyfh;}
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

