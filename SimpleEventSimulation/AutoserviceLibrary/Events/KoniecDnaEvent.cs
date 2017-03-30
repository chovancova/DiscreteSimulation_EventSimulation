using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class KoniecDnaEvent : AutoserviceEvent
    {
        public KoniecDnaEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //pozbieram statistiky
            //vynulujem den 
            //znova naplanujem
            throw new NotImplementedException();
        }
    }
}
