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
        public double CurrentTime { get; set; }
        private SimplePriorityQueue<SimEvent, double> _timeLine;

        public IGenerators[] Generators { get; set; }
        public virtual event EventHandler Refresh;
        public bool IsRunning { get; set; }
        public Double TimeEnd { get; set; }

        public SimCore(IGenerators[] generators)
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            Generators = generators;
            TimeEnd = 0;
            IsRunning = true;
        }

        protected SimCore()
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            TimeEnd = 0;
            IsRunning = true;
        }

        public void ScheduleEvent(SimEvent eSimEvent, double time)
        {
            if (CurrentTime > time) throw new Exception("Scheadule Event is not possible. Current time > time.");
            _timeLine.Enqueue(eSimEvent, time);
        }

        public void Simulate()
        {
            SimEvent currentEvent;

            while (_timeLine.Count > 0 && CurrentTime < TimeEnd && IsRunning)
            {
                //initialization of current event, time
                currentEvent = _timeLine.First();
                CurrentTime = currentEvent.EventTime;
                currentEvent.Execute(); //virtual method. 

                //here add - acceleration, deceleration, suspension, animation
                // OnRefresh();
                // Stop();
            }
        }

        public void Simulate(double simulationTime, SimEvent helpEvent)
        {
            while (true)
            {
                if (_timeLine.Count == 0) break;
                if (simulationTime <= CurrentTime) break;

                helpEvent = _timeLine.First;
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
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            TimeEnd = 0;
            IsRunning = false;
        }
    }
}