using System;
using System.Collections.Generic;
using AutoserviceLibrary.Entities;
using AutoserviceLibrary.Events;
using RandomGenerators;
using RandomGenerators.Generators;
using SimulationLibrary;

namespace AutoserviceLibrary
{
    public class AppCore : SimCore
    {
        private Queue<Zakaznik> _frontCakajuciZakaznik;
        private Queue<Zakaznik> _frontOpraveneAuto;
        private Queue<Zakaznik> _frontPokazeneAuto;
        private readonly GeneratorSeed _seed = new GeneratorSeed();

        public AutoserviceGenerators Gen { get; private set; }

        public int PocetVolnychPracovnikov1 { get; private set; }
        public int PocetVolnychPracovnikov2 { get; private set; }
        public int PocetPracovnikov1 { get; set; }
        public int PocetPracovnikov2 { get; set; }
    
        public AppCore(int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2, AutoserviceGenerators gen, ISimulationGui gui) :base(gui)
        {
            Gen = gen;
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            PocetPracovnikov1 = PocetVolnychPracovnikov1;
            PocetPracovnikov2 = PocetVolnychPracovnikov2;
            _resetStatisticsRadCakajucichZakaznikov();
            InitializeQueues();
        }

     public AppCore( int pocetVolnychPracovnikov1,
            int pocetVolnychPracovnikov2) 
        {
            Gen = new AutoserviceGenerators(_seed);
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            _resetStatisticsRadCakajucichZakaznikov();
        }

        public void UltraSimulation()
        {

        }

        public void AnalyticSimulation()
        {

        }

        public void NormalSimulation()
        {

        }



        public void InitializeQueues()
        {
            _frontCakajuciZakaznik = new Queue<Zakaznik>();
            _frontPokazeneAuto = new Queue<Zakaznik>();
            _frontOpraveneAuto = new Queue<Zakaznik>();
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
            _frontCakajuciZakaznik.Clear();
           PocetVolnychPracovnikov1 = PocetPracovnikov1;
           // PocetVolnychPracovnikov2 = PocetPracovnikov2;
        }

        public Zakaznik DalsiZakaznik()
        {
            if (_frontCakajuciZakaznik.Count == 0)
                return null;
            PridajStatistikuZmenuFrontuZakaznikov();
            return _frontCakajuciZakaznik.Dequeue();
        }

        

        public void PridajZakaznika(Zakaznik zakaznik)
        {
            PridajStatistikuZmenuFrontuZakaznikov();
            _frontCakajuciZakaznik.Enqueue(zakaznik);
        }

        public void PridajPokazeneAuto(Zakaznik zakaznik)
        {
            //pridaj statistiku
            _frontPokazeneAuto.Enqueue(zakaznik);
        }

        
    
        public Zakaznik DalsiePokazeneAuto()
        {
            if (_frontPokazeneAuto.Count == 0)
                return null;
            //pridaj statistiku 
            return _frontPokazeneAuto.Dequeue();
        }

        public void PridajOpraveneAuto(Zakaznik zakaznik)
        {
            //pridaj statistiku
            _frontOpraveneAuto.Enqueue(zakaznik);
        }

        public Zakaznik DalsieOpraveneAuto()
        {
            if (_frontOpraveneAuto.Count == 0)
                return null;
            //pridaj statistiku 
            return _frontOpraveneAuto.Dequeue();
        }

        public bool JeVolnyPracovnik1()
        {
            return PocetVolnychPracovnikov1 > 0;
        }

        public bool JeVolnyPracovnik2()
        {
            return PocetVolnychPracovnikov2 > 0;
        }

        public bool  ObsadPracovnikaSkupiny1()
        {
            if (PocetVolnychPracovnikov1 == 0)
            {
                return false;
            }
            PocetVolnychPracovnikov1--;
            return true;
            //todo statistiky
        }

        public bool ObsadPracovnikaSkupiny2()
        {
            if (PocetVolnychPracovnikov2 == 0)
            {
                return false;
            }
            PocetVolnychPracovnikov2--;
            return false;
            //todo statistiky
        }

        public void UvolniPracovnikaSkupiny1()
        {
            PocetVolnychPracovnikov1++;
        }

        public void UvolniPracovnikaSkupiny2()
        {
            PocetVolnychPracovnikov2++;
        }

        public bool JeFrontZakaznikovPrazdny()
        {
            return _frontCakajuciZakaznik.Count == 0;
        }

        public bool JeFrontOpravenychAutPrazdny()
        {
            return _frontOpraveneAuto.Count == 0;
        }

        public bool JeFrontPokazenychAutPrazdny()
        {
            return _frontPokazeneAuto.Count == 0;
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
                _dlzkaRaduCakajucichZakaznikov += _frontCakajuciZakaznik.Count*
                                                  (CurrentTime - _poslednyZmenenyCasRadCakajucichZakaznikov);
            _poslednyPocetCakajucehoZakaznika = _frontCakajuciZakaznik.Count;
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
                PriemernyCasStravenyVRadeCakajucichZakaznikov.AddLast(_celkovyCasCakaniaRadCakajucichZakaznikov/
                                                                      _pocetZakaznikovVRadeCakajucichZakaznikov);
                PriemernyPocetCakajucichVRadeCakajucichZakaznikov.AddLast(_dlzkaRaduCakajucichZakaznikov/CurrentTime);
            }
        }

        #endregion

        public void PridajStatistikuCakanieNaVybavenieObjednavky(double casCakania)
        {
           // throw new System.NotImplementedException();
        }

        public void PridajStatistikuCakanieNaOpravu(double statistika1)
        {
           // throw new System.NotImplementedException();
        }


        public override void BeforeReplication()
        {
           // throw new NotImplementedException();
        }

        public override void AfterReplication()
        {
           // throw new NotImplementedException();
        }

        public override void SimulationEnd()
        {
           // throw new NotImplementedException();
        }

        public override void ScheduleFirstEvent()
        {
            //3.
            KoniecDnaEvent a = new KoniecDnaEvent(0, this, new Zakaznik());
            this.ScheduleEvent(a, 0);
        }

        public override void AfterStopReplications()
        {
         //   throw new NotImplementedException();
        }


        public int PocetCakajucichZakaznikov()
        {
            return _frontCakajuciZakaznik.Count;
        }
        public int PocetPokazenychAut()
        {
            return _frontPokazeneAuto.Count;
        }

        public int PocetOpravenychAut()
        {
            return _frontOpraveneAuto.Count;
        }


    }
}