using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
    public struct Duration 
    {
        public int Max;
        public int Min;
        public double P;
       
        public Duration(int min, int max, double p)
        {
            Max = max;
            Min = min; 
            P = p; 
        }
    
    }

    public class DiscreteEmpiricalDistribution : IGenerators
    {
        public Random RandomNumberGenerator { get;  set; }
        //hodnota, pravdepodobnost 
        public Duration[] Values { get; set; }
        public IGenerators[] Generators { get; private set; }
        private bool _onlyOneValue;
        public DiscreteEmpiricalDistribution(int seed, Duration[] duration, bool onlyOneValue = false)
        {
            RandomNumberGenerator = new Random(seed);
            Values = duration;
            _onlyOneValue = onlyOneValue;
            if (!_onlyOneValue)
            {
                Generators = new DiscreteUniformDistribution[duration.Length];
                for (int i = 0; i < Generators.Length; i++)
                {
                    Generators[i] = new DiscreteUniformDistribution(RandomNumberGenerator.Next(), Values[i].Min,
                        Values[i].Max);
                }
            }
            else
            {
                Generators = null; 
            }
        }

        public DiscreteEmpiricalDistribution(int seed, Duration[] duration, IGenerators[] generators)
        {
            _onlyOneValue = false;
            RandomNumberGenerator = new Random(seed);
            Values = duration;
            Generators = generators;
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
                    if (_onlyOneValue)
                    {
                        value = Values[i].Min;
                    }
                    else
                    {
                          //< >
                          value = Generators[i].GenerateInt();
                         break;
                    }
                }
            }
            return value;
        }

        public double GenerateDouble()
        {
            //double value = 0;
            //double p = RandomNumberGenerator.NextDouble(); //pravdepodobnost v rozsahu od 0 - 1  
            //double tempPr = 0;
            ////napr. mam pravdepodobnosti 0.2,  0.5.,  0.3
            ////pravdepodobnostne rozsahy
            ////value  	rozsah pravd. 		probalibility		
            ////1.	  	0   - 0.2     			0.2
            ////2.	 	0.2 - 0.7 				0.5
            ////3. 	  	0.7 -  1				0.3
            ////ak je p napr. 0.4 tak to znamena, ze patri value 2 
            //// ak je 0.
            //for (int i = 0; i < Values.Length; i++)
            //{
            //    tempPr += Values[i].P;
            //    if (p < tempPr)
            //    {
            //        //< >
            //        value = Generators[i].GenerateDouble();
            //        break;
            //    }
            //}
            //return value;
            throw new NotImplementedException();

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
