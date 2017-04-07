using System;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    ///     U4 - Prevzatie auta od zákazníka
    ///     Naplánujem:
    ///     -	Preparkovanie auta pred dielnou s vygenerovaným časom Generátora 5 – preparkovanie.
    ///     Štatistiky:
    ///     -	S3b – Skončím počítanie doby stráveného v servise.
    ///     -	S4a – Začnem merať čas strávený čakaním na opravu. (od ukončenia prevzatia auta do servisu)
    /// </summary>
    internal class PrevzatieAutaOdZakaznikaEvent : AutoserviceEvent
    {
        public PrevzatieAutaOdZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U4 - Prevzatie auta od zákazníka
        ///     Naplánujem:
        ///     -	Preparkovanie auta pred dielnou s vygenerovaným časom Generátora 5 – preparkovanie.
        ///     Štatistiky:
        ///     -	S3b – Skončím počítanie doby stráveného v servise.
        ///     -	S4a – Začnem merať čas strávený čakaním na opravu. (od ukončenia prevzatia auta do servisu)
        /// </summary>
        public override void Execute()
        {
            if (AktualnyZakaznik == null) throw new Exception("NULL zakaznik. ");

            //statistiky
            ((AppCore) ReferenceSimCore).S3_AddValue(AktualnyZakaznik.S3_SkonciCakanie_bytia_v_servise(EventTime));
            AktualnyZakaznik.S4_ZacniCakanie_oprava(EventTime);

            //naplanujem preparkovanie auta
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator5_Preparkovanie();
            var preparkovanie = new PreparkovanieAutoEvent(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(preparkovanie);
        }
    }
}