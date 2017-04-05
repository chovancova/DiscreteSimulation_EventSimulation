using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events.Zaciatok
{
    /**
        Začiatok replikácie
        Vytvorím nového zákazníka. 
        Naplánujem: 
        -	Príchod zákazníka – s vygenerovaním časom z Generátora 1 – Zákazníci príchod. 
        -	Koniec dňa – s časom o osem hodín (28 800 sekúnd).
        */
   internal class ZaciatokReplikacieEvent : AutoserviceEvent
    {
        public ZaciatokReplikacieEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /**
        Začiatok replikácie
        Vytvorím nového zákazníka. 
        Naplánujem: 
        -	Príchod zákazníka – s vygenerovaním časom z Generátora 1 – Zákazníci príchod. 
        -	Koniec dňa – s časom o osem hodín (28 800 sekúnd).
        */
        public override void Execute()
        {
            //naplanujem prichod zakaznika
            var time = ((AppCore) ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
            var prichod = new PrichodZakaznikaEvent(time, ReferenceSimCore, new Zakaznik());
            ((AppCore) ReferenceSimCore).ScheduleEvent(prichod);

            var koniec = new KoniecDnaEvent(EventTime, ReferenceSimCore, null);
            ((AppCore) ReferenceSimCore).ScheduleEvent(koniec);
        }
    }
}