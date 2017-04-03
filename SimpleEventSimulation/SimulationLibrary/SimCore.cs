using System;
using System.Threading;
using Priority_Queue;
using RandomGenerators.Generators;

namespace SimulationLibrary
{
    public abstract class SimCore
    {
        public double CurrentTime { get; private set; }
        private SimplePriorityQueue<SimEvent, double> _timeLine;

        public double RefreshRate { get; set; }
        public double SleepingTime { get; set; }
        public bool IsRunning { get; set; }
        public bool Paused { get; set; }
        public bool Stopped { get; set; }
        //ak je true - refreshujem po kazdom evente, ak false refreshujem po kazdej replikacii
        public bool Refresh { get; set; }

        public ISimulationGui Gui { get; set; }

        public int ActualReplication { get; set; }
      
        public SimCore()
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            Stopped = false;
            ActualReplication = 0;
            Refresh = false;
        }
        public void ScheduleEvent(SimEvent eSimEvent, double time)
        {
            if (CurrentTime > time)
                throw new Exception("Scheadule Event is not possible. Current time > time.");
            _timeLine.Enqueue(eSimEvent, time);
        }
        
        public void Simulate(int numberOfReplication, double lenghtReplication)
        {

            for (int replication = 0; replication < numberOfReplication; replication++)
            {
                if (Stopped)
                {
                    break;
                }
                ResetCore(); 

                BeforeReplication();

                DoSimulationReplication(lenghtReplication, replication);

                AfterReplication();
           }
            SimulationEnd();
        }

        public void ResetCore()
        {
            _timeLine.Clear();
            CurrentTime = 0.0;
        }

        public  abstract void BeforeReplication();
        public abstract void AfterReplication();
        public abstract void SimulationEnd();

        public void DoSimulationReplication(double lenghtReplication, int replication)
        {
            ActualReplication = replication + 1;
            SimEvent temp;
            ScheduleRefreshEvent();
            while (_timeLine.Count > 0 && CurrentTime <= lenghtReplication)
            {
                    temp = _timeLine.Dequeue();
                    CurrentTime = temp.EventTime;
              
                //acceleration, deceleration, suspension, animation
               // RefreshEvent
            }
        }

        public void ScheduleRefreshEvent()
        {
           if(Refresh) ScheduleEvent(new RefreshEvent(CurrentTime, this), CurrentTime );
        }


        public double _maximumSimTime;


        public void Simulate()
        {
            SimEvent hlpEvt;

            while (_timeLine.Count > 0 &&  _maximumSimTime > CurrentTime)
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

     
        //public virtual void OnRefresh()
        //{
        //    var handler = Refresh;
        //    if (handler != null) handler(this, EventArgs.Empty);
        //}

        public void Stop()
        {
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            _maximumSimTime = 0;
            IsRunning = false;
        }

        public bool StopReplications;
        public void DoReplications(int n)
        {

            int i = 0;
            StopReplications = false;
            for (; i < n; i++)
            {
                if (StopReplications)
                {
                    break;
                }
           //     OnRefresh();
             }
        }








        public void DoEventSimulationExperiments(int n)
        {
            
        }

        public void DoExperiment()
        {
            
        }










































    }
}