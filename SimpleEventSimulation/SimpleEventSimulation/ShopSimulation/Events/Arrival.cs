using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class Arrival : SimEventShop
    {
       public Arrival(double eventTime, SimCore simulation, Customer currentCustomer) : base(eventTime, currentCustomer, simulation)
        {
        }
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
            //1. 
            var time = (this.EventTime + this.ReferenceSimCore.Generators[1].GenerateDouble());
            //initializaiton of new event - arrival of Customer
            var newEvent = new Arrival(time, ReferenceSimCore, new Customer());
            //2.
            //planning of event on timeline
            ReferenceSimCore.ScheduleEvent(newEvent, time);
           
            //////////3.
            //////////set current customer time of his/her arrival to the system. 
            ////////this.

            //4.
            //a.
            if (((SimCoreShop)ReferenceSimCore).IsServed)
            {
                //3.
                //set current customer time of his/her arrival to the system. 
                CurrentCustomer.StartWaiting(EventTime);
                //add him to waiting queue as last 
                ((SimCoreShop)ReferenceSimCore).AddCustomer(this.CurrentCustomer);

             //   Console.WriteLine(core.CurrentTime+"\tCustomer is wainting in queue." );
            }
            else
            {
                //b.
                //b.1.
              //  core.IsServed = true;
                //b.2.
                //do other event    
                //customer is paying for his newspapers
                //initialization of new event - start of payment
                StartPayment sp = new StartPayment(EventTime, ReferenceSimCore, CurrentCustomer);

                //b.3.
                //plan this event
                ((SimCoreShop)ReferenceSimCore).ScheduleEvent(sp, EventTime);

              //  Console.WriteLine(core.CurrentTime + "\tCustomer started paying.");
            }
        }

      
    }
}
