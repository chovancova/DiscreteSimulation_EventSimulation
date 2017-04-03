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
            Refresh = true;
            SleepingTime = 200;
            RefreshRate = 200;
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
                //ResetCore(); 

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
                    temp.Execute();
            }
        }

        public void ScheduleRefreshEvent()
        {
           if(Refresh) ScheduleEvent(new RefreshEvent(CurrentTime, this), CurrentTime );
        }
   }
}