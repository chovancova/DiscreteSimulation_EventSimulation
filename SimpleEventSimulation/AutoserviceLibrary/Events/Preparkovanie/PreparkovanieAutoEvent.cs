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
    ///U5 - Preparkovanie auta k dielni
    /// Pridám auto (zákazníka) do frontu pokazených áut. 
    ///Naplánujem:
    ///-	Začiatok opravy v okamžitom čase.
    ///-	Preparkovanie auta späť zákazníkovi – prioritne(pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom.Front opravených áut sa zníži o jedna.  
    ///-	Začiatok spracovania objednávky  - s druhou prioritou, ak je front opravených áut prázdny. Naplánujem s vygenerovaným časom z Generátora 5 – preparkovanie späť. Zvýšim počet voľných pracovníkov o jedna viac.
    ///Štatistiky:
    ///-	S4a – Začnem merať čas strávený čakaním na opravu. (od ukončenia prevzatia auta do servisu)
    ///-	S5a, S6a – Čas a počet áut čakajúcich v rade pokazených áut.  (Začiatok opravy) 
    ///-	S7b, S8b – Čas a počet áut čakajúcich v rade opravených áut.  (Preparkovanie auta späť zákazníkovi)  
    ///-	S9a – v prípade udalosti Začiatok spracovania objednávky, pridám počet voľných pracovníkov skupiny 1. 
    /// </summary>
    class PreparkovanieAutoEvent : AutoserviceEvent
    {
        public PreparkovanieAutoEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// U5 - Preparkovanie auta k dielni
        /// Pridám auto (zákazníka) do frontu pokazených áut. 
        /// Vyberiem opravene auto z frontu pokazenych aut. 
        /// Naplánujem:
        ///-	Začiatok opravy v okamžitom čase.
        ///-	Preparkovanie auta späť zákazníkovi 
        ///              – prioritne(pracovníci skupiny 1 uprednostňujú vrátenie opraveného auta zákazníkovi pred prijatím novej objednávky). 
        ///                Ak nie je front opravených prázdny, tak naplánujem udalosť s vygenerovaným časom Generátora 5 – preparkovanie s opraveným autom.
        ///                Front opravených áut sa zníži o jedna.  
        ///-	Začiatok spracovania objednávky  
        ///              - s druhou prioritou, ak je front opravených áut prázdny. 
        ///                Naplánujem s vygenerovaným časom z Generátora 5 – preparkovanie späť. 
        ///                Zvýšim počet voľných pracovníkov o jedna viac.
        /// Štatistiky:
        ///-	S4a – Začnem merať čas strávený čakaním na opravu. (od ukončenia prevzatia auta do servisu)
        ///-	S5a, S6a – Čas a počet áut čakajúcich v rade pokazených áut.  (Začiatok opravy) 
        ///-	S7b, S8b – Čas a počet áut čakajúcich v rade opravených áut.  (Preparkovanie auta späť zákazníkovi)  
        ///-	S9a – v prípade udalosti Začiatok spracovania objednávky, pridám počet voľných pracovníkov skupiny 1. 
        /// </summary>
        public override void Execute()
        {
            //vlozim auto do frontu

            ((AppCore)ReferenceSimCore).Front_PokazeneAuta_Pridaj(AktualnyZakaznik);
            //zaciatok opravy 
            var oprava = new ZaciatokOpravyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(oprava, EventTime);




            var time = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator5_Preparkovanie();

            //prioritne
            if (!((AppCore) ReferenceSimCore).JeFrontOpravenychAutPrazdny())
            {
                 //vyberiem auto z frontu opravenych aut
                var opraveneAuto = ((AppCore) ReferenceSimCore).Front_OpraveneAuta_Vyber();
                //preparkovanie spat
                var preparkujSpat = new PreparkovanieAutaSpatEvent(time, ReferenceSimCore, opraveneAuto);
                ReferenceSimCore.ScheduleEvent(preparkujSpat, time);
            }
            else
            {
                ReferenceSimCore.ScheduleEvent(new UvolniPracovnikaEvent(time, ReferenceSimCore, null), time);

            }
        }
    }
}