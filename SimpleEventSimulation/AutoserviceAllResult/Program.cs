using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary;

namespace AutoserviceAllResult
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadajte pocet replikacii");
            int pocet = Console.Read();
            if (pocet != 0)
            {
                RunAllResults(1000);

            }
            else
            {
                RunAllResults(pocet);

            }

            Console.ReadKey();
        }

    private static void RunAllResults(int replikacii = 35, int dlzka = 2592000, string filename = "data")
        {
           using (var file =
                new StreamWriter(filename + "_rep" +replikacii+"_"+".txt"))
            {
                file.Write((new ResultAutoservice()).ToStringHeader() + "\n");
                Console.WriteLine((new ResultAutoservice()).ToStringHeader());
                Console.WriteLine();

                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 16; j <= 26; j++)
                    {
                        var a = new AppCore(i, j, new AutoserviceGenerators(), null);
                        a.Refresh = false;
                        a.Simulate(replikacii, dlzka);
                        
                        Console.WriteLine(a.Results.ToString().Replace(',', '.'));
                        file.Write(a.Results.ToString().Replace(',', '.') + "\n");
                    }
                }
            }
            Console.WriteLine(filename);
        }
    }
}
