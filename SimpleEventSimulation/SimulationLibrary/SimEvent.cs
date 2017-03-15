using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SimulationLibrary
{
  public abstract class SimEvent : FastPriorityQueueNode
    {
        public float EventTime { get; set; }
        public abstract void Execute();
        public SimCore ReferenceSimCore { get; set; }
    }
}
