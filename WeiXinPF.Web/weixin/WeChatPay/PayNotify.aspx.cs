using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OneGulp.WeChat.MP.TenPayLibV3;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class PayNotify : System.Web.UI.Page
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

                WriteLog(builder.ToString());

                var paymentNotify = new PaymentNotify(builder.ToString());

                bool isSucceed = false;
                var response = paymentNotify.Report(out isSucceed);
                if (isSucceed)
                {
                    WriteLog("report Success");
                    //后台反馈微信信息
                    //var wxPay = new WXPaymentOperate();
                    //wxPay.GetPaymentCompleteInfomation(paymentNotify);
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
            WriteLog(response);

            Response.Clear();
            Response.Write(response);
            //Response.Flush();
            //Response.Close();
        }

        protected static void WriteLog(string content)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            var filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            var mySw = File.AppendText(filename);

            mySw.WriteLine(time + "   |" + content + Environment.NewLine);

            //关闭日志文件
            mySw.Close();
        }
    }
}