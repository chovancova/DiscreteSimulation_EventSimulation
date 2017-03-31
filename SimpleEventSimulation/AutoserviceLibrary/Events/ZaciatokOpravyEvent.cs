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
    /// Začiatok opravy
    /// Naplánujem: 
    ///  -	Koniec opravy – ak nie je front pokazených áut prázdny.Zistím si počet opráv, ktoré má auto, a vygenerujem Generátorom 7 – doba opravy jednotlivé doby opravy auta.Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.
    ///  -	Ak je front pokazených áut prázdny, tak sa zvýši počet voľných pracovníkov o jedna, a zníži počet obsadených o jedna. 
    /// </summary>
    class ZaciatokOpravyEvent : AutoserviceEvent
    {
        public ZaciatokOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }
        /// <summary>
        /// Začiatok opravy
        /// Naplánujem: 
        ///  -	Koniec opravy – ak nie je front pokazených áut prázdny.Zistím si počet opráv, ktoré má auto, a vygenerujem Generátorom 7 – doba opravy jednotlivé doby opravy auta.Súčet počtu opráv s dobami naplánujem udalosť koniec opravy, kde bude auto kompletne opravené.
        ///  -	Ak je front pokazených áut prázdny, tak sa zvýši počet voľných pracovníkov o jedna, a zníži počet obsadených o jedna. 
        /// </summary>
        public override void Execute()
        {
            var auto = ((AppCore)ReferenceSimCore).DalsieAuto();
            if (auto != null)
            {
                //koniec opravy
                int sucet = 0;
                for (int i = 0; i < auto.PocetOprav; i++)
                {
                    sucet += ((AppCore)ReferenceSimCore).Gen.Generator7_DobaOpravy();
                }
                var time = EventTime + sucet;
                auto.CelkovaDobaOpravy = sucet;
                var koniec = new KoniecOpravyEvent(time, ReferenceSimCore, auto);
                ReferenceSimCore.ScheduleEvent(koniec, time);
            }
            else
            {
                ((AppCore)ReferenceSimCore).PocetObsluhujucichPracovnikov2--;
                ((AppCore)ReferenceSimCore).PocetVolnychPracovnikov2++;
                //todo statistiky mozno
            }
        }
    }
}
