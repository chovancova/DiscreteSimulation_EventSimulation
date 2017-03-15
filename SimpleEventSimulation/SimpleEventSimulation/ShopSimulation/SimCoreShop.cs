﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerators;
using RandomGenerators.Generators;
using SimulationLibrary;

namespace SimpleEventSimulation.ShopSimulation
{
    public class SimCoreShop : SimCore
    {
        public bool IsServed { get; set; }

        public LinkedList<Customer> WaitingQueue { get; set; }
        
        public SimCoreShop(IGenerators[] generators) : base(generators)
        {  
            IsServed = false;
            WaitingQueue = new LinkedList<Customer>();
       }

        public SimCoreShop(double exp1, double exp2) : base()
        {
            GeneratorSeed seed = new GeneratorSeed();
            //Mo
            ExponencionalDistribution d1 = new ExponencionalDistribution(seed.GetRandomSeed(), exp1);
            //Mp
            ExponencionalDistribution d2 = new ExponencionalDistribution(seed.GetRandomSeed(), exp2);

            IGenerators[] generatorses = new[] { d1, d2 };

            this.Generators = generatorses;
        }
    }
}