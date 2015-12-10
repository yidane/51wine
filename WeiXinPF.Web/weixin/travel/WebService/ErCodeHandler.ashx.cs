using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.Web.WebService
{
    /// <summary>
    /// ErCodeHandler 的摘要说明
    /// </summary>
    public class ErCodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var key = HttpContext.Current.Request.QueryString["key"];
            if (!string.IsNullOrEmpty(key))
            {
                var ercode = QrCodeHelper.GetQrCodeImage(key);

                HttpContext.Current.Response.OutputStream.Write(ercode.GetBuffer(), 0, (int)ercode.Length);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}