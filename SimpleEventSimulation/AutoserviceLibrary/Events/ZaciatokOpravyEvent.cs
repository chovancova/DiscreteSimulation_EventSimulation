using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class ZaciatokOpravyEvent : AutoserviceEvent
    {
        public ZaciatokOpravyEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //vyberiem auto z frontu DIELNA
            // ak je prazdne, tak cakam 

            //vygenerujem cislo trvania opravy 
            //vegenerujem to sposobom - vygenerujem nahodny pocet oprav a pre kazdu opravu jeho trvanie a scitam to.. 
            //a naplanujem koniec opravy
            
            
        }
    }
}
