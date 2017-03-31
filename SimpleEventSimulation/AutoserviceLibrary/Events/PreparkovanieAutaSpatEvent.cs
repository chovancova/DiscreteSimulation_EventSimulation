using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class PreparkovanieAutaSpatEvent : AutoserviceEvent
    {
        public PreparkovanieAutaSpatEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        public override void Execute()
        {
            //vygenerujem dobu oblsuhy zakaznika <123, 257>
            //naplanujem event odchod zakaznika

            //vypocitam statistiku pre zakaznika v systeme... 
            //

            //ak je front OPRAVENE AUTA volny... 
                //ak je front cakajucich na zadanie objednavky prazdny, 
                //tak vlozim pracovnika do frontu volnych pracovnikov skupiny 1
                //inak 
                //naplanujem zaciatok zadania objednavky
                //vyberiem jedneho zakaznika z frontu a odstranim ho .. a nastavim ho danej udalosti

            //ak je nieco vo fronte OPRAVENE AUTA
                //zoberiem prve auto z opravenych aut
                //naplanujem event v case 
                //

            //TODO 

            //////////////////////
            //////////////////////
            ////////////////////// 

        }
    }
}
