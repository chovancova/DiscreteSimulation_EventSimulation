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
        ////   public Random RandomNumberGenerator { get; }


        ////   private int Min { get; set; }
        ////   private int Max { get; set; }
        ////   public double StandardDeviation { get; set; }
        ////   public double Mean1 { get; set; }

        ////   private double _maxToGenerateForProbability;
        ////   private double _minToGenerateForProbability;

        ////// Key is x, value is p(x)
        ////   private Dictionary<int, double> probabilities = new Dictionary<int, double>();
        ////   public NormalDistribution(int seed, int min, int max)
        ////   {
        ////       RandomNumberGenerator = new Random(seed);
        ////      max = max + 1;
        ////       this.Min = min;
        ////       this.Max = max;
        ////       // Assume random normal distribution from [min..max]
        ////       // Calculate mean. For [4 .. 6] the mean is 5.
        ////       this.Mean1 = ((max - min) / 2) + min;
        ////       ///Mean1 = mean;


        ////       // Calculate standard deviation
        ////       int xMinusMyuSquaredSum = 0;
        ////       for (int i = min; i <= max; i++)
        ////       {
        ////           xMinusMyuSquaredSum += (int)Math.Pow(i - this.Mean1, 2);
        ////       }

        ////       this.StandardDeviation =( Math.Sqrt(xMinusMyuSquaredSum / (max - min + 1)));
        ////       // Flat, uniform distros tend to have a stdev that's too high; for example,
        ////       // for 1-10, stdev is 3, meaning the ranges are 68% in 2-8, and 95% in -1 to 11...
        ////       // So we cut this down to create better statistical variation. We now
        ////       // get numbers like: 1dev=68%, 2dev=95%, 3dev=99% (+= 1%). w00t!
        ////       this.StandardDeviation *= (0.5);

        ////       for (int i = min; i <= max; i++)
        ////       {
        ////           probabilities[i] = calculatePdf(i);
        ////           // Eg. if we have: 1 (20%), 2 (60%), 3 (20%), we want to see
        ////           // 1 (20), 2 (80), 3 (100)

        ////           // Avoid index out of range exception
        ////           if (i - 1 >= min)
        ////           {
        ////               probabilities[i] += probabilities[i - 1];
        ////           }
        ////       }


        ////       this._minToGenerateForProbability = this.probabilities.Values.Min();
        ////       this._maxToGenerateForProbability = this.probabilities.Values.Max();


        ////   }

        ////   public double calculatePdf(int x)
        ////   {
        ////       // Formula from Wikipedia: http://en.wikipedia.org/wiki/Normal_distribution
        ////       // f(x) = e ^ [-(x-myu)^2 / 2*sigma^2]
        ////       //        -------------------------
        ////       //         root(2 * pi * sigma^2)

        ////       double negativeXMinusMyuSquared = -(x - this.Mean1) * (x - this.Mean1);
        ////       double variance = StandardDeviation * StandardDeviation;
        ////       double twoSigmaSquared = 2 * variance;
        ////       double twoPiSigmaSquared = Math.PI * twoSigmaSquared;

        ////       double eExponent = negativeXMinusMyuSquared / twoSigmaSquared;
        ////       double top = Math.Pow(Math.E, eExponent);
        ////       double bottom = Math.Sqrt(twoPiSigmaSquared);

        ////       return top / bottom;
        ////   }

        ////   public int Next()
        ////   {
        ////       // map [0..1] to [minToGenerateForProbability .. maxToGenerateForProbability]
        ////       // If we have a negative (eg. [-50 to 100]), generate [0 to 150] and subtract 50 to get [-50 to 100]
        ////       double pickedProb = this.RandomNumberGenerator.NextDouble() * (this._maxToGenerateForProbability - this._minToGenerateForProbability);
        ////       pickedProb -= this._minToGenerateForProbability;

        ////       for (int i = this.Min; i <= this.Max; i++)
        ////       {
        ////           if (pickedProb <= this.probabilities[i])
        ////           {
        ////               return i;
        ////           }
        ////       }


        ////       throw new InvalidOperationException("Internal error: your algorithm is flawed, young Jedi.");
        ////   }
        ////public int GenerateInt()
        ////{
        ////    return Next();
        ////}

        ////public double GenerateDouble()
        ////{
        ////    return Next();
        ////}


        //https://jamesmccaffrey.wordpress.com/2010/10/22/generating-normally-distributed-data/
        public Random RandomNumberGenerator { get; }
        private Random RandomNumberGenerator2 { get; }
        private double _mean;
        private double _standardDeviation;


        public NormalDistribution(int seed,int seed2, double mean, double stdev)
        {
            RandomNumberGenerator = new Random(seed);
            RandomNumberGenerator2 = new Random(seed2);

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
                double v1 = 1 - RandomNumberGenerator.NextDouble();
                double v2 = 1 - RandomNumberGenerator2.NextDouble();

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
            double x1 = 1 - RandomNumberGenerator.NextDouble();
            double x2 = 1 - RandomNumberGenerator2.NextDouble();

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
