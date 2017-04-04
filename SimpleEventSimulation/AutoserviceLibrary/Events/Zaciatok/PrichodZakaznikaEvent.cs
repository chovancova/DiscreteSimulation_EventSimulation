using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    /// <summary>
    ///     U1 - Príchod zákazníka
    ///     Naplánujem:
    ///     -	Front čakajúcich zákazníkov - Vložím do frontu čakajúcich zákazníkov  zákazníka s aktuálnym časom príchodu.
    ///     -	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.
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
        ///     -	Príchod ďalšieho zákazníka s vygenerovaným časom z Generátora 1.
        ///     Štatistiky:
        ///     -	S1a - Začnem merať čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky.
        ///     -	S2a  – Pripočítam jedného zákazníka v rade čakajúcich zákazníkov.
        ///     -	S3a - Začnem počítať čas strávený zákazníkov v servise.
        /// </summary>
        public override void Execute()
        {
            
            var time = EventTime + ((AppCore)ReferenceSimCore).Gen.Generator1_ZakazniciPrichod();
            var prichod = new PrichodZakaznikaEvent(time, ReferenceSimCore, new Zakaznik());
            ReferenceSimCore.ScheduleEvent(prichod, time);

            if (((AppCore) ReferenceSimCore).JeVolnyPracovnik1())
            {
                AktualnyZakaznik.S3_ZacniCakanie_bytia_v_servise(EventTime);

                var zadanie = new ZaciatokSpracovaniaObjednavkyEvent(EventTime, ReferenceSimCore, AktualnyZakaznik);
                ReferenceSimCore.ScheduleEvent(zadanie, EventTime);
            }
            else
            {
                AktualnyZakaznik.S1_ZacniCakanie_front_cakajucich_zakaznikov(EventTime);
                AktualnyZakaznik.S3_ZacniCakanie_bytia_v_servise(EventTime);
                ((AppCore) ReferenceSimCore).S2_AddValue();
                ((AppCore) ReferenceSimCore).Front_CakajuciZakaznici_PridajZakaznika(AktualnyZakaznik);
            }
                       
        }
    }
}