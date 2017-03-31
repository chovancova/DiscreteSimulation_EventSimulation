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
    /// Odchod zákazníka 
    /// Nastavím štatistiky: 
    ///  -	priemerný čas strávený zákazníkom v servise,
    ///  -	priemerného času stráveného zákazníkom čakaním na opravu(od ukončenia prevzatia auta). 
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
            //nastavim statistiku na odchod zakaznika zo systemu

            //  -	priemerný čas strávený zákazníkom v servise,

            //  -	priemerného času stráveného zákazníkom čakaním na opravu(od ukončenia prevzatia auta). 

        }
    }
}
