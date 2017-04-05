﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;
using AutoserviceLibrary.Events.Objednavka;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    /// U5 - Preparkovanie auta k dielni </summary>
    /// Vyberiem opravené auto z frontu opravených áut. 
    /// Naplánujem:
    ///       -	Začiatok opravy v okamžitom čase, ak je voľný pracovník skupiny 2. Obsadím pracovníka 2 skupiny. 
    ///       -	Front pokazených áut - Pridám auto (aktuálneho zákazníka) do frontu pokazených áut, ak existuje a zároveň nie je voľný pracovník skupiny 2.
    ///       -	Preparkovanie auta späť zákazníkovi – prioritne (pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). Vyberiem opravené auto z frontu opravených áut. Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom. 
    ///       -	Začiatok spracovania objednávky  - s druhou prioritou, ak je front opravených áut prázdny a zároveň nie je front čakajúcich zákazníkov prázdny. Vyberiem zákazníka z frontu čakajúcich zákazníkov. Naplánujem s vygenerovaným časom z Generátora 5 – preparkovanie. 
    ///       -	Uvoľnenie pracovníka – ak front opravených áut prázdny, a zároveň front čakajúcich zákazníkov, tak uvoľním pracovníka skupiny 1.  
    class PreparkovanieAutoEvent : AutoserviceEvent
    {
        public PreparkovanieAutoEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// U5 - Preparkovanie auta k dielni </summary>
        /// Vyberiem opravené auto z frontu opravených áut. 
        /// Naplánujem:
        ///       -	Začiatok opravy v okamžitom čase, ak je voľný pracovník skupiny 2. Obsadím pracovníka 2 skupiny. 
        ///       -	Front pokazených áut - Pridám auto (aktuálneho zákazníka) do frontu pokazených áut, ak existuje a zároveň nie je voľný pracovník skupiny 2.
        ///       -	Preparkovanie auta späť zákazníkovi – prioritne (pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). Vyberiem opravené auto z frontu opravených áut. Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom. 
        ///       -	Začiatok spracovania objednávky  - s druhou prioritou, ak je front opravených áut prázdny a zároveň nie je front čakajúcich zákazníkov prázdny. Vyberiem zákazníka z frontu čakajúcich zákazníkov. Naplánujem s vygenerovaným časom z Generátora 5 – preparkovanie. 
        ///       -	Uvoľnenie pracovníka – ak front opravených áut prázdny, a zároveň front čakajúcich zákazníkov, tak uvoľním pracovníka skupiny 1.  
        public override void Execute()
        {
            if(AktualnyZakaznik != null)
            {
                if (((AppCore)ReferenceSimCore).JeVolnyPracovnik2())
                {
                    ((AppCore)ReferenceSimCore).ObsadPracovnikaSkupiny2();

                   //naplanujem zaciatok opravy
                    var oprava = new ZaciatokOpravyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik);
                    ReferenceSimCore.ScheduleEvent(oprava);
                }
                else
                {
                    //vlozim do frontu auto pokazenych aut
                    ((AppCore)ReferenceSimCore).Front_PokazeneAuta_Pridaj(AktualnyZakaznik);
                }
            }
                                 
            //cas preparkovanie spat 
            var casPreparkovania = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator5_Preparkovanie();
          
            //PRIORITNE
            if (!((AppCore)ReferenceSimCore).JeFrontOpravenychAutPrazdny())
            {
                //vyberiem auto z frontu
                var opraveneAuto = ((AppCore)ReferenceSimCore).Front_OpraveneAuta_Vyber();
                //preparkovanie auta naspat
                var preparkovanieNaspat = new PreparkovanieAutaSpatEvent(casPreparkovania, ReferenceSimCore, opraveneAuto);
                ReferenceSimCore.ScheduleEvent(preparkovanieNaspat);
            }
            else
            {
                //NEPRIORITNE
                if (!((AppCore)ReferenceSimCore).JeFrontZakaznikovPrazdny())
                {
                    //vyberiem z frontu cakajucich zakaznikov
                    var cakajuciZakaznik = ((AppCore)ReferenceSimCore).Front_CakajuciZakaznici_VyberZakaznika();
                    //naplanujem zaciatok obsluhy
                    var zaciatokObsluhy = new ZaciatokSpracovaniaObjednavkyEvent(casPreparkovania, ReferenceSimCore, cakajuciZakaznik);
                    ReferenceSimCore.ScheduleEvent(zaciatokObsluhy);
                }
                else
                {
                    //uvolnim pracovnika 1
                    ((AppCore)ReferenceSimCore).UvolniPracovnikaSkupiny1();
                }
            }

         }
    }
}