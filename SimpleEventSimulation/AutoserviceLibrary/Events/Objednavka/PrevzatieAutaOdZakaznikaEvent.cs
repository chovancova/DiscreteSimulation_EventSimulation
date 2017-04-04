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
    ///U4 - Prevzatie auta od zákazníka
    ///Naplánujem: 
    ///-	Preparkovanie auta pred dielnou s vygenerovaným časom Generátora 5 – preparkovanie.
    ///Štatistiky: 
    ///-	S3b – Skončím počítanie doby stráveného v servise. 
    /// </summary>
    class PrevzatieAutaOdZakaznikaEvent : AutoserviceEvent
    {
        public PrevzatieAutaOdZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        ///U4 - Prevzatie auta od zákazníka
        ///Naplánujem: 
        ///-	Preparkovanie auta pred dielnou s vygenerovaným časom Generátora 5 – preparkovanie.
        ///Štatistiky: 
        ///-	S3b – Skončím počítanie doby stráveného v servise. 
        /// </summary>
        public override void Execute()
        {
            var cakanie = AktualnyZakaznik.S3_SkonciCakanie_bytia_v_servise(EventTime);
            ((AppCore)ReferenceSimCore).S3_AddValue(cakanie);

            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator5_Preparkovanie();
            var preparkovanie = new PreparkovanieAutoEvent(time, ReferenceSimCore, AktualnyZakaznik);
            ReferenceSimCore.ScheduleEvent(preparkovanie, time);
        }
    }
}
