using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityServerPF.Messages.Commands;
using CityServerPF.Shared;
using NServiceBus;


    public class CheckRestMoneyHandler:IHandleMessages<CheckRestMoney>
    {
        IBus bus;

        public CheckRestMoneyHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(CheckRestMoney message)
        {
        if (DebugFlagMutator.Debug)
        {
            Debugger.Break();
        }

        Console.WriteLine("Process CheckRestMoneyCommand");
        }
    }
