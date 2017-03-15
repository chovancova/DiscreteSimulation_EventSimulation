using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    public class DiscreteUniformDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; }
        //trvanie v dnoch - minimalne
        public int Tmin { get; private set; }
        public int Tmax { get; private set; }

        public DiscreteUniformDistribution(int seed, int min, int max)
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

        //Tmax kvoli zatvorke 
        public int GenerateInt()
        {
            return RandomNumberGenerator.Next(Tmin, Tmax + 1);
        }

        //Pravdepodobnost vyskytu kazdehoh mozneho javu X
        //Pr(X = x)= p(x) = 1 / n
        public double Probality()
        {
            return (double)1 / (Tmax - Tmin);
        }
        
    
        public double GenerateDouble()
        {
            return (double) GenerateInt();
        }
        
        //Stredna hodnota
        //E(X) = (n + 1) / 2
        public double Mean()
        {
            return (double)((Tmax + 1 - Tmin + 1) / 2 + (Tmin - 1));
        }

        //Rozptyl 
        //D(X) = (n -1 + 1)^2/12 = ( n^2 - 1 )/ 12
        public double Spread()
        {
            return (double)((Tmax + 1 - Tmin) ^ 2 - 1) / 12;
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }
    }
}
