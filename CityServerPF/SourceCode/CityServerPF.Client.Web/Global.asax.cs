using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

    using NServiceBus;

    public class Global : System.Web.HttpApplication
    {
        public static IBus Bus;

        public override void Dispose()
        {
            if (Bus != null)
            {
                Bus.Dispose();
            }
            base.Dispose();
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            var configuration = new BusConfiguration();
            configuration.EndpointName("CityServerPF.Client.Web");
        //configuration.ApplyCommonConfiguration();

        configuration.UseTransport<MsmqTransport>();
        configuration.UsePersistence<InMemoryPersistence>();
        configuration.UseSerialization<JsonSerializer>();
        configuration.Conventions()
            .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("CityServerPF") && t.Namespace.EndsWith("Commands"))
            .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("CityServerPF") && t.Namespace.EndsWith("Events"))
            .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("CityServerPF") && t.Namespace.EndsWith("RequestResponse"))
            .DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        configuration.RijndaelEncryptionService();
        configuration.EnableInstallers();

        Bus = NServiceBus.Bus.Create(configuration).Start();
        }
    }