using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class Exit : SimEventShop
    {
        public override void Execute()
        {
            SimCoreShop core = ((SimCoreShop) (this.ReferenceSimCore));

            //customer is leaving, so cash is free
            if (core.WaitingQueue.Count == 0)
            {
                core.IsServed = false;
            }
            else
            {
                //new customer start paying for .. 
                StartPayment sp = new StartPayment();
                sp.CurrentCustomer = core.WaitingQueue.First();
                core.WaitingQueue.RemoveFirst();
                sp.EventTime = this.EventTime;
                this.ReferenceSimCore.PlanEvent(sp);
            }
            //for all
            this.CurrentCustomer.ArrivalTimeToSystem = this.EventTime - this.CurrentCustomer.ArrivalTimeToSystem;
        }
    }
}
