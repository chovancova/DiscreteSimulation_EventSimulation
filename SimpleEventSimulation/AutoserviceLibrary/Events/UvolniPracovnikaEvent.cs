using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class UvolniPracovnikaEvent : AutoserviceEvent
    {
        public UvolniPracovnikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        public override void Execute()
        {
            ((AppCore)ReferenceSimCore).UvolniPracovnikaSkupiny1();
        }
    }
}
