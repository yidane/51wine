namespace WeiXinPF.WeiXinComm
{
    using System;
    using System.Linq;
    using System.Web;

    public class WebHelper
	{   
		/// <summary>
		/// 转向
		/// </summary>
		/// <param name="url">转向的url</param>
		public static void RedirectUrl(string url)
		{
			HttpContext.Current.Response.Redirect(MapUrl(url));
		}

		public static void RedirectUrlWithOutHostPort(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		/// <summary>
		/// 得到站点地址
		/// </summary>
		/// <returns></returns>
		public static string GetHostPort()
		{
			if (HttpContext.Current.Request.Url.Port == 80)
				return string.Format("http://{0}{1}/",
				HttpContext.Current.Request.Url.Host,
				HttpContext.Current.Request.ApplicationPath.TrimEnd('/'));
			else
				return string.Format("http://{0}:{1}{2}/",
					HttpContext.Current.Request.Url.Host,
					HttpContext.Current.Request.Url.Port,
					HttpContext.Current.Request.ApplicationPath.TrimEnd('/'));
		}

		/// <summary>
		/// 得到完整路径
		/// </summary>
		/// <param name="url">相对url</param>
		/// <returns></returns>
		public static string MapUrl(string url)
		{
			return string.Format("{0}{1}", GetHostPort(), url);
		}

		/// <summary>
		/// 得到完整物理路径
		/// </summary>
		/// <returns></returns>
		public static string GetBaseDirectory()
		{
			return string.Format(@"{0}", AppDomain.CurrentDomain.BaseDirectory);
		}

		/// <summary>
		/// 得到物理文件路径
		/// </summary>
		/// <param name="path">相对地址</param>
		/// <returns></returns>
		public static string MapPath(string path)
		{
			return string.Format(@"{0}{1}", GetBaseDirectory(), path);
		}

		public static string GetQueryString(string key)
		{
			if (HttpContext.Current.Request.QueryString[key] == null)
			{ return string.Empty; }
			else
			{ return HttpContext.Current.Request.QueryString[key]; }
		}

        public static string GetPageName()
        {
            return HttpContext.Current.Request.Path.Split('/').Last();
        }
	}
}