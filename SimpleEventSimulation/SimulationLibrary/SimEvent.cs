using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SimulationLibrary
{
  public abstract class SimEvent 
    {
        public double EventTime { get; set; }
        public abstract void Execute();
        public SimCore ReferenceSimCore { get; set; }

        protected SimEvent(double eventTime, SimCore simulation)
        {
            EventTime =  eventTime;
            ReferenceSimCore = simulation;
        }
    }
}
