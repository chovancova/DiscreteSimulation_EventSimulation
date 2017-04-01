using System;
using System.Collections.Generic;
using AutoserviceLibrary.Entities;
using RandomGenerators;
using RandomGenerators.Generators;
using SimulationLibrary;

namespace AutoserviceLibrary
{
    internal class AppCore : SimCore
    {
        private Queue<Zakaznik> _cakajuciZakaznik;
        private Queue<Zakaznik> _opraveneAuto;
        private Queue<Zakaznik> _pokazeneAuto;
        private readonly GeneratorSeed _seed = new GeneratorSeed();


        public AppCore(IGenerators[] generators, double maxTime, int pocetVolnychPracovnikov1,
            int pocetVolnychPracovnikov2) : base(generators, maxTime)
        {
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            Gen = new AutoserviceGenerators(_seed);
            _resetStatisticsRadCakajucichZakaznikov();
        }

        protected AppCore(double maxTime, AutoserviceGenerators gen, int pocetVolnychPracovnikov1,
            int pocetVolnychPracovnikov2) : base(maxTime)
        {
            Gen = gen;
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            _resetStatisticsRadCakajucichZakaznikov();
        }

        public AutoserviceGenerators Gen { get; private set; }

        public int PocetVolnychPracovnikov1 { get; set; }
        public int PocetVolnychPracovnikov2 { get; set; }
        public int PocetObsluhujucichPracovnikov1 { get; set; }
        public int PocetObsluhujucichPracovnikov2 { get; set; }


        public void InitializeQueues()
        {
            _cakajuciZakaznik = new Queue<Zakaznik>();
            _pokazeneAuto = new Queue<Zakaznik>();
            _opraveneAuto = new Queue<Zakaznik>();
        }

        public void Reset()
        {
            InitializeQueues();
            _resetStatisticsRadCakajucichZakaznikov();
        }

        private void _resetStatisticsRadCakajucichZakaznikov()
        {
            _poslednyPocetCakajucehoZakaznika = -1;
            _pocetZakaznikovVRadeCakajucichZakaznikov = 0;
            _celkovyCasCakaniaRadCakajucichZakaznikov = 0;
            _dlzkaRaduCakajucichZakaznikov = 0;
            _poslednyZmenenyCasRadCakajucichZakaznikov = 0;
        }

        public void ResetFrontCakajucichZakaznikov()
        {
            _cakajuciZakaznik.Clear();
        }

        public Zakaznik DalsiZakaznik()
        {
            if (_cakajuciZakaznik.Count == 0)
                return null;
            PridajStatistikuZmenuFrontuZakaznikov();
            return _cakajuciZakaznik.Dequeue();
        }

        public void PridajZakaznika(Zakaznik zakaznik)
        {
            PridajStatistikuZmenuFrontuZakaznikov();
            _cakajuciZakaznik.Enqueue(zakaznik);
        }

        public void PridajPokazeneAuto(Zakaznik zakaznik)
        {
            //pridaj statistiku
            _pokazeneAuto.Enqueue(zakaznik);
        }

        public Zakaznik DalsiePokazeneAuto()
        {
            if (_pokazeneAuto.Count == 0)
                return null;
            //pridaj statistiku 
            return _pokazeneAuto.Dequeue();
        }

        public void PridajOpraveneAuto(Zakaznik zakaznik)
        {
            //pridaj statistiku
            _opraveneAuto.Enqueue(zakaznik);
        }

        public Zakaznik DalsieOpraveneAuto()
        {
            if (_opraveneAuto.Count == 0)
                return null;
            //pridaj statistiku 
            return _opraveneAuto.Dequeue();
        }

        #region Statistiky

        private int _poslednyPocetCakajucehoZakaznika;
        private int _pocetZakaznikovVRadeCakajucichZakaznikov;
        private double _celkovyCasCakaniaRadCakajucichZakaznikov;
        private double _dlzkaRaduCakajucichZakaznikov;
        private double _poslednyZmenenyCasRadCakajucichZakaznikov;
        private int _iteration;

        public LinkedList<double> PriemernyCasStravenyVRadeCakajucichZakaznikov { get; set; }
        public LinkedList<double> PriemernyPocetCakajucichVRadeCakajucichZakaznikov { get; set; }

        private void PridajStatistikuZmenuFrontuZakaznikov()
        {
            if (_poslednyPocetCakajucehoZakaznika >= 0)
                _dlzkaRaduCakajucichZakaznikov += _cakajuciZakaznik.Count*(CurrentTime - _poslednyZmenenyCasRadCakajucichZakaznikov);
            _poslednyPocetCakajucehoZakaznika = _cakajuciZakaznik.Count;
            _poslednyZmenenyCasRadCakajucichZakaznikov = CurrentTime;
        }

        public void PridajStatistikuCakaniaFrontZakaznikov(double waitingtime)
        {
            _pocetZakaznikovVRadeCakajucichZakaznikov++;
            _celkovyCasCakaniaRadCakajucichZakaznikov += waitingtime;
            _iteration++;
            if (_iteration == 10000)
            {
                _iteration = 0;
                PriemernyCasStravenyVRadeCakajucichZakaznikov.AddLast(_celkovyCasCakaniaRadCakajucichZakaznikov/_pocetZakaznikovVRadeCakajucichZakaznikov);
                PriemernyPocetCakajucichVRadeCakajucichZakaznikov.AddLast(_dlzkaRaduCakajucichZakaznikov/CurrentTime);
            }
        }












        #endregion
    }
}