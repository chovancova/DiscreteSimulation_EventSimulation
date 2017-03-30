using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace AutoserviceLibrary.Events
{
    class ZaciatokZadaniaObjednavkyEvent : AutoserviceEvent
    {
        public ZaciatokZadaniaObjednavkyEvent(double eventTime, SimCore simulation) : base(eventTime, simulation)
        {
        }

        public override void Execute()
        {
            //zisti ci je volne pole volnych pracovnikov skupiny 1 
            //ak ano, tak naplanuje cinnost vytvorenie a prevzatie auta od zakaznika
            //vyberie daneho pracovnika z frontu volnych pracovnikov a vymaze ho frontu
            //vyberie daneho zakaznika z frontu cakajucich na objednavku a vymaze ho z frontu

            //vygenerujem cas vytvorenia objednavky <80,160>
            //vygenerujem cas prevzatia auta od zakaznika <70.310> 

            // a casy spocitam a naplanujem na dany cas event Vytvorenie objednavky a prevzatie auta od zakaznika

            //zakaznikovi zapocitam to, aby bral do statistiky prvy vyg. cas - a to je vytvorenie objednavky
          }
    }
}
