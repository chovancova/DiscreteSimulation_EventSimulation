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
    ///U1 - Príchod zákazníka
    ///Naplánujem: 
    ///-	Front čakajúcich zákazníkov - Vložím do frontu čakajúcich zákazníkov  zákazníka s aktuálnym časom príchodu.
    ///-	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.  
    ///-	Začiatok spracovania objednávky - naplánujem udalosť okamžite.
    ///Štatistiky: 
    ///-	S1a - Začnem merať čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky.  
    ///-	S2a  – Pripočítam jedného zákazníka v rade čakajúcich zákazníkov.
    ///-	S3a - Začnem počítať čas strávený zákazníkov v servise.
    /// </summary>
    class PrichodZakaznikaEvent : AutoserviceEvent
    {
        public PrichodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U1 - Príchod zákazníka
        ///Naplánujem: 
         ///-	Začiatok spracovania objednávky - naplánujem udalosť okamžite.
        ///-	Front čakajúcich zákazníkov - Vložím do frontu čakajúcich zákazníkov zákazníka s aktuálnym časom príchodu.
        ///-	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.  
        ///Štatistiky: 
        ///-	S1a - Začnem merať čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky.  
        ///-	S2a  – Pripočítam jedného zákazníka v rade čakajúcich zákazníkov.
        ///-	S3a - Začnem počítať čas strávený zákazníkov v servise.
        /// </summary>
        public override void Execute()
        {
                AktualnyZakaznik.ZacniCakatVRade(EventTime);
                AktualnyZakaznik.ZacniCakatNaVybavenieObjednavky(EventTime);
                ((AppCore)ReferenceSimCore).PridajZakaznika(AktualnyZakaznik);

             var zadanie = new ZaciatokSpracovaniaObjednavkyEvent(EventTime, ReferenceSimCore, null);
            ReferenceSimCore.ScheduleEvent(zadanie, EventTime);


            var time2 = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
            var prichod = new PrichodZakaznikaEvent(time2, ReferenceSimCore, new Zakaznik());
            ReferenceSimCore.ScheduleEvent(prichod, time2);
        }
    }
}
