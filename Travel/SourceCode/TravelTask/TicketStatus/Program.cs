using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.CommonFunctions;

namespace TicketStatus
{
    using System.Threading;

    using Travel.Application.DomainModules.Order.Service;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3秒钟后启动。");



            var windowsTimeSpan = WebConfigureHelper.Appsettings.WindowsConfig.WindowsTimeSpan;

            var tmr = new Timer(doWork, "获取门票状态......", 3000, windowsTimeSpan * 60 * 1000);
            var tmr2 = new Timer(ReleaseOvertimeOrders, 30 * 60, 3000, 30 * 60 * 1000);

            Console.WriteLine("按任意键退出.");
            Console.ReadLine();
            Console.WriteLine("按任意键确认退出.");
            Console.ReadLine();
            tmr.Dispose();
            tmr2.Dispose();

        }

        private static void doWork(object data)
        {
            var service = new OrderService();
            Console.WriteLine("开始获取票务状态。");
            try
            {
                service.SearchTicketStatus(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：");
                Console.WriteLine(ex.Message);
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine(ex.StackTrace);
                //throw;
            }
        }

        private static void ReleaseOvertimeOrders(object data)
        {
            var service = new OrderService();
            Console.WriteLine("开始处理超时订单。");
            try
            {
                service.OrderReleaseProcess(int.Parse(data.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：");
                Console.WriteLine(ex.Message);
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine(ex.StackTrace);
                //throw;
            }
        }
    }
}
