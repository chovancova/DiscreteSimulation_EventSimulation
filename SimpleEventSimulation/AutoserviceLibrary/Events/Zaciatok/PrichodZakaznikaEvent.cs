using System;
using AutoserviceLibrary.Entities;
using AutoserviceLibrary.Events.Objednavka;
using SimulationLibrary;

namespace AutoserviceLibrary.Events.Zaciatok
{
    /// <summary>
    ///     U1 - Príchod zákazníka
    ///     Naplánujem:
    ///     -	Front čakajúcich zákazníkov - Vložím do frontu čakajúcich zákazníkov  zákazníka s aktuálnym časom príchodu.
    ///     -	Príchod nového zákazníka s vygenerovaným časom z Generátora 1.
    ///     -	Začiatok spracovania objednávky - naplánujem udalosť okamžite, ak je volny pracovnik.
    ///     Štatistiky:
    ///     -	S1a - Začnem merať čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky.
    ///     -	S2a  – Pripočítam jedného zákazníka v rade čakajúcich zákazníkov.
    ///     -	S3a - Začnem počítať čas strávený zákazníkov v servise.
    /// </summary>
    internal class PrichodZakaznikaEvent : AutoserviceEvent
    {
        public PrichodZakaznikaEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik)
            : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        /// <summary>
        ///     U1 - Príchod zákazníka
        ///     Naplánujem:
        ///     -	Začiatok spracovania objednávky - naplánujem udalosť okamžite, ak je volny pracovnik.
        ///     -	Front čakajúcich zákazníkov - Vložím do frontu čakajúcich zákazníkov zákazníka s aktuálnym časom príchodu.
        ///     -	Príchod nového zákazníka s vygenerovaným časom z Generátora 1.
        ///     Štatistiky:
        ///     -	S1a - Začnem merať čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky.
        ///     -	S2a  – Pripočítam jedného zákazníka v rade čakajúcich zákazníkov.
        ///     -	S3a - Začnem počítať čas strávený zákazníkov v servise.
        /// </summary>
        public override void Execute()
        {
            if (AktualnyZakaznik == null) throw new Exception("NULL zakaznik. ");

            AktualnyZakaznik.S1_ZacniCakanie_front_cakajucich_zakaznikov(EventTime);
            AktualnyZakaznik.S3_ZacniCakanie_bytia_v_servise(EventTime);
            ((AppCore) ReferenceSimCore).S2_AddValue();

            if (((AppCore) ReferenceSimCore).JeVolnyPracovnik1())
            {
                //obsad volneho pracovnik
                ((AppCore) ReferenceSimCore).ObsadPracovnikaSkupiny1();
                var zadanie = new ZaciatokSpracovaniaObjednavkyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik);
                ReferenceSimCore.ScheduleEvent(zadanie);
            }
            else
            {
                //inak vlozim zakaznika do frontu cakajucich zakaznikov
                ((AppCore) ReferenceSimCore).Front_CakajuciZakaznici_PridajZakaznika(AktualnyZakaznik);
            }

            //naplanujem novy prichod zákazníka
            var time = EventTime + ((AppCore) ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
            var prichod = new PrichodZakaznikaEvent(time, ReferenceSimCore, new Zakaznik());
            ReferenceSimCore.ScheduleEvent(prichod);
        }
    }
}