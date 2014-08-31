using System;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Timers;
using Wine.Util;

namespace Wine.WindowsService
{
    public partial class AutoGrapWineData : ServiceBase
    {
        private Timer _mTimer = null;

        private static long _times = 0;

        private const string LogSource = "51WineWindowsService";

        public AutoGrapWineData()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //启动轮询服务
                _mTimer = _mTimer ?? new Timer();
                _mTimer.Interval = 5 * 60 * 1000;
                _mTimer.Elapsed += timer_Elapsed;
                _mTimer.Start();
                //IIS启动时候就开始轮询
                DownloadData();
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DownloadData();
        }

        private void DownloadData()
        {
            try
            {
                _times++;
                WriteInformation(string.Format("WindowsService download data begin;Times:{0}", _times));
                new Data.WineManager().DownloadData();
                WriteInformation(string.Format("WindowsService download data end;Times:{0}", _times));

                Request51WineWapSite();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(LogSource, ex.Message, EventLogEntryType.Error);
            }
        }

        private void Request51WineWapSite()
        {
            WriteInformation(string.Format("WindowsService auto visit wapSite begin;Times:{0}", _times));
            WebClient client = new WebClient();
            client.DownloadString(ConfigManager.WapSiteUrl);
            WriteInformation(string.Format("WindowsService auto visit wapSite end;Times:{0}", _times));
        }

        #region 事件日志

        private void WriteInformation(string message)
        {
            //if (!EventLog.SourceExists(LogSource))
            //    EventLog.CreateEventSource(LogSource, "Application");
            //EventLog.WriteEntry(LogSource, message, EventLogEntryType.Information);
        }

        private void WriteError(string message)
        {
            //if (!EventLog.SourceExists(LogSource))
            //    EventLog.CreateEventSource(LogSource, "Application");
            //EventLog.WriteEntry(LogSource, message, EventLogEntryType.Error);
        }

        #endregion

        protected override void OnStop()
        {
        }
    }
}
