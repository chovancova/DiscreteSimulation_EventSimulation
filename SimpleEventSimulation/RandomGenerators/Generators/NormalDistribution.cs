using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    //http://www.csharpcity.com/reusable-code/random-number-generators/
    public class NormalDistribution : IGenerators
    {

        private readonly Random _randomNumberGenerator;
        private readonly Random _randomNumberGenerator2;
        private readonly double _mean;
        private readonly double _standardDeviation;


        public NormalDistribution(int seed,int seed2, double mean, double stdev)
        {
            _randomNumberGenerator = new Random(seed);
            _randomNumberGenerator2 = new Random(seed2);

            _mean = mean; //190
            _standardDeviation = stdev;//120

            }
        private double _nextGaussian;
        private bool _haveNextGaussian = false;

        /** Return a random double drawn from a Gaussian distribution with mean 0 and variance 1. */
        public  double NextGaussian()
        {
            //http://www.java2s.com/Code/Java/Development-Class/ReturnasamplefromtheGammaPoissionGaussiandistributionwithparameterIA.htm
            if (!_haveNextGaussian)
            {
                double v1 = 1 - _randomNumberGenerator.NextDouble();
                double v2 = 1 - _randomNumberGenerator2.NextDouble();

                double x1, x2;
                x1 = Math.Sqrt(-2 * Math.Log(v1)) * Math.Cos(2 * Math.PI * v2);
                x2 = Math.Sqrt(-2 * Math.Log(v1)) * Math.Sin(2 * Math.PI * v2);

                _nextGaussian = x2;
                _haveNextGaussian = true;
                return x1;
            }
            else
            {
                _haveNextGaussian = false;
                return _nextGaussian;
            }
        }

        /** Return a random double drawn from a Gaussian distribution with mean m and variance s2. */
        public  double NextGaussian(double m, double s2)
        {

            //NORM(0,1) = hodnoty od -4 - +4 
            double a = NextGaussian();

            return (a * Math.Sqrt(s2)) +m;
        }

        public double Next()
        {
            //https://gist.github.com/tansey/1444070
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            double x1 = 1 - _randomNumberGenerator.NextDouble();
            double x2 = 1 - _randomNumberGenerator2.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);

        //+-120           + 190
            return (y1 *(1) )  + _mean;
        }

        public int GenerateInt()
        {
            return (int) Next();
        }

        public double GenerateDouble()
        {
           double a =  NextGaussian(_mean, _standardDeviation);
            
            if (a < (_mean - _standardDeviation))
            {
                Console.WriteLine(a);
            }
            if (a > (_mean + _standardDeviation))
            {
                Console.WriteLine(a);
            }
            return a;
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }

        public double Mean()
        {
            return _mean;
        }

        public double Spread()
        {
            throw new NotImplementedException();
        }

    }
}
