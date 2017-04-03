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
    ///U2 - Začiatok spracovania objednávky
    ///Naplánujem: 
    ///-	Koniec spracovania objednávky, ak je pracovník skupiny 1 voľný a zároveň front čakajúcich zákazníkov nie je prázdny, tak naplánujem udalosť v čase vygenerovaným Generátorom 3 – prevzatie objednávky. Znížim počet voľných pracovníkov o jedna. 
    ///-	Ak je front čakajúcich prázdny, tak čakám. 
    ///Štatistiky: 
    ///-	S9b - Započítam do štatistiky počet voľných pracovníkov skupiny 1. 
    /// </summary>
    class ZaciatokSpracovaniaObjednavkyEvent : AutoserviceEvent
    {
        public ZaciatokSpracovaniaObjednavkyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U2 - Začiatok spracovania objednávky
        ///Naplánujem: 
        ///-	Koniec spracovania objednávky, ak je pracovník skupiny 1 voľný a zároveň front čakajúcich zákazníkov nie je prázdny, tak naplánujem udalosť v čase vygenerovaným Generátorom 3 – prevzatie objednávky. Znížim počet voľných pracovníkov o jedna. 
        ///-	Ak je front čakajúcich prázdny, tak čakám. 
        ///Štatistiky: 
        ///-	S9b - Započítam do štatistiky počet voľných pracovníkov skupiny 1. 
        ///</summary>
        public override void Execute()
        {
            if (((AppCore) ReferenceSimCore).JeVolnyPracovnik1())
            {
                ((AppCore)ReferenceSimCore).ObsadPracovnikaSkupiny1();

                //naplanujem spracovanie objednavky 
                var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator4_PrevzatieAuta();
                var spracovanie = new KoniecSpracovaniaObjednavky(time, ReferenceSimCore, null);
                ReferenceSimCore.ScheduleEvent(spracovanie, time);
            }
            else
            {
               // ReferenceSimCore.ScheduleEvent(new ZaciatokSpracovaniaObjednavkyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik), EventTime );
            }
        }
    }
}
