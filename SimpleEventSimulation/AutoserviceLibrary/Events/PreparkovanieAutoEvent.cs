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
    /// Preparkovanie auta k dielni
    ///Naplánujem:
    /// -	Začiatok opravy v okamžitom čase - Ak je pracovník skupiny 2 voľný.Znížim počet voľných pracovníkov skupiny 2, a zvýšim počet obsluhujúcich pracovníkov skupiny 2 o jedna.
    /// -	Front pokazených áut – ak nie je voľný žiaden pracovník skupiny 2, tak pridám auto (zákazníka) do frontu pokazených áut.Započítam do štatistiky začiatok čakania v rade na opravu auta.
    /// -	Uvoľnenie pracovníka – ak je front opravených áut prázdny, a zároveň front čakajúcich zákazníkov, tak sa zvýši počet voľných pracovníkov 1 o jedna, a zníži sa počet obsluhujúcich pracovníkov skupiny 1. Táto udalosť sa vykoná okamžite. 
    /// -	Preparkovanie auta späť zákazníkovi – prioritne (pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom.Front opravených áut sa zníži o jedna.  
    /// -	Zadanie objednávky – ak nie je žiadne opravené auto v dielni, a zároveň je vo fronte čakajúci zákazník, tak sa udalosť naplánuje s časom vygenerovaným Generátorom 5 – preparkovanie späť, spočítaním spolu s časom vygenerovaním Generátorom 3 – prevzatie objednávky. Zákazník je vybraný z frontu čakajúcich zákazníkov, jeho štatistika ukončenia čakania v rade je nastavená na čas vygenerovaným Generátorom 5.  
    /// </summary>
    class PreparkovanieAutoEvent : AutoserviceEvent
    {
        public PreparkovanieAutoEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Preparkovanie auta k dielni
        ///Naplánujem:
        /// -	Začiatok opravy v okamžitom čase - Ak je pracovník skupiny 2 voľný.Znížim počet voľných pracovníkov skupiny 2, a zvýšim počet obsluhujúcich pracovníkov skupiny 2 o jedna.
        /// -	Front pokazených áut – ak nie je voľný žiaden pracovník skupiny 2, tak pridám auto (zákazníka) do frontu pokazených áut.Započítam do štatistiky začiatok čakania v rade na opravu auta.
        /// -	Uvoľnenie pracovníka – ak je front opravených áut prázdny, a zároveň front čakajúcich zákazníkov, tak sa zvýši počet voľných pracovníkov 1 o jedna, a zníži sa počet obsluhujúcich pracovníkov skupiny 1. Táto udalosť sa vykoná okamžite. 
        /// -	Preparkovanie auta späť zákazníkovi – prioritne (pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom.Front opravených áut sa zníži o jedna.  
        /// -	Zadanie objednávky – ak nie je žiadne opravené auto v dielni, a zároveň je vo fronte čakajúci zákazník, tak sa udalosť naplánuje s časom vygenerovaným Generátorom 5 – preparkovanie späť, spočítaním spolu s časom vygenerovaním Generátorom 3 – prevzatie objednávky. Zákazník je vybraný z frontu čakajúcich zákazníkov, jeho štatistika ukončenia čakania v rade je nastavená na čas vygenerovaným Generátorom 5.  
        /// </summary>
        public override void Execute()
        {
            //zaciatok opravy 
            if (((AppCore) ReferenceSimCore).PocetVolnychPracovnikov2 > 0)
            {
                ((AppCore) ReferenceSimCore).PocetVolnychPracovnikov2--;
                ((AppCore) ReferenceSimCore).PocetObsluhujucichPracovnikov2++;
                var time = EventTime;
                var oprava = new ZaciatokOpravyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik);
                ReferenceSimCore.ScheduleEvent(oprava, time);

            }
            else 
            //front pokazenych aut
            {
                AktualnyZakaznik.ZacniCakatNaOpravu(EventTime);
                ((AppCore)ReferenceSimCore).PridajPokazeneAuto(AktualnyZakaznik);
            }

            var auto = ((AppCore)ReferenceSimCore).DalsieOpraveneAuto();
            var time1 = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator5_Preparkovanie();

            if (auto != null)
            {
                //preparkovanie auta spat zakaznikovi
                var preparkovanie = new PreparkovanieAutaSpatEvent(time1, ReferenceSimCore, auto);
                ReferenceSimCore.ScheduleEvent(preparkovanie, time1);
            }
            else
            {
                var zakaznik = ((AppCore) ReferenceSimCore).DalsiZakaznik();
                ((AppCore)ReferenceSimCore).PridajStatistikuCakaniaFrontZakaznikov(time1);

                if (zakaznik == null)
                {
                    ((AppCore) ReferenceSimCore).PocetObsluhujucichPracovnikov1--;
                    ((AppCore) ReferenceSimCore).PocetVolnychPracovnikov1++;
                }
                else
                {
                    //naplanovanie zadanie objednavky
                    var time2 = time1 + ((AppCore)ReferenceSimCore).Gen.Generator3_PrevzatieObjednavky();
                    var zadanie = new ZadanieObjednavky(time2, ReferenceSimCore, zakaznik);
                    ReferenceSimCore.ScheduleEvent(zadanie, time2);
                }
            }
        }
    }
}
