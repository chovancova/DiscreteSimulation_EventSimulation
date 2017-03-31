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
    /// Preparkovanie auta späť zákazníkovi
    /// Naplánujem: 
    ///  -	Odchod zákazníka v čase vygenerovanom Generátorom 6 – prevzatie auta.
    ///  -	Zadanie objednávky – ak front čakajúcich zákazníkov nie je prázdni, tak naplánujem zadanie objednávky vo vygenerovanom čase Generátorom 3 – Prevzatie objednávky. 
    ///  -	Uvoľnenie pracovníka  - ak front čakajúcich zákazníkov je prázdny, tak zvýšim počet voľných pracovníkov o jedna, a znížim počet obsluhujúcich pracovníkov skupiny1. 
    /// </summary>
    class PreparkovanieAutaSpatEvent : AutoserviceEvent
    {
        public PreparkovanieAutaSpatEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Preparkovanie auta späť zákazníkovi
        /// Naplánujem: 
        ///  -	Odchod zákazníka v čase vygenerovanom Generátorom 6 – prevzatie auta.
        ///  -	Zadanie objednávky – ak front čakajúcich zákazníkov nie je prázdni, tak naplánujem zadanie objednávky vo vygenerovanom čase Generátorom 3 – Prevzatie objednávky. 
        ///  -	Uvoľnenie pracovníka  - ak front čakajúcich zákazníkov je prázdny, tak zvýšim počet voľných pracovníkov o jedna, a znížim počet obsluhujúcich pracovníkov skupiny1. 
        /// </summary>
        public override void Execute()
        {
            //odchod zákazníka 
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator6_Prevzatie();
            var odchod = new OdchodZakaznikaEvent(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(odchod, time);

            //zadanie objednavky
            Zakaznik zakaznik = ((AppCore) ReferenceSimCore).DalsiZakaznik();
            if (zakaznik != null)
            {
                var time2 = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator3_PrevzatieObjednavky();
                var zadanie = new ZadanieObjednavky(time, ReferenceSimCore, zakaznik);
                ReferenceSimCore.ScheduleEvent(zadanie, time2);
            }
            else
            {
                ((AppCore) ReferenceSimCore).PocetObsluhujucichPracovnikov1--;
                ((AppCore) ReferenceSimCore).PocetVolnychPracovnikov1++;
            }
        }
    }
}
