using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class KoniecOpravyEvent : AutoserviceEvent

    {
        public KoniecOpravyEvent(double eventTime, SimCore simulation, Zakaznik aktualnyZakaznik) : base(eventTime, simulation, aktualnyZakaznik)
        {
        }

        public override void Execute()
        {
            //Ak je vo front DIELNA nejake auto
            //naplanuj zaciatok opravy okamzite dalsieho auta

            //ak je front aut  prazdny, tak naplanujem cakanie


            // opravene auto 
            //ak je v skupine 1 niekto volny, 
            //treba dane auto preparkovat
            // a auto ide do parametru daalsieho eventu
            //naplanujem udalost - Preparkovanie auta spat zakaznikovi



            //ak nie je ziaden volny zakaznik typu 1 volny, 
            //tak             
            // vlozim opravene auto do frontu OPRAVENE AUTA



        }
    }
}
