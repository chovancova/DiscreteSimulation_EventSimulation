using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class OdchodZakaznikaEvent : AutoserviceEvent
    {
        public OdchodZakaznikaEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //nastavim statistiku na odchod zakaznika zo systemu

        }
    }
}
