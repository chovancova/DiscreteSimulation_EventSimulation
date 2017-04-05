using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    internal class KoniecOpravyEvent : AutoserviceEvent

    {
        public KoniecOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U7 - Koniec opravy
        /// </summary>
        /// Naplánujem:
        /// - Začiatok opravy - Ak nie je front pokazených prázdny, tak vyberiem pokazené auto, a naplánujem udalosť okamžite.
        /// - Uvoľnenie pracovníka – ak je front pokazených áut, tak zvýšim počet voľných pracovníkov.
        /// - Preparkovanie auta späť zákazníkovi – ak je voľný pracovník skupiny 1. Vygenerujem čas Generátorom 5 – Preparkovanie auta späť.
        /// - Front opravených áut – ak nie je voľný pracovník skupiny 1, tak opravené auto vložím do frontu opravených áut, kde to auto bude čakať na vyzdvihnutie pracovníkom skupiny 1.
        public override void Execute()
        {
            if (((AppCore) ReferenceSimCore).JeVolnyPracovnik1())
            {
                ((AppCore) ReferenceSimCore).ObsadPracovnikaSkupiny1();
                //preparkujem spat
                var casPreparkovania =EventTime+ ((AppCore) ReferenceSimCore).Gen.Generator5_Preparkovanie();
                var preparkovanieSpat = new PreparkovanieAutaSpatEvent(casPreparkovania, ReferenceSimCore,
                    AktualnyZakaznik);
                ((AppCore) ReferenceSimCore).ScheduleEvent(preparkovanieSpat);
            }
            else
            {
                AktualnyZakaznik.Typ = TypZakaznika.OpraveneAuto;
                //vlozim do frontu opravenych aut
                ((AppCore) ReferenceSimCore).Front_OpraveneAuta_Pridaj(AktualnyZakaznik);
            }


            //vyberiem auto z frontu 
            var pokazeneAuto = ((AppCore)ReferenceSimCore).Front_PokazeneAuta_Vyber();

            if (pokazeneAuto!=null)
            {
                               //zacnem opravovat dalsie auto
                var zacniOpravovat = new ZaciatokOpravyEvent(EventTime, ReferenceSimCore, pokazeneAuto);
                ((AppCore) ReferenceSimCore).ScheduleEvent(zacniOpravovat);
            }
            else
            {
                //uvolni pracovnika sk. 2
                ((AppCore) ReferenceSimCore).UvolniPracovnikaSkupiny2();
            }
        }
    }
}