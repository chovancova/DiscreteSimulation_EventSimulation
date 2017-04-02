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
    ///U3 - Koniec spracovania objednávky
    ///Vyberiem zákazníka z frontu čakajúcich zákazníkov.
    ///Naplánujem: 
    ///-	Prevzatie auta od zákazníka s vygenerovaným časom Generátorom 4 – prevzatie auta. 
    ///Štatistiky: 
    ///-	S1b – Skončím meranie času čakania zákazníka v rade na zadanie objednávky. 
    ///-	S2b – Skončím počítanie doby zákazníkov v rade čakajúcich zákazníkov. 
    /// </summary>
    class KoniecSpracovaniaObjednavky : AutoserviceEvent
    {
        public KoniecSpracovaniaObjednavky(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U3 - Koniec spracovania objednávky
        ///Vyberiem zákazníka z frontu čakajúcich zákazníkov.
        ///Naplánujem: 
        ///-	Prevzatie auta od zákazníka s vygenerovaným časom Generátorom 4 – prevzatie auta. 
        ///Štatistiky: 
        ///-	S1b – Skončím meranie času čakania zákazníka v rade na zadanie objednávky. 
        ///-	S2b – Skončím počítanie doby zákazníkov v rade čakajúcich zákazníkov. 
        /// </summary>
        public override void Execute()
        {
            if (!((AppCore) ReferenceSimCore).JeFrontZakaznikovPrazdny())
            {
                var zakaznik = ((AppCore)ReferenceSimCore).DalsiZakaznik();

                var cakanie = zakaznik.SkonciCakanieVRade(EventTime);
                ((AppCore)ReferenceSimCore).PridajStatistikuCakaniaFrontZakaznikov(cakanie);
                
                //naplanujem prevzatie auta 
                var timePrevzatia = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator4_PrevzatieAuta();
                    var prevzatie = new PrevzatieAutaOdZakaznikaEvent(timePrevzatia, ReferenceSimCore, zakaznik);
                    ReferenceSimCore.ScheduleEvent(prevzatie, timePrevzatia);
            }
            else
            {
              //  ReferenceSimCore.ScheduleEvent(new KoniecSpracovaniaObjednavky(EventTime, ReferenceSimCore, AktualnyZakaznik), EventTime);
            }
        }
    }
}
