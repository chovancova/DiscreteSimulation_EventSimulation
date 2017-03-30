using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;
using RandomGenerators;
using RandomGenerators.Generators;
using SimulationLibrary;

namespace AutoserviceLibrary
{
    class AppCore : SimCore
    {
        private Queue<Zakaznik> _pokazeneAuto;
        private Queue<Zakaznik> _opraveneAuto;

        private Queue<Zakaznik> _cakajuciZakaznik;

        public AutoserviceGenerators Gen { get; private set; }
        private GeneratorSeed _seed = new GeneratorSeed();

        public int PocetVolnychPracovnikov1 { get; set; }
        public int PocetVolnychPracovnikov2 { get; set; }
        public int PocetObsluhujucichPracovnikov1 { get; set; }
        public int PocetObsluhujucichPracovnikov2 { get; set; }

        public StatisticsService Statistika { get; set;  }

      
        public AppCore(IGenerators[] generators, double maxTime, int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2) : base(generators, maxTime)
        {
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            Gen = new AutoserviceGenerators(_seed);
            Statistika = new StatisticsService();
        }

        protected AppCore(double maxTime, AutoserviceGenerators gen, int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2) : base(maxTime)
        {
            Gen = gen;
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
        }

        public void InitializeQueues()
        {
            _cakajuciZakaznik = new Queue<Zakaznik>();
            _pokazeneAuto = new Queue<Zakaznik>();
            _opraveneAuto = new Queue<Zakaznik>();
        }

        public void Reset()
        {
            InitializeQueues();
        }


        public void ResetFrontCakajucichZakaznikov()
        {
            _cakajuciZakaznik.Clear();
        }

        public Zakaznik DalsiZakaznik()
        {
            if (_cakajuciZakaznik.Count == 0)
            {
                return null; 
            }
            PridajStatistikuZmenuFrontuZakaznikov();
            return _cakajuciZakaznik.Dequeue();
        }

        public void PridajZakaznika(Zakaznik zakaznik)
        {
            PridajStatistikuZmenuFrontuZakaznikov();
            _cakajuciZakaznik.Enqueue(zakaznik);   
        }





        #region Statistiky

        private int _lastCount = 0;
        private int _numberOfCustomers = 0;
        private double _totalWaitingTime = 0;
        private double _lengthOfFront = 0;
        private double _lastChangedTime = 0;
        private int _iteration;
        private void PridajStatistikuZmenuFrontuZakaznikov()
        {
            if (_lastCount >= 0)
            {
                _lengthOfFront += _cakajuciZakaznik.Count * ( CurrentTime - _lastChangedTime);
            }
            _lastCount = _cakajuciZakaznik.Count;
            _lastChangedTime = CurrentTime;
        }

        public void PridajStatistikuCakaniaFrontZakaznikov(double waitingtime)
        {
            _numberOfCustomers++;
            _totalWaitingTime += waitingtime;
            _iteration++;
            if (_iteration == 10000)
            {
                _iteration = 0;
                Console.WriteLine("Average Time spent in row:\t" + _totalWaitingTime / _numberOfCustomers);
                Console.WriteLine("Average number of customers:\t" + _lengthOfFront / CurrentTime);
                Console.WriteLine();
            }
        }


        #endregion




    }
}
