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
    ///U9 - Odchod zákazníka
    ///Uvoľním zákazníka zo systému. 
    ///Štatistiky: 
    ///-	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu.    
    /// </summary>
    class KoniecOpravyEvent : AutoserviceEvent

    {
        public KoniecOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///U7 - Koniec opravy
        ///Opravené auto vložím do frontu opravených áut, kde to auto bude čakať na vyzdvihnutie pracovníkom skupiny 1.   
        ///Naplánujem:
        ///-	Začiatok opravy – naplánujem okamžite. Zvýšim počet voľných pracovníkov o jedna. 
        ///Štatistiky: 
        ///-	S10b - Započítam do štatistiky počet voľných pracovníkov skupiny 2. 
        ///-	S7a, S8a - Začnem počítať čas čakania a počet opravených áut v rade opravených áut. 
        /// </summary>
        public override void Execute()
        {
            ((AppCore)ReferenceSimCore).Front_OpraveneAuta_Pridaj(AktualnyZakaznik);
            
            //naplanujem zaciatok opravy
            var zaciatok = new ZaciatokOpravyEvent(EventTime, ReferenceSimCore, null);
            ReferenceSimCore.ScheduleEvent(zaciatok,EventTime);
            ((AppCore)ReferenceSimCore).UvolniPracovnikaSkupiny2();

        }
    }
}
