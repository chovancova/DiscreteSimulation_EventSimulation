using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    ///     U9 - Odchod zákazníka
    /// </summary>
    /// Uvoľním zákazníka zo systému.
    /// Naplánujem: 
    /// -	Preparkovanie auta pred dielnou – naplánujem okamžite. 
    /// Štatistiky: 
    /// -	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu.
    internal class OdchodZakaznikaEvent : AutoserviceEvent
    {
        public OdchodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U9 - Odchod zákazníka
        /// </summary>
        /// Uvoľním zákazníka zo systému.
        /// Naplánujem: 
        /// -	Preparkovanie auta pred dielnou – naplánujem okamžite. 
        /// Štatistiky: 
        /// -	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu.
        public override void Execute()
        {
            ((AppCore) ReferenceSimCore).S4_AddValue(AktualnyZakaznik.S4_SkonciCakanie_oprava(EventTime));
            ((AppCore)ReferenceSimCore).S5_AddValue(AktualnyZakaznik.S5_SkonciCakanie_system(EventTime));
            AktualnyZakaznik = null;

            var preparkovanie = new PreparkovanieAutoEvent(EventTime, ReferenceSimCore, null);
            ((AppCore) ReferenceSimCore).ScheduleEvent(preparkovanie);

            ((AppCore)ReferenceSimCore).PocetLudiOdisli++;
        }
    }
}