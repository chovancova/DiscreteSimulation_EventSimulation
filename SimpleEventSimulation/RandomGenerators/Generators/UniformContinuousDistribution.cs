using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public class UniformContinuousDistribution : IGenerators
   {
       private readonly Random _randomNumberGenerator;
        private readonly int _tmin;
        private readonly int _tmax;

        public UniformContinuousDistribution(int seed, int min, int max)
        {
            _randomNumberGenerator = new Random(seed);
            if (min < max)
            {
                this._tmin = min;
                this._tmax = max;
            }
            else
            {
                this._tmin = max;
                this._tmax = min;
            }
        }

        public int GenerateInt()
        { 
            throw new NotImplementedException();

            // return (int)(RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin);
        }

        public int GenerateRounded()
        {
            throw new NotImplementedException();

            //return (int)Math.Round((_randomNumberGenerator.NextDouble() * (_tmax - _tmin) + _tmin));
        }

        public double GenerateDouble()
        {
            return (double)(_randomNumberGenerator.NextDouble() * (_tmax - _tmin) + _tmin);
        }

        //Funkcia hustoty rozdelenia
        // f(x) = 1 / ( B - A )  
        public double f()
        {
            return (double)1 / (_tmax - _tmin);
        }

        //Stredna hodnota
        //E(x) = ( A + B ) / 2
        public double E()
        {
            return (double)(_tmin + _tmax) / 2;
        }

        //Rozptyl
        //D(x) = (B - A)^2 / 12
        public double D()
        {
            return (double)((_tmax - _tmin) ^ 2) / 12;
        }

       
    }
}
