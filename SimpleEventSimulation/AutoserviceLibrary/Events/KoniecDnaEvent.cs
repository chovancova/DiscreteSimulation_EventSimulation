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
    /// Koniec dňa 
    /// Vypočítajú sa štatistiky na konci 8 hodinového dňa.Vynuluje sa front čakajúcich zákazníkov. 
    /// Naplánujem: 
    ///	 -  Koniec dňa – s časom o osem hodín (28 800 sekúnd).
    ///	 -  Príchod zákazníka – s vygenerovaním časom z Generátora 1 – Zákazníci príchod.
    /// </summary>
    public class KoniecDnaEvent : AutoserviceEvent
    {
        public KoniecDnaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        /// Koniec dňa 
        /// Vypočítajú sa štatistiky na konci 8 hodinového dňa.Vynuluje sa front čakajúcich zákazníkov. 
        /// Naplánujem: 
        ///	 -  Koniec dňa – s časom o osem hodín (28 800 sekúnd).
        ///	 -  Príchod zákazníka – s vygenerovaním časom z Generátora 1 – Zákazníci príchod.
        /// </summary>
        public override void Execute()
        {
            if (ReferenceSimCore != null)
            {
                //pozbieram statistiky
                //todo ŠTATISTIKY

                //vynulujem den 
                ((AppCore) ReferenceSimCore).ResetFrontCakajucichZakaznikov();

                //naplanujem koniec dna 
                var time = this.EventTime + 8*60*60;
                var newEvent = new KoniecDnaEvent(time, ReferenceSimCore, new Zakaznik());
                ReferenceSimCore.ScheduleEvent(newEvent, time);

                //naplanujem prichod zakaznika
                time = this.EventTime + ((AppCore) ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
                var prichod = new PrichodZakaznikaEvent(time, ReferenceSimCore, new Zakaznik());
                ReferenceSimCore.ScheduleEvent(prichod, time);
            }
        }
    }
}
