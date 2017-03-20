using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation.Events
{
    class StartPayment : SimEventShop
    {
        /** Začiatk platenia
              1. Vytvorím si inštanciu Exit.
              2. Nastavím si simulačný čas. 
              3. Zavolám si generátor => Vygenerujem 5 minút. 
              4. Nastavím simulačný čas na aktuálny čas. 
              5. Naplánujem čas. 
              6. Vložím do prioritného frontu. 
              7. Vytvorím inštanciu Customer. 

            Instancia triedy Customers - musím nastaviť referenciu zákazníka na toho, 
            ktorého plánujem odchod. 
            Posúvam referenciu, aby zákazník skutočne odišiel. 
            Pribudne atribúút IsAvailable... 
        */
        public override void Execute()
        {
            SimCoreShop core = ((SimCoreShop) (this.ReferenceSimCore));
            //new event - exit of customer
            //1.
            Exit ex = new Exit();
            Console.WriteLine(this.EventTime);
            //2.
            //3. 
            //set time of event -> genrate from random number 
            ex.EventTime = (float)(this.EventTime + core.Generators[0].GenerateDouble());

            if (this.CurrentCustomer?.ArrivalTimeToSystem==0)
                Console.WriteLine("StartPayment - Current Customer has arrival time to system to set to zero. ");
            //4.
            core.CurrentTime += ex.EventTime;
            //5.
            //6.
            //put on timeline
            core.PlanEvent(ex);
            //7.
            Customer customer = new Customer();
            customer.ArrivalTimeToSystem = core.CurrentTime;
            ex.CurrentCustomer = customer;
            //
            ex.ReferenceSimCore = this.ReferenceSimCore;
            Console.WriteLine(core.CurrentTime + "\tCustomer exit event.");

            //set is served to true
            core.IsServed = true;
        }
    }
}
