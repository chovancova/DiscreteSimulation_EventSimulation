using System;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events.Objednavka
{
    /// <summary>
    ///     U2 - Začiatok spracovania objednávky
    ///     Naplánujem:
    ///     -	Koniec spracovania objednávky,
    ///     aplánujem udalosť v čase vygenerovaným Generátorom 3 – prevzatie objednávky.
    /// </summary>
    public class ZaciatokSpracovaniaObjednavkyEvent : AutoserviceEvent
    {
        public ZaciatokSpracovaniaObjednavkyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U2 - Začiatok spracovania objednávky
        ///     Naplánujem:
        ///     -	Koniec spracovania objednávky,
        ///     tak naplánujem udalosť v čase vygenerovaným Generátorom 3 – prevzatie objednávky.
        ///     Znížim počet voľných pracovníkov o jedna.
        /// </summary>
        public override void Execute()
        {
            if (AktualnyZakaznik == null) throw new Exception("NULL zakaznik. ");

            //naplanujem spracovanie objednavky 
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator3_PrevzatieObjednavky();
            var spracovanie = new KoniecSpracovaniaObjednavky(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(spracovanie);
        }
    }
}