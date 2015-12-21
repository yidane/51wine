using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using NServiceBus;


    class Hosting
    {
        static void Main()
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("CityServerPF.Server.WaterSupply");
            busConfiguration.ApplyCommonConfiguration();
            using (IBus bus = Bus.Create(busConfiguration).Start())
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
