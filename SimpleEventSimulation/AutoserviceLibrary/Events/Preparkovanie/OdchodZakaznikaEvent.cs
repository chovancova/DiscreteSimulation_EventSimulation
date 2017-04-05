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
    ///U9 - Odchod zákazníka
    ///Uvoľním zákazníka zo systému. 
    ///Štatistiky: 
    ///-	S4b - Skončím počítanie času stráveným zákazníkom čakaním na opravu.    
    /// </summary>
    class OdchodZakaznikaEvent : AutoserviceEvent
    {
        public OdchodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Odchod zákazníka 
        /// Nastavím štatistiky: 
        ///  -	priemerný čas strávený zákazníkom v servise,
        ///  -	priemerného času stráveného zákazníkom čakaním na opravu(od ukončenia prevzatia auta). 
        /// </summary>
        public override void Execute()
        {
            ((AppCore) ReferenceSimCore).S4_AddValue(AktualnyZakaznik.S4_SkonciCakanie_oprava(EventTime));

            ReferenceSimCore.ScheduleEvent(new UvolniPracovnikaEvent(EventTime, ReferenceSimCore, null), EventTime);
        }
    }
}
