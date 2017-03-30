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
            double refresh = EventTime + ReferenceSimCore.RefreshRate;
            RefreshEvent refreshEvent = new RefreshEvent(refresh, ReferenceSimCore);
            ReferenceSimCore.ScheduleEvent(refreshEvent, refresh);

            Thread.Sleep((int) Math.Round(ReferenceSimCore.SleepingTime));
        }
    }
}
