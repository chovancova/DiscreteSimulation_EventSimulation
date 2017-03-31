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
    /// Spracovanie objednávky
    /// Spracovanie objednávky znamená prevzatie objednávky, 
    /// zapíše do informačného systému údaje o aute, 
    /// popis porúch a ide so zákazníkom prevziať auto.
    ///    Vygenerujem Generátorom 2 počet opráv zákazníkovi.
    ///    Započítam štatistiku času potrebného na vybavenie objednávky.
    ///
    ///    Naplánujem: 
    ///	       Preparkovanie auto pred dielnou s vygenerovaným časom z Generátora 5 – preparkovanie.
    /// </summary>
    class SpracovanieObjednavky : AutoserviceEvent
    {
        public SpracovanieObjednavky(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Spracovanie objednávky
        /// Spracovanie objednávky znamená prevzatie objednávky, 
        /// zapíše do informačného systému údaje o aute, 
        /// popis porúch a ide so zákazníkom prevziať auto.
        ///    Vygenerujem Generátorom 2 počet opráv zákazníkovi.
        ///    Započítam štatistiku času potrebného na vybavenie objednávky.
        ///
        ///    Naplánujem: 
        ///	       Preparkovanie auto pred dielnou s vygenerovaným časom z Generátora 5 – preparkovanie.
        /// </summary>
        public override void Execute()
        {
            var pocetOprav = ((AppCore) ReferenceSimCore).Gen.Generator2_PocetOprav();
            AktualnyZakaznik.PocetOprav = pocetOprav; 

            //todo Statistika na zapocitanie casu potrebneho na vybavenie objednavky
            
            //naplanujem preparkovanie auta 
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator5_Preparkovanie();
            var preparkovanie = new PreparkovanieAutoEvent(time,ReferenceSimCore,AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(preparkovanie, time);
       }
    }
}
