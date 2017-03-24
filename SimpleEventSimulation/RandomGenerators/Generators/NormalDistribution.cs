using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    //http://www.csharpcity.com/reusable-code/random-number-generators/
    public class NormalDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get; }


        private int Min { get; set; }
        private int Max { get; set; }
        public double StandardDeviation { get; set; }
        public double Mean1 { get; set; }

        private double _maxToGenerateForProbability;
        private double _minToGenerateForProbability;

     // Key is x, value is p(x)
        private Dictionary<int, double> probabilities = new Dictionary<int, double>();
        public NormalDistribution(int seed, int min, int max)
        {
            RandomNumberGenerator = new Random(seed);
           max = max + 1;
            this.Min = min;
            this.Max = max;
            // Assume random normal distribution from [min..max]
            // Calculate mean. For [4 .. 6] the mean is 5.
            this.Mean1 = ((max - min) / 2) + min;
            ///Mean1 = mean;


            // Calculate standard deviation
            int xMinusMyuSquaredSum = 0;
            for (int i = min; i <= max; i++)
            {
                xMinusMyuSquaredSum += (int)Math.Pow(i - this.Mean1, 2);
            }

            this.StandardDeviation =( Math.Sqrt(xMinusMyuSquaredSum / (max - min + 1)));
            // Flat, uniform distros tend to have a stdev that's too high; for example,
            // for 1-10, stdev is 3, meaning the ranges are 68% in 2-8, and 95% in -1 to 11...
            // So we cut this down to create better statistical variation. We now
            // get numbers like: 1dev=68%, 2dev=95%, 3dev=99% (+= 1%). w00t!
            this.StandardDeviation *= (0.5);

            for (int i = min; i <= max; i++)
            {
                probabilities[i] = calculatePdf(i);
                // Eg. if we have: 1 (20%), 2 (60%), 3 (20%), we want to see
                // 1 (20), 2 (80), 3 (100)

                // Avoid index out of range exception
                if (i - 1 >= min)
                {
                    probabilities[i] += probabilities[i - 1];
                }
            }
           

            this._minToGenerateForProbability = this.probabilities.Values.Min();
            this._maxToGenerateForProbability = this.probabilities.Values.Max();


        }

        public double calculatePdf(int x)
        {
            // Formula from Wikipedia: http://en.wikipedia.org/wiki/Normal_distribution
            // f(x) = e ^ [-(x-myu)^2 / 2*sigma^2]
            //        -------------------------
            //         root(2 * pi * sigma^2)

            double negativeXMinusMyuSquared = -(x - this.Mean1) * (x - this.Mean1);
            double variance = StandardDeviation * StandardDeviation;
            double twoSigmaSquared = 2 * variance;
            double twoPiSigmaSquared = Math.PI * twoSigmaSquared;

            double eExponent = negativeXMinusMyuSquared / twoSigmaSquared;
            double top = Math.Pow(Math.E, eExponent);
            double bottom = Math.Sqrt(twoPiSigmaSquared);

            return top / bottom;
        }

        public int Next()
        {
            // map [0..1] to [minToGenerateForProbability .. maxToGenerateForProbability]
            // If we have a negative (eg. [-50 to 100]), generate [0 to 150] and subtract 50 to get [-50 to 100]
            double pickedProb = this.RandomNumberGenerator.NextDouble() * (this._maxToGenerateForProbability - this._minToGenerateForProbability);
            pickedProb -= this._minToGenerateForProbability;

            for (int i = this.Min; i <= this.Max; i++)
            {
                if (pickedProb <= this.probabilities[i])
                {
                    return i;
                }
            }

            throw new InvalidOperationException("Internal error: your algorithm is flawed, young Jedi.");
        }

   

        public int GenerateInt()
        {
            return Next();
        }

        public double GenerateDouble()
        {
            return Next();
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }

        public double Mean()
        {
            return Mean1;
        }

        public double Spread()
        {
            throw new NotImplementedException();
        }

    }
}
