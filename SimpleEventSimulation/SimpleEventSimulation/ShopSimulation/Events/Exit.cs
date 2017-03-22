using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class Exit : SimEventShop
    {
        public Exit(double eventTime, SimCore simulation, Customer currentCustomer) : base(eventTime, currentCustomer, simulation)
        {
        }
        /**
         * Odchod zákazníka
         * - môžu nastať tieto dve situácie
                a. niekto čaká
                b. je voľný
        */
        public override void Execute()
        {
            Customer customer = ((SimCoreShop)ReferenceSimCore).NextCustomer();
            ((SimCoreShop)ReferenceSimCore).IsServed = false;

            if (customer != null)
            {
                StartPayment st = new StartPayment(EventTime, ReferenceSimCore, customer);
                ReferenceSimCore.ScheduleEvent(st, EventTime);
                ((SimCoreShop)ReferenceSimCore).IsServed = true; 
            }
        }
    }
}
