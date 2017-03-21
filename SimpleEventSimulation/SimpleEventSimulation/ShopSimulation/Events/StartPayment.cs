using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

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
            //set is served to true
            ((SimCoreShop)(this.ReferenceSimCore)).IsServed = true;
            //1.
            //2.
            var waitingTime = CurrentCustomer.EndWaiting(EventTime);
            //for statistics
            ((SimCoreShop)(this.ReferenceSimCore)).AddStatisticsWaitingTimeStat(waitingTime);
            //3. 
            //4.
            //set time of event -> genrate from random number 
            var time = this.EventTime + ((SimCoreShop)(this.ReferenceSimCore)).Generators[0].GenerateDouble();
            //5.
            //6. 
            //7. 
            Exit ex = new Exit(time, ReferenceSimCore, CurrentCustomer);
            ReferenceSimCore.ScheduleEvent(ex,time);
         }

        public StartPayment(double eventTime, SimCore simulation, Customer currentCustomer) : base(eventTime, simulation, currentCustomer)
        {
        }
    }
}
