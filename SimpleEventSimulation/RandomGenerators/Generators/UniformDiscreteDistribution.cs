using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    //The uniform distribution models a situation where a fixed number of outcomes all have an equal probability of occurring.
   public class UniformDiscreteDistribution : IGenerators
   {
       private readonly Random _randomNumberGenerator;
        //trvanie v dnoch - minimalne
       private readonly int _tmin;
       private readonly int _tmax;
        public UniformDiscreteDistribution(int seed, int min, int max)
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
            if (_tmin == _tmax) return _tmin;
            return _randomNumberGenerator.Next(_tmin,_tmax+1) ;
        }

        //Pravdepodobnost vyskytu kazdehoh mozneho javu X
        //Pr(X = x)= p(x) = 1 / n
        public double P()
        {
            return (double)1 / (_tmax - _tmin);
        }
        
    
        public double GenerateDouble()
        {
            throw new NotImplementedException();
            //return RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin;
        }

        //Stredna hodnota
        //E(X) = (n + 1) / 2
        public double E()
        {
            return (double)((_tmax + 1 - _tmin + 1) / 2 + (_tmin - 1));
        }

        //Rozptyl 
        //D(X) = (n -1 + 1)^2/12 = ( n^2 - 1 )/ 12
        public double D()
        {
            return (double)((_tmax + 1 - _tmin) ^ 2 - 1) / 12;
        }

      
    }
}
