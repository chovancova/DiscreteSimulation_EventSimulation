using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class TriangleUniformDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; }
        public int Max { get; private set; }
        public int Min { get; private set; }
        public int Modus { get; private set; }
        public TriangleUniformDistribution(int seed, int min, int max, int modus)
        {
            RandomNumberGenerator = new Random(seed);
            Min = min;
            Max = max;
            Modus = modus; 
        }

        public int GenerateInt()
        {
            return (int) Math.Ceiling(GenerateDouble());
        }

        public double GenerateDouble()
        {
            double v = RandomNumberGenerator.NextDouble();
            double c = Modus - Min;
            double m = Max - Min;
            if (v <= (c / m))
            {
                return Min + Math.Sqrt(v * m * c);

            }
            else
            {
                return Max - Math.Sqrt((1 - v) * m * (Max - Modus));
            }
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }

        public double Mean()
        {
            return Modus; 
        }

        public double Spread()
        {
            throw new NotImplementedException();
        }
    }
}
