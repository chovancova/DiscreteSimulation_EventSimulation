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
        public Statistics Statistic { get; set; }
        public virtual event EventHandler Refresh;
        public bool IsRunning { get; set; }
        public Double TimeEnd { get; set; }
        public Double Time { get; set; }

        public SimCore(IGenerators[] generators)
        {
            TimeLine = new FastPriorityQueue<SimEvent>(2144);
            CurrentTime = 0.0f;
            Generators = generators;
            Statistic = new Statistics();
            TimeEnd = 0;
            IsRunning = false;
        }

        protected SimCore()
        {
            TimeLine = new FastPriorityQueue<SimEvent>(2147);
            CurrentTime = 0.0f;
            Statistic = new Statistics();
            TimeEnd = 0;
            IsRunning = false;
        }

        public void PlanEvent(SimEvent eSimEvent)
        {
            TimeLine.Enqueue(eSimEvent, CurrentTime);
        }

        public void Simulate(double maxSimulationRunTime = float.MaxValue/2)
        {
            SimEvent currentEvent;
            CurrentTime = 0.0f;

            while (CurrentTime <= maxSimulationRunTime)
            {
                 if (TimeLine.Count == 0)
                {
                    break;
                }
                 //initialization of current event, time
                currentEvent = TimeLine.First();

                if (CurrentTime <= maxSimulationRunTime)
                {
                    //execution of event
                    currentEvent.Execute(); //virtual method. 
                    CurrentTime = currentEvent.EventTime;
                    Time = currentEvent.EventTime;
                }
                else
                {
                    break;
                }
               
                //here add - acceleration, deceleration, suspension, animation
               // OnRefresh();
               // Stop();
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

        public virtual void OnRefresh()
        {
            var handler = Refresh;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Stop()
        {
            TimeLine = new FastPriorityQueue<SimEvent>(214748364);
            CurrentTime = 0.0f;
            Statistic = new Statistics();
            TimeEnd = 0;
            IsRunning = false;
        }

    }
}
