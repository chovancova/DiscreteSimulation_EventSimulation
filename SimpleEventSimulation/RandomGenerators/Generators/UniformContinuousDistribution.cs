using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class UniformContinuousDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; set; }
        //trvanie v dnoch - minimalne
        public int Tmin { get; private set; }
        public int Tmax { get; private set; }

        public UniformContinuousDistribution(int seed, int min, int max)
        {
            RandomNumberGenerator = new Random(seed);
            if (min < max)
            {
                this.Tmin = min;
                this.Tmax = max;
            }
            else
            {
                this.Tmin = max;
                this.Tmax = min;
            }
        }

        public int GenerateInt()
        {
            throw new NotImplementedException();

            // return (int)(RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin);
        }

        public int GenerateRounded()
        {
            return (int)Math.Round((RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin));
        }

        public double GenerateDouble()
        {
            return (double)(RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin);
        }

        //Funkcia hustoty rozdelenia
        // f(x) = 1 / ( B - A )  
        public double DensityDistribution()
        {
            return (double)1 / (Tmax - Tmin);
        }

        //Stredna hodnota
        //E(x) = ( A + B ) / 2
        public double Mean()
        {
            return (double)(Tmin + Tmax) / 2;
        }

        //Rozptyl
        //D(x) = (B - A)^2 / 12
        public double Spread()
        {
            return (double)((Tmax - Tmin) ^ 2) / 12;
        }

       
    }
}
