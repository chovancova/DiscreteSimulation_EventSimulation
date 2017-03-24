using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators
{
    public class GeneratorSeed
    {
        private Random seed { get; set; }
      //  private int generatorSeed { get; set; }
        private List<int> Seeds { get; set; }

        public GeneratorSeed()
        {
            Seeds = new List<int>();
            int s = (int)DateTime.Now.Ticks;
            seed = new Random(s);
        }
        public int GetRandomSeed()
        {
            int random = seed.Next();
            Seeds.Add(random);
            return random;
        }
    }
}
