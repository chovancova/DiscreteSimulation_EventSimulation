using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerators;
using RandomGenerators.Generators;
using SimpleEventSimulation.ShopSimulation;

namespace SimpleEventSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            SimCoreShop shop = new SimCoreShop(4.0, 5.0);
            shop.Simulate();
            Console.ReadLine();
        }
    }
}
