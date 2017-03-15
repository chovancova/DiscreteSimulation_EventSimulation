using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class StartPayment : SimEventShop
    {
        public override void Execute()
        {
            SimCoreShop core = ((SimCoreShop) (this.ReferenceSimCore));
            //new event - exit of customer
            Exit ex = new Exit();
            ex.CurrentCustomer = this.CurrentCustomer;
            ex.ReferenceSimCore = this.ReferenceSimCore;

            //set time of event -> genrate from random number 
            ex.EventTime = (float)(this.EventTime + core.Generators[0].GenerateDouble());
            //put on timeline
            core.PlanEvent(ex);
            //set is served to true
            core.IsServed = true;
        }
    }
}
