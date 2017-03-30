using RandomGenerators;
using RandomGenerators.Generators;

namespace AutoserviceLibrary
{
    public class AutoserviceGenerators
    {
        private readonly ExponencionalDistribution _gen1;
        private readonly DiscreteEmpiricalDistribution _gen2;
        private readonly UniformContinuousDistribution _gen3;
        private readonly UniformContinuousDistribution _gen4;
        private readonly TriangleUniformDistribution _gen5;
        private readonly UniformContinuousDistribution _gen6;
        private readonly DiscreteEmpiricalDistribution _gen7;

        public AutoserviceGenerators()
        {
            var seed = new GeneratorSeed();

            _gen1 = new ExponencionalDistribution(seed.GetRandomSeed(), 300);
            _gen2 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), new[]
            {
                new Duration(1, 1, 0.4), new Duration(2, 2, 0.15), new Duration(3, 3, 0.14), new Duration(4, 4, 0.12),
                new Duration(5, 5, 0.1), new Duration(6, 6, 0.09)
            });

            _gen3 = new UniformContinuousDistribution(seed.GetRandomSeed(), 70, 310);
            _gen4 = new UniformContinuousDistribution(seed.GetRandomSeed(), 80, 160);
            _gen5 = new TriangleUniformDistribution(seed.GetRandomSeed(), 120, 540, 240);
            _gen6 = new UniformContinuousDistribution(seed.GetRandomSeed() + seed.GetRandomSeed(), 123, 257);
            _gen7 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), new[]
            {
                new Duration(0, 0, 0.7), new Duration(0, 0, 0.2), new Duration(0, 0, 0.1)
            }, new IGenerators[]
            {
                new UniformDiscreteDistribution(seed.GetRandomSeed(), 2, 20),
                new DiscreteEmpiricalDistribution(seed.GetRandomSeed(),
                    new[] {new Duration(10, 40, 0.1), new Duration(41, 61, 0.6), new Duration(62, 100, 0.3)}),
                new UniformDiscreteDistribution(seed.GetRandomSeed(), 120, 260)
            });
        }
        public AutoserviceGenerators(GeneratorSeed seed)
        {
            _gen1 = new ExponencionalDistribution(seed.GetRandomSeed(), 300);
            _gen2 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), new[]
            {
                new Duration(1, 1, 0.4), new Duration(2, 2, 0.15), new Duration(3, 3, 0.14), new Duration(4, 4, 0.12),
                new Duration(5, 5, 0.1), new Duration(6, 6, 0.09)
            });

            _gen3 = new UniformContinuousDistribution(seed.GetRandomSeed(), 70, 310);
            _gen4 = new UniformContinuousDistribution(seed.GetRandomSeed(), 80, 160);
            _gen5 = new TriangleUniformDistribution(seed.GetRandomSeed(), 120, 540, 240);
            _gen6 = new UniformContinuousDistribution(seed.GetRandomSeed() + seed.GetRandomSeed(), 123, 257);
            _gen7 = new DiscreteEmpiricalDistribution(seed.GetRandomSeed(), new[]
            {
                new Duration(0, 0, 0.7), new Duration(0, 0, 0.2), new Duration(0, 0, 0.1)
            }, new IGenerators[]
            {
                new UniformDiscreteDistribution(seed.GetRandomSeed(), 2, 20),
                new DiscreteEmpiricalDistribution(seed.GetRandomSeed(),
                    new[] {new Duration(10, 40, 0.1), new Duration(41, 61, 0.6), new Duration(62, 100, 0.3)}),
                new UniformDiscreteDistribution(seed.GetRandomSeed(), 120, 260)
            });
        }

        /// <summary>
        ///     prud zakaznikov prichadzaujucich do autoservisu je poissonovsky prud s intenzitou
        ///     z == 12 zakaznikov za hodinu
        ///     modelujem exp.rozdelenim  300 s
        /// </summary>
        /// <returns></returns>
        public double Generator1()
        {
            return _gen1.GenerateDouble();
        }

        /// <summary>
        ///     Pravdepodobnosti poctu oprav, tkore bude zakaznik pozadovat
        ///     pocet oprav       1       2       3       4       5       6
        ///     pravdepodobnst    0.4     0.15    0.14    0.12    0.1     0.09
        ///     Empiricke rozdelenie
        /// </summary>
        /// <returns></returns>
        public int Generator2()
        {
            return _gen2.GenerateInt();
        }

        /// <summary>
        ///     Cas potrebny na prevzatie objednavky od zakaznika
        ///     o = 190 s +- 120 s
        ///     Spojite rovnomerne - 70, 310
        /// </summary>
        /// <returns></returns>
        public double Generator3()
        {
            return _gen3.GenerateDouble();
        }

        /// <summary>
        ///     cas potrebny na prevzatie auta od zakaznika
        ///     p = 120s +- 40 s
        ///     diskretne uniform = 80, 160
        /// </summary>
        /// <param name="seed"></param>
        public double Generator4()
        {
            return _gen4.GenerateDouble();
        }

        /// <summary>
        ///     Preparkovanie auta z parkoviska do dielne alebo naspat ssa riadi
        ///     trojuholnikovym rozdelenim s parametrami
        ///     min = 120 s, max 540, a modus = 240 s
        /// </summary>
        /// <param name="seed"></param>
        public double Generator5()
        {
            return _gen5.GenerateDouble();
        }

        /// <summary>
        ///     Prevzatie opraveneho auta trva s = 190 s +- 67s
        ///     diskretne uniform = <123, 257>
        /// </summary>
        /// <param name="seed"></param>
        public double Generator6()
        {
            return _gen6.GenerateDouble();
        }

        /// <summary>
        ///     /////////////////////////////////////////////////
        ///     pravdepodobnosti jednotlivych oprav a ich trvanie
        ///     JEDNODUCHA OPRAVA - 0.7
        ///     diskretne rovnomerne = Tmin = 2, Tmax = 20
        ///     STREDNE TAZKA OPRAVA - 0.2
        ///     diskretne empiricke
        ///     T:   10-40       41-61       62-100
        ///     p:   0.1         0.6         0.3
        ///     ZLOZITA OPRAVA
        ///     diskretne rovnomerne Tmin = 120, Tmax = 260
        /// </summary>
        /// <returns></returns>
        public int Generator7()
        {
            return _gen7.GenerateInt();
        }
    }
}