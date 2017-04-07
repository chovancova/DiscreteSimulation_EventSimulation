using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Priority_Queue;
using RandomGenerators.Generators;

namespace SimulationLibrary
{
    public abstract class SimCore
    {
        public double CurrentTime { get; private set; }
        private FastPriorityQueue<SimEvent> _timeLine;
        public double RefreshRate { get; set; }
        public double SleepingTime { get; set; }
        public bool IsRunning { get; set; }
        public bool Paused { get; set; }
        public bool Stopped { get; set; }
        //ak je true - refreshujem po kazdom evente, ak false refreshujem po kazdej replikacii
        public bool Refresh { get; set; }
        public int ActualReplication { get; set; }
        public int NumberOfReplication { get; private set; }
      public bool Done { get; private set; }
        public bool SuperExtraUltraMode { get; set; }
        public SimCore()
        {
            _timeLine = new FastPriorityQueue<SimEvent>(200);
            CurrentTime = 0.0f;
            Stopped = false;
            ActualReplication = 0;
            NumberOfReplication = 0; 
            Refresh = false;
            SleepingTime = 20;
            RefreshRate = 20;
            SuperExtraUltraMode = false;
        }
        public void ScheduleEvent(SimEvent eSimEvent, double time)
        {
            //if (CurrentTime > time)
            //    throw new Exception("Scheadule Event is not possible. Current time > time.");
            //  _timeLine.Enqueue(eSimEvent, time);
            _timeLine.Enqueue(eSimEvent, (float) time);
        }

        public void ScheduleEvent(SimEvent eSimEvent)
        {
            //if (CurrentTime > eSimEvent.EventTime)
            //    throw new Exception("Scheadule Event is not possible. Current time > time.");
            _timeLine.Enqueue(eSimEvent, (float)eSimEvent.EventTime);
        }
        protected abstract void BeforeSimulation();
        public  abstract void BeforeReplication();
        public abstract void AfterReplication();
        public abstract void SimulationEnd();
        public abstract void ScheduleFirstEvent();
        public abstract void DoGraphics();
        

        public void Simulate(int numberOfReplication, double lenghtReplication)
        {
            NumberOfReplication = numberOfReplication;
            Done = false;
            BeforeSimulation();
            for (ActualReplication = 0; ActualReplication < NumberOfReplication; ActualReplication++)
            {
                ResetCore(); 
                BeforeReplication();
                DoSimulationReplication(lenghtReplication);
                AfterReplication();
                if (!SuperExtraUltraMode) DoGraphics();

                if (Stopped)
                {
                    break;
                }
            }
            SimulationEnd();
            Done = true;
             if(!SuperExtraUltraMode) DoGraphics();
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
            if (!SuperExtraUltraMode) if (Refresh) ScheduleRefreshEvent();
            while (_timeLine.Count > 0 && CurrentTime < lenghtReplication)
            {
                temp = _timeLine.Dequeue();
                CurrentTime = temp.EventTime;
                temp.Execute();
                
                if (!SuperExtraUltraMode)
                {
                    if (Refresh)
                    DoGraphics();
                    if (Paused)
                    {
                        while (Paused)
                        {
                            Thread.Sleep(100);
                            if (!SuperExtraUltraMode)
                                DoGraphics();
                        }
                    }
                    if (Stopped)
                    {
                        break;
                    }
                }
            }
        }



        public void ScheduleRefreshEvent()
        {
           if(Refresh) ScheduleEvent(new RefreshEvent(CurrentTime, this), CurrentTime );
        }
   }
}