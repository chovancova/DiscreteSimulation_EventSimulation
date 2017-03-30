using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class SpracovanieObjednavky : AutoserviceEvent
    {
        public SpracovanieObjednavky(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
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
