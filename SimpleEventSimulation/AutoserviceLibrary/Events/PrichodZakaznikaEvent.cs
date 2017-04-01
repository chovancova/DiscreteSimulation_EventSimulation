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
    /// Príchod zákazníka
    ///  Naplánujem: 
    ///   -	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.  
    ///   -	Ak je voľný pracovník 1, tak naplánujem udalosť Zadanie objednávky s pripočítaním časom z Generátora 3 – prevzatie objednávky, a znížim počet voľných pracovníkov a zvýšim počet obluhujúcich pracovníkov.
    ///   -	Ak nie je voľný žiaden pracovník skupiny 1, tak zákazníkovi nastavím aktuálny čas príchodu a vložím ho do frontu čakajúcich zákazníkov.
    /// </summary>
    class PrichodZakaznikaEvent : AutoserviceEvent
    {
        public PrichodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Príchod zákazníka
        ///  Naplánujem: 
        ///   -	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.  
        ///   -	Ak je voľný pracovník 1, tak naplánujem udalosť Zadanie objednávky s pripočítaním časom z Generátora 3 – prevzatie objednávky, a znížim počet voľných pracovníkov a zvýšim počet obluhujúcich pracovníkov.
        ///   -	Ak nie je voľný žiaden pracovník skupiny 1, tak zákazníkovi nastavím aktuálny čas príchodu a vložím ho do frontu čakajúcich zákazníkov.
        /// </summary>
        public override void Execute()
        {
            var time = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
            var prichod = new PrichodZakaznikaEvent(time, ReferenceSimCore, new Zakaznik());
            ReferenceSimCore.ScheduleEvent(prichod, time);
            
            if (((AppCore) ReferenceSimCore).PocetVolnychPracovnikov1 > 0)
            {
                ((AppCore) ReferenceSimCore).PocetVolnychPracovnikov1--;
                ((AppCore) ReferenceSimCore).PocetObsluhujucichPracovnikov1++;
                time = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator3_PrevzatieObjednavky();
                var zadanie = new ZadanieObjednavky(EventTime, ReferenceSimCore, AktualnyZakaznik);
                ReferenceSimCore.ScheduleEvent(zadanie, time);

            }
            else
            {
                AktualnyZakaznik.ZacniCakatVRade(EventTime);
                ((AppCore)ReferenceSimCore).PridajZakaznika(AktualnyZakaznik);
            }
        }
    }
}
