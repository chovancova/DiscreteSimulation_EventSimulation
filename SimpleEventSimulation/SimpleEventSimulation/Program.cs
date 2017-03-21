using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerators;
using RandomGenerators.Generators;
using SimpleEventSimulation.ShopSimulation;
using SimpleEventSimulation.ShopSimulation.Events;

namespace SimpleEventSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExponencionalDistribution ex = new ExponencionalDistribution(342342434, 5);
            //for (int i = 0; i < 46288; i++)
            //{
            //    Console.Write(ex.GenerateDouble() + "\t");
            //}

            /**
             * Pred simuláciou
                1. Inicializujem čerpaciu stanicu
                2. Nastavím simulačný čas. 
                3. Naplánujem príchod zákazníka. 
                4. Začnem simulovať. 
             */
            //2.
            int simTime = 1000000;
            //1.
            SimCoreShop shop = new SimCoreShop(simTime, 4.0, 5.0);
            
            //3.
            Arrival a = new Arrival(0, shop, new Customer());
            shop.ScheduleEvent(a, 0);
           //4. 
            shop.Simulate();
        
            Console.ReadLine();
        }
    }
}
