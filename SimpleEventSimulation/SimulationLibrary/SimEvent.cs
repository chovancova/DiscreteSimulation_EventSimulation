using Priority_Queue;

namespace SimulationLibrary
{
    /// <summary>
    /// Abstrakná simulačná udalosť. 
    /// </summary>
  public abstract class SimEvent : FastPriorityQueueNode
    {
        /// <summary>
        /// Čas vykonania udalosti. Slúži aj ako priorita. 
        /// </summary>
        public double EventTime { get; set; }
        /// <summary>
        /// Abstraktná metóda na vykonanie udalosti. 
        /// </summary>
        public abstract void Execute();
        protected SimCore ReferenceSimCore { get; }

        protected SimEvent(double eventTime, SimCore simulation)
        {
            EventTime =  eventTime;
            ReferenceSimCore = simulation;
        }
    }
}
