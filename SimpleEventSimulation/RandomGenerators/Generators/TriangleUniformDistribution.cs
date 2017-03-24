using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class TriangleUniformDistribution : IGenerators
    {
        private readonly Random _randomNumberGenerator;
        private readonly int _max;
        private readonly int _min;
        private readonly int _modus;
        public TriangleUniformDistribution(int seed, int min, int max, int modus)
        {
            _randomNumberGenerator = new Random(seed);
            _min = min;
            _max = max;
            _modus = modus; 
        }

        public int GenerateInt()
        {
            throw new NotImplementedException();
        }

        public double GenerateDouble()
        {
            double v = _randomNumberGenerator.NextDouble();
            double c = _modus - _min;
            double m = _max - _min;
            if (v <= (c / m))
            {
                return _min + Math.Sqrt(v * m * c);

            }
            else
            {
                return _max - Math.Sqrt((1 - v) * m * (_max - _modus));
            }
        }
     }
}
