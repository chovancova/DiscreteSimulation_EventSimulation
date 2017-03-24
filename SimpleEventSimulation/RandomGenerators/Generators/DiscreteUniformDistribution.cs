﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    //The uniform distribution models a situation where a fixed number of outcomes all have an equal probability of occurring.
   public class DiscreteUniformDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; }
        //trvanie v dnoch - minimalne
        public int Tmin { get; private set; }
        public int Tmax { get; private set; }

        public DiscreteUniformDistribution(int seed, int min, int max)
        {
            RandomNumberGenerator = new Random(seed);
            //if (min < max)
            //{
            this.Tmin = min;
            this.Tmax = max;
            //}
            //else
            //{
            //    this.Tmin = max;
            //    this.Tmax = min;
            //}
        }

        //Tmax kvoli zatvorke 
        public int GenerateInt()
        {
            if (Tmin == Tmax) return Tmin;
           return (RandomNumberGenerator.Next((Tmin), (Tmax+1)));
        //x = r * (b-a) + a
        //   return (int) ((1-RandomNumberGenerator.NextDouble())*(Tmax+1 - Tmin) + Tmin);

          //  return RandomNumberGenerator.Next(Tmax+1) + Tmin ;
        }

        //Pravdepodobnost vyskytu kazdehoh mozneho javu X
        //Pr(X = x)= p(x) = 1 / n
        public double Probality()
        {
            return (double)1 / (Tmax - Tmin);
        }
        
    
        public double GenerateDouble()
        {
            throw new NotImplementedException();
            //return RandomNumberGenerator.NextDouble() * (Tmax - Tmin) + Tmin;
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
