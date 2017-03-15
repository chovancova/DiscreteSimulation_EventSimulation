using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class ExponencionalDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; }

        public double lamda { get; set; }
        private double median;

        public ExponencionalDistribution(int seed, double median)
        {
            RandomNumberGenerator = new Random(seed);
            lamda = 1.0 / median;
        }
        public double GetValue()
        {
            return Math.Log((1 - RandomNumberGenerator.NextDouble())) / (-lamda);
        }

        public double GenerateDouble()
        {
            return Math.Log((1 - RandomNumberGenerator.NextDouble())) / (-lamda);
        }

        public int GenerateInt()
        {
            return (int)(Math.Log((1 - RandomNumberGenerator.NextDouble())) / (-lamda));
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }

        public double Mean()
        {
            return median;
        }

        public double Spread()
        {
            return lamda;
        }
    }
}
