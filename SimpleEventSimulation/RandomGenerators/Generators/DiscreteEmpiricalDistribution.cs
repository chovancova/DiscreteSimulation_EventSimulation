using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    public struct Duration 
    {
        public int MIN, MAX;
        public double P;

        public Duration(int min, int max, double p)
        {
            this.MAX = max;
            this.MIN = min;
            this.P = p;
        }
    }

    public class DiscreteEmpiricalDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get;  set; }
        //hodnota, pravdepodobnost 
        public Duration[] Values { get; set; }
        public DiscreteUniformDistribution[] Generators { get; private set; }

        public DiscreteEmpiricalDistribution(int seed, Duration[] duration)
        {
            RandomNumberGenerator = new Random(seed);
            Values = duration;
            Generators = new DiscreteUniformDistribution[duration.Length];
            for (int i = 0; i < Generators.Length; i++)
            {
                Generators[i] = new DiscreteUniformDistribution(RandomNumberGenerator.Next(), Values[i].MIN, Values[i].MAX);
            }
        }

        public int GenerateInt()
        {
            int value = 0;
            double p = RandomNumberGenerator.NextDouble(); //pravdepodobnost v rozsahu od 0 - 1  
            double tempPr = 0;
            //napr. mam pravdepodobnosti 0.2,  0.5.,  0.3
            //pravdepodobnostne rozsahy
            //value  	rozsah pravd. 		probalibility		
            //1.	  	0   - 0.2     			0.2
            //2.	 	0.2 - 0.7 				0.5
            //3. 	  	0.7 -  1				0.3
            //ak je p napr. 0.4 tak to znamena, ze patri value 2 
            // ak je 0.
            for (int i = 0; i < Values.Length; i++)
            {
                tempPr += Values[i].P;
                if (p < tempPr)
                {
                    //< >
                    value = Generators[i].GenerateInt();
                    break;
                }
            }
            return value;
        }

        public double GenerateDouble()
        {
            return (double) GenerateInt();
        }

        public double DensityDistribution()
        {
            throw new NotImplementedException();
        }

        public double Mean()
        {
            throw new NotImplementedException();
        }

        public double Spread()
        {
            throw new NotImplementedException();
        }
    }
}
