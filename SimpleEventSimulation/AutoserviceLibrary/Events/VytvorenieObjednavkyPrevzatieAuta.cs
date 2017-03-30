using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class VytvorenieObjednavkyPrevzatieAuta : AutoserviceEvent
    {
        public VytvorenieObjednavkyPrevzatieAuta(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //zapocitam zakaznikovi statistiku casu potrebneho na vybavenie objednavky

            //vygenerujem cas prepravkovania TROJ
            //naplanujem cinnost preparkovanie auto pred dielnou 

            //zakaznika posuniem dalej s instanciou auta... 


        }
    }
}
