using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class ZaciatokSimulacieEvent : AutoserviceEvent
    {
        public ZaciatokSimulacieEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //naplanujem koniec dna 
            //naplanujem prichod zakaznika
            
        }
    }
}
