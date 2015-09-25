using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Travel.Application.DomainModules.Order.Service;

namespace TicketStatusService
{
    public partial class TickStatus : ServiceBase
    {
        private Timer m_timer = null;
        private static int m_LoopTimes = 0;

        public TickStatus()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //启动轮询服务
                m_timer = m_timer ?? new Timer();
                m_timer.Interval = 5 * 60 * 100;
                m_timer.Elapsed += timer_Elapsed;
                m_timer.Start();

                LogManager.WriteLog("服务日志", "启动服务");

                var service = new OrderService();
                service.SearchTicketStatus(100);

                LogManager.WriteLog("轮询日志", String.Format("轮询服务轮询第{0}次", m_LoopTimes));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("异常日志", ex.Message);
                LogManager.WriteLog("异常日志", "------------------------------------------------------------");
                LogManager.WriteLog("异常日志", ex.StackTrace);
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var service = new OrderService();
                service.SearchTicketStatus(100);

                m_LoopTimes = m_LoopTimes + 1;
                LogManager.WriteLog("轮训日志", String.Format("轮训服务轮训第{0}次", m_LoopTimes));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("异常日志", ex.Message);
                LogManager.WriteLog("异常日志", "------------------------------------------------------------");
                LogManager.WriteLog("异常日志", ex.StackTrace);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
