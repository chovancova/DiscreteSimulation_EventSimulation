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
        private readonly Random _randomNumberGenerator;
        //hodnota, pravdepodobnost 
        private readonly Duration[] _values;
        private readonly IGenerators[] _generators;
        private readonly bool _onlyOneValue;
        public DiscreteEmpiricalDistribution(int seed, Duration[] duration, bool onlyOneValue = false)
        {
            _randomNumberGenerator = new Random(seed);
            _values = duration;
            _onlyOneValue = onlyOneValue;
            if (!_onlyOneValue)
            {
                _generators = new UniformDiscreteDistribution[duration.Length];
                for (int i = 0; i < _generators.Length; i++)
                {
                    _generators[i] = new UniformDiscreteDistribution(_randomNumberGenerator.Next(), _values[i].Min,
                        _values[i].Max);
                }
            }
            else
            {
                _generators = null; 
            }
        }

        public DiscreteEmpiricalDistribution(int seed, Duration[] duration, IGenerators[] generators)
        {
            _onlyOneValue = false;
            _randomNumberGenerator = new Random(seed);
            _values = duration;
            _generators = generators;
        }


        public int GenerateInt()
        {
            int value = 0;
            double p = _randomNumberGenerator.NextDouble(); //pravdepodobnost v rozsahu od 0 - 1  
            double tempPr = 0;
            //napr. mam pravdepodobnosti 0.2,  0.5.,  0.3
            //pravdepodobnostne rozsahy
            //value  	rozsah pravd. 		probalibility		
            //1.	  	0   - 0.2     			0.2
            //2.	 	0.2 - 0.7 				0.5
            //3. 	  	0.7 -  1				0.3
            //ak je p napr. 0.4 tak to znamena, ze patri value 2 
            // ak je 0.
            for (int i = 0; i < _values.Length; i++)
            {
                tempPr += _values[i].P;
                if (p < tempPr)
                {
                    if (_onlyOneValue)
                    {
                        value = _values[i].Min;
                    }
                    else
                    {
                          //< >
                          value = _generators[i].GenerateInt();
                         break;
                    }
                }
            }
            return value;
        }

        public double GenerateDouble()
        {
             throw new NotImplementedException();
        }

       
    }
}
