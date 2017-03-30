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
           // TestDistributionsToFile();

            /**
             * Pred simuláciou
                1. Inicializujem čerpaciu stanicu
                2. Nastavím simulačný čas. 
                3. Naplánujem príchod zákazníka. 
                4. Začnem simulovať. 
             */
            //2.
            double simTime = 100000000.0;
            //1.
            SimCoreShop shop = new SimCoreShop(simTime, 5.0, 6.0);
            
            //3.
            Arrival a = new Arrival(0, shop, new Customer());
            shop.ScheduleEvent(a, 0);
           //4. 
            shop.Simulate();
        
            Console.ReadLine();
        }

        static void TestDistributionsToFile()
        {
            GeneratorSeed seed = new GeneratorSeed();
            int numbers = 1000000; 
             using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"exponencionalne_test_data.txt"))
            {
                ExponencionalDistribution ex = new ExponencionalDistribution(seed.GetRandomSeed(), 6);

                for (int i = 0; i < numbers; i++)
                {
                    file.WriteLine((ex.GenerateDouble().ToString().Replace(',', '.')) + "\t");
                }
            }

            //using (System.IO.StreamWriter file =
            //new System.IO.StreamWriter(@"diskretne_test_data.txt"))
            //{
            //    UniformDiscreteDistribution ex = new UniformDiscreteDistribution(seed.GetRandomSeed(), 3,6);

            //    for (int i = 0; i < numbers; i++)
            //    {
            //        file.WriteLine((ex.GenerateDouble().ToString().Replace(',', '.')) + "\t");
            //    }
            //}


        }

    }
}
