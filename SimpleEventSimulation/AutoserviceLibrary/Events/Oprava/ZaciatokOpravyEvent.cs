using System;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    /// U6 - Začiatok opravy </summary>
    /// Naplánujem: 
    ///    - Koniec opravy –  Vygenerujem Generátorom 2 počet opráv, ktoré má auto, a pre každú opravu vygenerujem Generátorom 7 – dobu opravy auta v sekundách.Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.Znížim počet voľných pracovníkov o jedna. 
   internal class ZaciatokOpravyEvent : AutoserviceEvent
    {
        public ZaciatokOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        /// U6 - Začiatok opravy </summary>
        /// Naplánujem: 
        ///    - Koniec opravy –  Vygenerujem Generátorom 2 počet opráv, ktoré má auto, a pre každú opravu vygenerujem Generátorom 7 – dobu opravy auta v sekundách.Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.Znížim počet voľných pracovníkov o jedna. 
        public override void Execute()
        {
            if (AktualnyZakaznik.Typ != TypZakaznika.PokazeneAuto)
                throw new Exception("Aktualny zakaznik musi byt typu pokazene auto. ");

            //koniec opravy
            var dobaOpravySpolu = 0;
            var pocetOprav = ((AppCore) ReferenceSimCore).Gen.Generator2_PocetOprav();
            for (var i = 0; i < pocetOprav; i++)
                dobaOpravySpolu += ((AppCore) ReferenceSimCore).Gen.Generator7_DobaOpravy()*60;

            var time = EventTime + dobaOpravySpolu;
            var koniecOpravy = new KoniecOpravyEvent(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(koniecOpravy, time);
        }
    }
}
