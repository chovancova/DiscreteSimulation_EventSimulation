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
    /// Zadanie objednávky
    /// Nastavím koniec čakania zákazníkovi a započítam čas čakania v rade do štatistiky.
    /// Naplánujem: 
    ///    - Spracovanie objednávky(vytvorenie objednávky a prevzatie auta od zákazníka) s vygenerovaním časom z Generátora 4 – prevzatie auta.
    /// </summary>
    class ZadanieObjednavky : AutoserviceEvent
    {
        public ZadanieObjednavky(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Zadanie objednávky
        /// Nastavím koniec čakania zákazníkovi a započítam čas čakania v rade do štatistiky.
        /// Naplánujem: 
        ///    - Spracovanie objednávky(vytvorenie objednávky a prevzatie auta od zákazníka) s vygenerovaním časom z Generátora 4 – prevzatie auta.
        /// </summary>
        public override void Execute()
        {
            var cakanie = AktualnyZakaznik.SkonciCakanieVRade(EventTime);
            ((AppCore)ReferenceSimCore).PridajStatistikuCakaniaFrontZakaznikov(cakanie);

            //naplanujem spracovanie objednavky 
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator4_PrevzatieAuta();
            var spracovanie = new SpracovanieObjednavky(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(spracovanie, time);

            //todo Statistika - zakaznikovi zapisem tento cas, ze je v systeme.

        }
    }
}
