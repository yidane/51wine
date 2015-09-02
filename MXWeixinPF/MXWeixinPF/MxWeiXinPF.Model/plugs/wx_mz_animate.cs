using System;
namespace MxWeiXinPF.Model
{
	/// <summary>
	/// wx_mz_animate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_mz_animate
	{
		public wx_mz_animate()
		{}
		#region Model
		private int _id;
		private int? _iid;
		private decimal? _width;
		private decimal? _height;
		private decimal? _x_loaction;
		private decimal? _y_location;
		private string _animate_type;
		private decimal? _start_seconds;
		private decimal? _continue_seconds;
		private string _animate_img;
		private DateTime? _createdate;
		private int? _sort_id;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? iid
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? height
		{
			set{ _height=value;}
			get{return _height;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? x_loaction
		{
			set{ _x_loaction=value;}
			get{return _x_loaction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? y_location
		{
			set{ _y_location=value;}
			get{return _y_location;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string animate_type
		{
			set{ _animate_type=value;}
			get{return _animate_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? start_seconds
		{
			set{ _start_seconds=value;}
			get{return _start_seconds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? continue_seconds
		{
			set{ _continue_seconds=value;}
			get{return _continue_seconds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string animate_img
		{
			set{ _animate_img=value;}
			get{return _animate_img;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createdate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sort_id
		{
			set{ _sort_id=value;}
			get{return _sort_id;}
		}
		#endregion Model

	}
}

