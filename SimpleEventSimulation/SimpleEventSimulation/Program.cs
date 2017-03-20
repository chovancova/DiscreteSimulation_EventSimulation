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
             //1.
            SimCoreShop shop = new SimCoreShop(4.0, 5.0);
            //2.
            int simTime = 1000000;
            //3.
            Arrival a = new Arrival();
            a.CurrentCustomer = new Customer();
            a.ReferenceSimCore = shop;
            shop.PlanEvent(a);
            a.Execute();
            //4. 
            shop.Simulate(simTime);
            
            double avarage = shop.Statistic.MeanAvarage;
            avarage = avarage / shop.CurrentTime;
            Console.WriteLine(avarage);
            Console.ReadLine();
        }
    }
}
