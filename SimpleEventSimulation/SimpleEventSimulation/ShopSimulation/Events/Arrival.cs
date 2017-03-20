using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class Arrival : SimEventShop
    {
      /**
        * Príchod zákazníka
            1. Naplánuem príchod ďalšieho zákazníka. (Nemá inštancie)
            2. Naplanujem na časovú os. 
            3. Naplánujem príchod, referenciujem nového zákaznika. 
            4. Vytvorím príchod samého seba. 

         Príznak IsServed
           a. TRUE - vložím do frontu referenciu zákaznika. 
           b. FALSE 
                - 1. nastavím na TRUE
                - 2. vytvorím inštanciu StartPayment
                - 3. naplánujem si udalosť s aktuálnym simulačným časom. 
        *   
        **/
        public override void Execute()
        {
            SimCoreShop core = ((SimCoreShop) (this.ReferenceSimCore));
            //initializaiton of new event - arrival of Customer
            var newEvent = new Arrival();
            //1. 
            newEvent.EventTime = (float) (this.EventTime + this.ReferenceSimCore.Generators[1].GenerateDouble());
            newEvent.ReferenceSimCore = this.ReferenceSimCore;
            //2.
            //planning of event on timeline
            this.ReferenceSimCore.PlanEvent(newEvent);
            //3.
            //set current customer time of his/her arrival to the system. 
            this.CurrentCustomer.ArrivalTimeToSystem = this.EventTime;
            //4.
            //a.
            if (core.IsServed)
            {
                //add him to waiting queue as last 
                core.WaitingQueue.AddLast(this.CurrentCustomer);
                Console.WriteLine(core.CurrentTime+"\tCustomer is wainting in queue." );
            }
            else
            {
                //b.
                //b.1.
                core.IsServed = true;
                //b.2.
                //do other event    
                //customer is paying for his newspapers
                StartPayment sp = new StartPayment();
                //initialization of new event - start of payment
                sp.ReferenceSimCore = this.ReferenceSimCore;
                sp.CurrentCustomer = this.CurrentCustomer;
                //set event time to this
                sp.EventTime = this.EventTime + 0; 

                //b.3.
                //plan this event
                sp.ReferenceSimCore.PlanEvent(sp);

                core.Customers++;
                core.Statistic.Increment(sp.EventTime);

                Console.WriteLine(core.CurrentTime + "\tCustomer started paying.");
            }
        }
    }
}
