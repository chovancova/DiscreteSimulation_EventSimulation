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
            //generatorSeed = Guid.NewGuid().GetHashCode();
            seed = new Random();
        }
        public int GetRandomSeed()
        {
            int random = seed.Next();
            Seeds.Add(random);
            return random;
        }
    }
}
