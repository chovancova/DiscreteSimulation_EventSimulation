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
    ///U6 - Začiatok opravy
    ///Vyberiem auto z frontu pokazených áut.Ak je front pokazených áut prázdny, tak čakám. 
    ///Naplánujem: 
    ///-	Koniec opravy –  Ak je voľný pracovník skupiny 1, tak vygenerujem Generátorom 2 počet opráv, 
    ///                      ktoré má auto, a pre každú opravu vygenerujem Generátorom 7 – dobu opravy auta.
    ///                      Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.
    ///                      Znížim počet voľných pracovníkov o jedna. 
    ///Štatistiky:
    ///-	S5b, S6b – Skončím počítanie času, počtu pre dané auto v rade pokazených áut. 
    ///-	S10a – Započítam do štatistiky počet voľných pracovníkov skupiny 2. 
    ///-	Skončím počítanie času čakania pokazeného auta v rade pokazených áut. 
    /// </summary>
    class ZaciatokOpravyEvent : AutoserviceEvent
    {
        public ZaciatokOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U6 - Začiatok opravy
        ///Vyberiem auto z frontu pokazených áut.Ak je front pokazených áut prázdny, tak čakám. 
        ///Naplánujem: 
        ///-	Koniec opravy –  Ak je voľný pracovník skupiny 1, tak vygenerujem Generátorom 2 počet opráv, 
        ///                      ktoré má auto, a pre každú opravu vygenerujem Generátorom 7 – dobu opravy auta.
        ///                      Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.
        ///                      Znížim počet voľných pracovníkov o jedna. 
        ///Štatistiky:
        ///-	S5b, S6b – Skončím počítanie času, počtu pre dané auto v rade pokazených áut. 
        ///-	S10a – Započítam do štatistiky počet voľných pracovníkov skupiny 2. 
        ///-	Skončím počítanie času čakania pokazeného auta v rade pokazených áut. 
        /// </summary>
        public override void Execute()
        {
            if (!((AppCore) ReferenceSimCore).JeFrontPokazenychAutPrazdny() )
            {
                if (((AppCore) ReferenceSimCore).JeVolnyPracovnik2())
                {
                    ((AppCore) ReferenceSimCore).ObsadPracovnikaSkupiny2();

                    var auto = ((AppCore) ReferenceSimCore).DalsiePokazeneAuto();

                    //koniec opravy
                    int sucet = 0;
                    int pocetOprav = ((AppCore) ReferenceSimCore).Gen.Generator2_PocetOprav();
                    for (int i = 0; i < pocetOprav; i++)
                    {
                        sucet += ((AppCore) ReferenceSimCore).Gen.Generator7_DobaOpravy()*60;
                    }

                    var time = EventTime + sucet;
                    var koniecOpravy = new KoniecOpravyEvent(time, ReferenceSimCore, auto);
                    ReferenceSimCore.ScheduleEvent(koniecOpravy, time);
                }
                else
                {
                    //cakaj

                }
            }
        }
    }
}
