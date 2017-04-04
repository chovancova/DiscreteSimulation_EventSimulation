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
      
        public SimCore(ISimulationGui gui =  null)
        {
            Gui = gui;
            _timeLine = new SimplePriorityQueue<SimEvent, double>();
            CurrentTime = 0.0f;
            Stopped = false;
            ActualReplication = 0;
            Refresh = false;
            SleepingTime = 20;
            RefreshRate = 20;
        }
        public void ScheduleEvent(SimEvent eSimEvent, double time)
        {
            if (CurrentTime > time)
                throw new Exception("Scheadule Event is not possible. Current time > time.");
            _timeLine.Enqueue(eSimEvent, time);
        }
        public  abstract void BeforeReplication();
        public abstract void AfterReplication();
        public abstract void SimulationEnd();
        public abstract void ScheduleFirstEvent();
        public abstract void AfterStopReplications();

        public void Simulate(int numberOfReplication, double lenghtReplication)
        {
            for (ActualReplication = 0; ActualReplication < numberOfReplication; ActualReplication++)
            {
                ResetCore(); 
                BeforeReplication();
                DoSimulationReplication(lenghtReplication);
                AfterReplication();
                if (Stopped)
                {
                    AfterStopReplications();
                    break;
                }
            }
            SimulationEnd();
        }


        public void ResetCore()
        {
            _timeLine.Clear();
            CurrentTime = 0.0;
        }
        
        public void DoSimulationReplication(double lenghtReplication)
        {
            SimEvent temp;
            ScheduleFirstEvent();
            ScheduleRefreshEvent();
            while (_timeLine.Count > 0 && CurrentTime < lenghtReplication)
            {
                temp = _timeLine.Dequeue();
                CurrentTime = temp.EventTime;
                temp.Execute();
                Gui.RefreshGui();
                if (Paused)
                {
                    while (Paused)
                    {
                        Thread.Sleep(200);
                        Gui.RefreshGui();
                    }   
                }
                if (Stopped)
                {
                    AfterStopReplications();
                    break;
                }

            }
        }



        public void ScheduleRefreshEvent()
        {
           if(Refresh) ScheduleEvent(new RefreshEvent(CurrentTime, this), CurrentTime );
        }
   }
}