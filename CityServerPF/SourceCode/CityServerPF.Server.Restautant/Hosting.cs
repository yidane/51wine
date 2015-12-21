using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace CityServerPF.Server.Restautant
{
    public class Hosting
    {
        private static void Main()
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("CityServerPF.Server.Restautant");
            busConfiguration.ApplyCommonConfiguration();
            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
