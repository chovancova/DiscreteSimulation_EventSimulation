using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class PreparkovanieAutoEvent: AutoserviceEvent
    {
        public PreparkovanieAutoEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            // ak je parkovisko opravenych aut prazdne a nikto nestoji 
            // a je volny pracovnik 
            // daj do frontu volnych pracovnikov
            //inak naplanuj event zaciatok objednavania v case TROJ

            //auto zakaznika vlozim do frontu DIELNA 


            // ak nie je ziadne auto v dielni, a ak je volny pracovnik 
            //vyberiem jedneho pracovnika zo frontu volnych pracovnikov 
            //naplanujem zaciatok opravy

            //ak je nejake auto opravene
            //nastavim cas z TROJ 
            //vyberiem z frontu opravenych aut jedno
            //naplanujem udalost Preparkovanie auta spat zakaznikovi 



            //sledujem statistiku bytia auta - zakaznika v oprave
        }
    }
}
