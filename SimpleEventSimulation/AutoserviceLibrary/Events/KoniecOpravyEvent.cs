using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    /// Koniec opravy
    /// Naplánujem:
    /// -	Začiatok opravy – naplánujem okamžite.Opravené auto vložím do frontu opravených áut, kde to auto bude čakať na vyzdvihnutie pracovníkom skupiny 1.  
    /// </summary>
    class KoniecOpravyEvent : AutoserviceEvent

    {
        public KoniecOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Koniec opravy
        /// Naplánujem:
        /// -	Začiatok opravy – naplánujem okamžite.Opravené auto vložím do frontu opravených áut, kde to auto bude čakať na vyzdvihnutie pracovníkom skupiny 1.  
        /// </summary>
        public override void Execute()
        {
            ((AppCore)ReferenceSimCore).PridajAuto(AktualnyZakaznik);

            //naplanujem zaciatok opravy
            var time = EventTime + 0.0; 
            var zaciatok = new ZaciatokOpravyEvent(time, ReferenceSimCore, new Zakaznik());
        }
    }
}
