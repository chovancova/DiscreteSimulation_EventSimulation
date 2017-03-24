using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomGenerators;
using RandomGenerators.Generators;

namespace TestGenerators
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGenerators();

        }

        public static void TestGenerators()
        {
            //SIMULACNY CAS SU SEKUNDY
            GeneratorSeed seed = new GeneratorSeed();
            generator1(seed);

            generator2(seed);

            generator3(seed);

            generator4(seed);

            generator5(seed);

            generator6(seed);

            generator7(seed);

            generator8(seed);

            generator9(seed);

            generator10(seed);
        }

        private static void generator10(GeneratorSeed seed)
        {
            //VYGENEROVANY TYP OPRAVY
            Duration[] d3 =
            {
                new Duration(0, 0, 0.1), new Duration(0, 0, 0.6), new Duration(0, 0, 0.3)
            };
            IGenerators[] generators = new IGenerators[]
            {
                new DiscreteUniformDistribution(seed.GetRandomSeed(), 2, 20),
                new DiscreteEmpiricalDistribution(seed.GetRandomSeed(),
                    new[] {new Duration(10, 40, 0.1), new Duration(41, 61, 0.6), new Duration(62, 100, 0.3)}),
                new DiscreteUniformDistribution(seed.GetRandomSeed(), 120, 260)
            };

            DiscreteEmpiricalDistribution gen10 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), d3, generators);
            TestDistributionsToFileInt(gen10, "generator_10_empiricke.dst");
        }

        private static void generator9(GeneratorSeed seed)
        {
            //ZLOZITA OPRAVA
            //diskretne rovnomerne Tmin = 120, Tmax = 260
            DiscreteUniformDistribution gen9 = new DiscreteUniformDistribution(seed.GetRandomSeed(), 120, 260);
            TestDistributionsToFileInt(gen9, "generator_9_diskretne_uniform_min_120_max_260.dst");
        }

        private static void generator8(GeneratorSeed seed)
        {
            //STREDNE TAZKA OPRAVA
            //diskretne empiricke 
            // T:   10-40       41-61       62-100
            // p:   0.1         0.6         0.3
            Duration[] d2 =
            {
                new Duration(10, 40, 0.1), new Duration(41, 61, 0.6), new Duration(62, 100, 0.3)
            };
            DiscreteEmpiricalDistribution gen8 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), d2);
            TestDistributionsToFileInt(gen8, "generator_8_empiricke.dst");
        }

        private static void generator7(GeneratorSeed seed)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //pravdepodobnosti jednotlivych oprav a ich trvanie
            // JEDNODUCHA OPRAVA
            //diskretne rovnomerne = Tmin = 2, Tmax = 20
            DiscreteUniformDistribution gen7 = new DiscreteUniformDistribution(seed.GetRandomSeed(), 2, 20);
            TestDistributionsToFileInt(gen7, "generator_7_diskretne_uniform_min_2_max_20.dst");
        }

        private static void generator6(GeneratorSeed seed)
        {
            //prevzatie opraveneho auta trva s = 190 s +- 67s
            //diskretne uniform = <123, 257>
            DiscreteUniformDistribution gen6 = new DiscreteUniformDistribution(seed.GetRandomSeed() + seed.GetRandomSeed(), 123,
                257);
            TestDistributionsToFileInt(gen6, "generator_6_diskretne_uniform_min_123_max_257.dst");

            NormalDistribution gen6b = new NormalDistribution(seed.GetRandomSeed(), 123, 257);
            TestDistributionsToFileInt(gen6b, "generator_6b_normalne_123-257.dst");
        }

        private static void generator5(GeneratorSeed seed)
        {
            //preparkovanie auta z parkoviska do dielne alebo naspat ssa riadi 
            //trojuholnikovym rozdelenim s parametrami 
            //min = 120 s, max 540, a modus = 240 s
            TriangleUniformDistribution gen5 = new TriangleUniformDistribution(seed.GetRandomSeed(), 120, 540, 240);
            TestDistributionsToFileDouble(gen5, "generator_5_trojholnikove_min_120_max_540_modus_240.dst");
        }

        private static void generator4(GeneratorSeed seed)
        {
            //cas potrebny na prevzatie auta od zakaznika 
            //p = 120s +- 40 s
            // diskretne uniform = <80, 160> 
            DiscreteUniformDistribution gen4 = new DiscreteUniformDistribution(seed.GetRandomSeed(), 80, 160);
            TestDistributionsToFileInt(gen4, "generator_4_diskretne_uniform_min_80_max_160.dst");

            NormalDistribution gen4b = new NormalDistribution(seed.GetRandomSeed(), 80, 160);
            TestDistributionsToFileInt(gen4b, "generator_4b_normalne_80_-160.dst");
        }

        private static void generator3(GeneratorSeed seed)
        {
            //Cas potrebny na prevzatie objednavky od zakaznika
            //o = 190 s +- 120 s  
            //Diskretne uniform - <70, 310>
            DiscreteUniformDistribution gen3 = new DiscreteUniformDistribution(seed.GetRandomSeed(), 70, 310);
            TestDistributionsToFileInt(gen3, "generator_3_diskretne_uniform_min_70_max_310.dst");

            NormalDistribution gen3b = new NormalDistribution(seed.GetRandomSeed(), 70, 310);
            TestDistributionsToFileInt(gen3b, "generator_3b_normalne_70-310.dst");
        }

        private static void generator2(GeneratorSeed seed)
        {
            //Pravdepodobnosti poctu oprav, tkore bude zakaznik pozadovat
            //pocet oprav       1       2       3       4       5       6
            //pravdepodobnst    0.4     0.15    0.14    0.12    0.1     0.09
            //Empiricke rozdelenie
            Duration[] d = new[]
            {
                new Duration(1, 1, 0.4), new Duration(2, 2, 0.15), new Duration(3, 3, 0.14), new Duration(4, 4, 0.12),
                new Duration(5, 5, 0.1), new Duration(6, 6, 0.09)
            };
            DiscreteEmpiricalDistribution discPocet_oprav = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), d);
            TestDistributionsToFileInt(discPocet_oprav, "generator_2_empiricke.dst");
        }

        private static void generator1(GeneratorSeed seed)
        {
            //prud zakaznikov prichadzaujucich do autoservisu je poissonovsky prud s intenzitou 
            //z == 12 zakaznikov za hodinu
            //modelujem exp.rozdelenim => 300 s 
            ExponencionalDistribution ex300 = new ExponencionalDistribution(seed.GetRandomSeed(), 300);
            TestDistributionsToFileDouble(ex300, "generator_1_exp_300.dst");
        }


        static void TestDistributionsToFileDouble(IGenerators generator, string filename = "data.dst")
        {
            int numbers = 1000000;
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@filename))
            {
                for (int i = 0; i < numbers; i++)
                {
                    file.Write((generator.GenerateDouble().ToString().Replace(',', '.')) + "\t");
                }
            }
            Console.WriteLine(filename);
        }
        static void TestDistributionsToFileInt(IGenerators generator, string filename = "data.dst")
        {
            int numbers = 1000000;
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(filename))
            {
                for (int i = 0; i < numbers; i++)
                {
                    file.Write(generator.GenerateInt().ToString() + "\t");
                }
            }
            Console.WriteLine(filename);

        }

    }
}
