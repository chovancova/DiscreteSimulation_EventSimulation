using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
   public abstract class AutoserviceEvent : SimEvent
    {
        //customer1, druhy, zakaznik, auto a pdodobne
        public AutoserviceEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

    }
}
