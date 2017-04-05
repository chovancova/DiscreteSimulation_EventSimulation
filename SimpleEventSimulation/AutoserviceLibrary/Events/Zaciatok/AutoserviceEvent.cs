using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    public abstract class AutoserviceEvent : SimEvent
    {
        //customer1, druhy, zakaznik, auto a pdodobne
        protected AutoserviceEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation)
        {
            AktualnyZakaznik = aktualnyZakaznik;
        }

        public Zakaznik AktualnyZakaznik { get; set; }
    }
}