﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    ///U8 - Preparkovanie auta späť zákazníkovi
    ///Zvýšim počet voľných pracovníkov o jedna. 
    ///Naplánujem: 
    ///-	Odchod zákazníka v čase vygenerovanom Generátorom 6 – prevzatie auta. 
    ///-	Začiatok spracovania objednávky – okamžite.
    ///Štatistiky: 
    ///-	S9b - Započítam do štatistiky počet voľných pracovníkov skupiny 1. 
    /// </summary>
    class PreparkovanieAutaSpatEvent : AutoserviceEvent
    {
        public PreparkovanieAutaSpatEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U8 - Preparkovanie auta späť zákazníkovi
        ///Zvýšim počet voľných pracovníkov o jedna. 
        ///Naplánujem: 
        ///-	Odchod zákazníka v čase vygenerovanom Generátorom 6 – prevzatie auta. 
        ///-	Začiatok spracovania objednávky – okamžite.
        ///Štatistiky: 
        ///-	S9b - Započítam do štatistiky počet voľných pracovníkov skupiny 1. 
        /// </summary>
        public override void Execute()
        {
            //odchod zakaznika
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator6_Prevzatie();
            var odchod = new OdchodZakaznikaEvent(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(odchod, time);

            ((AppCore)ReferenceSimCore).UvolniPracovnikaSkupiny1();

            //zaciatok spracovania objednavky 
            ReferenceSimCore.ScheduleEvent(new ZaciatokSpracovaniaObjednavkyEvent(time, ReferenceSimCore, null), time);


        }
    }
}
