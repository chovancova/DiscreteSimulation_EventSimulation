using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationLibrary
{
    class RefreshEvent : SimEvent
    {
        public RefreshEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }
        /***
         * Refresh event is always carried out. 
         **/
        public override void Execute()
        {
            if (ReferenceSimCore.Refresh)
            {
                double refreshTime = EventTime + ReferenceSimCore.RefreshRate;
                RefreshEvent refreshEvent = new RefreshEvent(refreshTime, ReferenceSimCore);
                ReferenceSimCore.ScheduleEvent(refreshEvent, refreshTime);
                try
                {
                    Thread.Sleep((int) Math.Round(ReferenceSimCore.SleepingTime));
                    ReferenceSimCore.Gui?.RefreshGui();

                }
                catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine(ex.Message);
                    Thread.CurrentThread.Interrupt();
                }        
            }
          
        }
    }
}
