using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        private Queue<Customer> WaitingQueue { get; set; }
        //for statistics
        public int NumberOfCustomers { get; set; }
        public double TotalWaitingTime { get; set; }
        public double LengthOfFront { get; set; }
        public int LastCount { get; set;  }
        public double LastChangedTime { get; set;  }
        private int _iteration;
        public SimCoreShop(double maxSimulationTime, IGenerators[] generators) : base(generators)
        {
            TimeEnd = maxSimulationTime;
            IsServed = false;
            WaitingQueue = new Queue<Customer>();
            //statistics
            NumberOfCustomers = 0;
            TotalWaitingTime = 0;
            LastCount = -1;
            _iteration = 0;
            LengthOfFront = 0;
            LastChangedTime = 0; 
        }

        public SimCoreShop(double maxSimulationTime, double exp1, double exp2) : base()
        {
            TimeEnd = maxSimulationTime;

            GeneratorSeed seed = new GeneratorSeed();
            //Mo
            ExponencionalDistribution d1 = new ExponencionalDistribution(seed.GetRandomSeed(), exp1);
            //Mp
            ExponencionalDistribution d2 = new ExponencionalDistribution(seed.GetRandomSeed(), exp2);

            IGenerators[] generatorses = new[] { d1, d2 };

            this.Generators = generatorses;
            IsServed = false;
            WaitingQueue = new Queue<Customer>();
            //statistics
            NumberOfCustomers = 0;
            TotalWaitingTime = 0;
            LastCount = -1;
            _iteration = 0;
            LengthOfFront = 0;
            LastChangedTime = 0;
        }
        //for statistics
        public void AddStatisticsChangeOfFront()
        {
            if (LastCount >= 0)
            {
                LengthOfFront += WaitingQueue.Count*(CurrentTime - LastChangedTime);
            }
            LastCount = WaitingQueue.Count;
            LastChangedTime = CurrentTime; 
        }

        public void AddStatisticsWaitingTimeStat(double waitingtime)
        {
            NumberOfCustomers++;
            TotalWaitingTime += waitingtime;
            _iteration++;
            if (_iteration == 10000)
            {
                _iteration = 0;
                Console.WriteLine(TotalWaitingTime / NumberOfCustomers);
                Console.WriteLine(LengthOfFront/CurrentTime);
                Console.WriteLine();
            }
        }

        public Customer NextCustomer()
        {
            if (WaitingQueue.Count == 0)
            {
                return null; 
            }
            AddStatisticsChangeOfFront();
            return WaitingQueue.Dequeue();
        }

        public void AddCustomer(Customer customer)
        {
            AddStatisticsChangeOfFront();
            WaitingQueue.Enqueue(customer);
        }
    }
}
