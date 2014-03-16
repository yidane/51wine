using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Timers;

namespace WineWap
{
    public class Global : System.Web.HttpApplication
    {
        private WineDataCache oWineDataCache = new WineDataCache();

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                var UpdateCacheTimeSpan = System.Configuration.ConfigurationManager.AppSettings["UpdateCacheTimeSpan"];
                var timeSpanDouble = 0;
                if (!int.TryParse(UpdateCacheTimeSpan, out timeSpanDouble))
                {
                    timeSpanDouble = 60;
                }
                //启动轮询服务
                Timer timer = new Timer();
                timer.Interval = timeSpanDouble * 60 * 1000;
                timer.Elapsed += timer_Elapsed;
                timer.Start();
                //IIS启动时候就开始轮询

                this.oWineDataCache.InitData();
            }
            catch (Exception ex)
            {
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.oWineDataCache.InitData();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}