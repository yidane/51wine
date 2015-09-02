using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicketStatus
{
    using System.Threading;

    using Travel.Application.DomainModules.Order.Service;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3秒钟后启动。");
            //Timer tmr = new Timer(doWork, "获取门票状态......", 3000, 
            //    10000);

            var service = new OrderService();
            Console.WriteLine("开始获取票务状态。");
            try
            {
                service.SearchTicketStatus(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine(ex.StackTrace);
                //throw;
            }

            Console.WriteLine("按任意键退出.");
            Console.ReadLine();
            Console.WriteLine("按任意键确认退出.");
            Console.ReadLine();
            //tmr.Dispose();
        }

        private static void doWork(object data)
        {
            var service = new OrderService();
            Console.WriteLine("开始获取票务状态。");
            service.SearchTicketStatus(100);
        }
    }
}
