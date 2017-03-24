using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class ExponencionalDistribution : IGenerators
    {
        private readonly Random _randomNumberGenerator;
        private readonly double _lambda;

        public ExponencionalDistribution(int seed, double median)
        {
            _randomNumberGenerator = new Random(seed);
            _lambda = 1.0 / median;
        }
        public double GenerateDouble()
        {
            return Math.Log((1 - _randomNumberGenerator.NextDouble())) / (-_lambda);
        }

        public int GenerateInt()
        {
            return (int)(Math.Log((1 - _randomNumberGenerator.NextDouble())) / (-_lambda));
        }
    }
}
