using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class Arrival : SimEventShop
    {
        public override void Execute()
        {
            SimCoreShop core = ((SimCoreShop) (this.ReferenceSimCore));
            //initializaiton of new event - arrival of Customer
            var newEvent = new Arrival();
            newEvent.EventTime = (float) (this.EventTime + this.ReferenceSimCore.Generators[1].GenerateDouble());
            newEvent.ReferenceSimCore = this.ReferenceSimCore;

            //planning of event on timeline
            this.ReferenceSimCore.PlanEvent(newEvent);
            //set current customer time of his/her arrival to the system. 
            this.CurrentCustomer.ArrivalTimeToSystem = this.EventTime;

            if (core.IsServed)
            {
                //add him to waiting queue as last 
                core.WaitingQueue.AddLast(this.CurrentCustomer);
            }
            else
            {
                //do other event    
                //customer is paying for his newspapers
                StartPayment sp = new StartPayment();
                //initialization of new event - start of payment
                sp.ReferenceSimCore = this.ReferenceSimCore;
                sp.CurrentCustomer = this.CurrentCustomer;
                //set event time to this
                sp.EventTime = this.EventTime + 0; 
                //plan this event
                sp.ReferenceSimCore.PlanEvent(sp);
            }
        }
    }
}
