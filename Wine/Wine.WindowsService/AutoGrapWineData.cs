using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Wine.WindowsService
{
    public partial class AutoGrapWineData : ServiceBase
    {
        private Timer m_timer = null;

        public AutoGrapWineData()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //启动轮询服务
                m_timer = m_timer ?? new Timer();
                m_timer.Interval = 5 * 60 * 1000;
                m_timer.Elapsed += timer_Elapsed;
                m_timer.Start();
                //IIS启动时候就开始轮询
                new Data.WineManager().DownloadData();
            }
            catch (Exception ex)
            {
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            new Data.WineManager().DownloadData();
        }

        protected override void OnStop()
        {
        }
    }
}
