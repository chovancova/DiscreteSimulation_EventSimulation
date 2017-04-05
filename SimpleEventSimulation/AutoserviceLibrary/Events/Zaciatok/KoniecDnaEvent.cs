using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    ///     U0 - Koniec dňa
    ///     Vynuluje sa front čakajúcich zákazníkov.
    ///     Naplánujem:
    ///     -	Koniec dňa – s časom o osem hodín (28 800 sekúnd).
    ///     Štatistiky:
    ///     -	S11 – počet zákazníkov v rade na konci dňa.
    /// </summary>
    internal class KoniecDnaEvent : AutoserviceEvent
    {
        public KoniecDnaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U0 - Koniec dňa
        ///     Vynuluje sa front čakajúcich zákazníkov.
        ///     Naplánujem:
        ///     -	Koniec dňa – s časom o osem hodín (28 800 sekúnd).
        ///     Štatistiky:
        ///     -	S11 – počet zákazníkov v rade na konci dňa.
        /// </summary>
        public override void Execute()
        {
            if (ReferenceSimCore != null)
            {
                //vynulujem den 
                ((AppCore) ReferenceSimCore).Front_CakajuciZakaznici_Reset();

                //naplanujem koniec dna 
                var time = EventTime + AppCore.DlzkaDnaSekundy;
                var newEvent = new KoniecDnaEvent(time, ReferenceSimCore, new Zakaznik());
                ReferenceSimCore.ScheduleEvent(newEvent);
            }
        }
    }
}