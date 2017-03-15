using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RandomGenerators.Generators
{
   public interface IGenerators
   {
       Random RandomNumberGenerator { get; }
       int GenerateInt();
       double GenerateDouble();
       double DensityDistribution();
       double Mean();
       double Spread();
   }
}
