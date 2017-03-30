using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class PrichodZakaznikaEvent : AutoserviceEvent
    {
        public PrichodZakaznikaEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //zakaznika ak je front prazdny a ako aj je volny pracovnik, tak naplanujem event Zaciatok zadania objednavky
            //ak je vo fronte niekto, tak vlozim zakaznika do frontu cakajucich ludi na zadanie objednavky
            

            //naplanujem prichod noveho zakaznika - generator EXP(300)
            
        }
    }
}
