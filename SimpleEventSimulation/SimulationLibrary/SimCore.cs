using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using RandomGenerators;
using RandomGenerators.Generators;

namespace SimulationLibrary
{
   public class SimCore
    {
        public float CurrentTime { get; set; }
        public FastPriorityQueue<SimEvent> TimeLine { get; set; }

        public IGenerators[] Generators { get; set; }
        
        public SimCore(IGenerators[] generators)
        {
            TimeLine = new FastPriorityQueue<SimEvent>(214748364);
            CurrentTime = 0.0f;
            Generators = generators;
        }

        protected SimCore()
        {
            TimeLine = new FastPriorityQueue<SimEvent>(214748364);
            CurrentTime = 0.0f;
        }

        public void PlanEvent(SimEvent eSimEvent)
        {
            TimeLine.Enqueue(eSimEvent, CurrentTime);
        }

        public void Simulate(double maxSimulationRunTime = float.MaxValue/2)
        {
            SimEvent currentEvent;
            CurrentTime = 0.0f;

            while ((TimeLine.Count!=0))
            {
                //initialization of current event, time
                currentEvent = TimeLine.First;

                if (CurrentTime <= maxSimulationRunTime)
                {
                    //execution of event
                    currentEvent.Execute(); //virtual method. 
                }
                else
                {
                    break;
                }
                //here add - acceleration, deceleration, suspension, animation
            }
        }
        
        public void Simulate(double simulationTime, SimEvent helpEvent)
        {
            while (true)
            {
                if (TimeLine.Count == 0) break;
                if (simulationTime <= CurrentTime) break;

                helpEvent = TimeLine.First;
                CurrentTime = helpEvent.EventTime;
                helpEvent.Execute();
            }
        }
    }
}
