using System;
using Priority_Queue;
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
        public double MaximumSimulationTime { get; set; }
        private int _numberOfStatistic = 4;
        public double[] Result { get; set; }

        public SimCore(IGenerators[] generators, double maxTime)
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            Generators = generators;
            MaximumSimulationTime = maxTime;
            IsRunning = true;
        }

        protected SimCore(double maxTime)
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            MaximumSimulationTime = maxTime;
            IsRunning = true;
        }

        public void ScheduleEvent(SimEvent eSimEvent, double time)
        {
            if (CurrentTime > time) throw new Exception("Scheadule Event is not possible. Current time > time.");
            _timeLine.Enqueue(eSimEvent, time);
        }

        public void Simulate()
        {
            SimEvent hlpEvt;

            while (_timeLine.Count > 0 &&  MaximumSimulationTime > CurrentTime && IsRunning)
            {
                //initialization of current event, time
                hlpEvt = _timeLine.Dequeue();
                CurrentTime = hlpEvt.EventTime;
                hlpEvt.Execute(); //virtual method. 

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
            MaximumSimulationTime = 0;
            IsRunning = false;
        }

        public bool StopReplications;
        public void DoReplications(int n)
        {
            long[] sumResults = new long[_numberOfStatistic];

            int i = 0;
            StopReplications = false;
            for (; i < n; i++)
            {
                if (StopReplications)
                {
                    break;
                }
                OnRefresh();
             }
           // Result = DoStatistics(0, i);
        }


        public double[] DoStatistics(long[] sum, int countExperiments)
        {
            double[] averages = new double[_numberOfStatistic];
            for (int i = 0; i < _numberOfStatistic; i++)
            {
                averages[i] = ((double)sum[i] / countExperiments);
            }
            return averages;
        }
    }
}