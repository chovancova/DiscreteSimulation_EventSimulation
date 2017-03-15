using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationLibrary;

namespace SimpleEventSimulation.ShopSimulation
{
   abstract class SimEventShop: SimEvent
    {
        //Customer in event
        public Customer CurrentCustomer { get; set; }
    }
}
