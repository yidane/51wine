using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Application.DomainModules.Order.Core;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Presentation.Web.Message
{
    public partial class MessageNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.Equals(Request.HttpMethod, "POST", StringComparison.CurrentCultureIgnoreCase))
                    throw new Exception("Error HttpMethod");

                var s = Request.InputStream;
                var count = 0;
                var buffer = new byte[1024];
                var builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Close();
                s.Dispose();

                var paymentNotify = new PaymentNotify(builder.ToString());

                bool isSucceed = false;
                var response = paymentNotify.Report(out isSucceed);
                if (isSucceed)
                {
                    //后台反馈微信信息
                    var wxPay = new WXPaymentOperate();
                    wxPay.GetPaymentCompleteInfomation(paymentNotify);
                }

                ReturnResponse(response);
            }
            catch (Exception exception)
            {
                var errorResponse = PaymentNotify.ReportError(exception.Message);
                ReturnResponse(errorResponse);
            }
        }

        private void ReturnResponse(string response)
        {
            Response.Clear();
            Response.Write(response);
            Response.Flush();
            Response.Close();
        }
    }
}