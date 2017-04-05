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
    /// U9 - Odchod zákazníka </summary>
    /// Uvoľním zákazníka zo systému.
    /// Naplánujem: 
    ///   -	Preparkovanie auta pred dielnou – naplánujem okamžite. 
    /// Štatistiky: 
    ///   -	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu. 
    class OdchodZakaznikaEvent : AutoserviceEvent
    {
        public OdchodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// U9 - Odchod zákazníka </summary>
        /// Uvoľním zákazníka zo systému.
        /// Naplánujem: 
        ///   -	Preparkovanie auta pred dielnou – naplánujem okamžite. 
        /// Štatistiky: 
        ///   -	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu.    
        public override void Execute()
        {
            ((AppCore) ReferenceSimCore).S4_AddValue(AktualnyZakaznik.S4_SkonciCakanie_oprava(EventTime));
            AktualnyZakaznik = null;

            var preparkovanie = new PreparkovanieAutoEvent(EventTime, ReferenceSimCore, null);
            ((AppCore)ReferenceSimCore).ScheduleEvent(preparkovanie);
        }
    }
}
