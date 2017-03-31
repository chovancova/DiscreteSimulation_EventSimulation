using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    public class PrazdnyPrevozEvent : AutoserviceEvent
    {
        public PrazdnyPrevozEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
