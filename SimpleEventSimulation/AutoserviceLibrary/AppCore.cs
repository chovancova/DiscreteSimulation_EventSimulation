using System;
using System.Collections.Generic;
using AutoserviceLibrary.Entities;
using AutoserviceLibrary.Events.Zaciatok;
using SimulationLibrary;

namespace AutoserviceLibrary
{
    public class AppCore : SimCore, IDisposable
    {
        public const int DlzkaDnaSekundy = 28800;
        public ResultAutoservice Results { get; set; }

        public AppCore(int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2, AutoserviceGenerators gen,
            ISimulationGui gui) : base(gui)
        {
            Gen = gen;
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            PocetPracovnikov1 = PocetVolnychPracovnikov1;
            PocetPracovnikov2 = PocetVolnychPracovnikov2;
            InitializeQueues();           
        }

        public AutoserviceGenerators Gen { get; }
      

        public void NastavKonfiguraciu(int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2)
        {
            PocetVolnychPracovnikov1 = pocetVolnychPracovnikov1;
            PocetVolnychPracovnikov2 = pocetVolnychPracovnikov2;
            PocetPracovnikov1 = PocetVolnychPracovnikov1;
            PocetPracovnikov2 = PocetVolnychPracovnikov2;
        }

        public void UltraModeSimulation(int pocetReplikacii, int dlzkaReplikacie, int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2)
        {
            Refresh = false;
            NastavKonfiguraciu(pocetVolnychPracovnikov1, pocetVolnychPracovnikov2);
            Simulate(pocetReplikacii, dlzkaReplikacie);
        }

        public void NormalModeSimulation(int pocetReplikacii, int dlzkaReplikacie, int pocetVolnychPracovnikov1, int pocetVolnychPracovnikov2)
        {
            Refresh = true;

            NastavKonfiguraciu(pocetVolnychPracovnikov1, pocetVolnychPracovnikov2);
            Simulate(pocetReplikacii, dlzkaReplikacie);
        }

        
        public int PocetLudiOdisli{get;set;}

        

        #region OVERRIDE METHODS

        protected override void BeforeSimulation()
        {
            InitializeQueues();
            ResetGlobalStatistics();
            IS_Reset();
            IS_CakanieZadanieObjednavky_Reset();
            S1_Reset();
            S2_Reset();
            S3_Reset();
            S4_Reset();
            S11_Reset();
            ResetPracovnikov1();
            ResetPracovnikov2();
            PocetLudiOdisli = 0; 
        }

        public override void BeforeReplication()
        {
            ClearQueues();
            S1_Reset();
            S2_Reset();
            S3_Reset();
            S4_Reset();
            S11_Reset();
            ResetPracovnikov1();
            ResetPracovnikov2();
            PocetLudiOdisli = 0; 
        }

        public override void AfterReplication()
        {
            SG_AddValue();
            IS_AddValue(S4_PriemernyCasOpravy());
            IS_AddValue2(S1_PriemernyCasCakania());

            if(ActualReplication%100==0)
               Gui?.RefreshGui();
        }

        public override void SimulationEnd()
        {
            Results=new ResultAutoservice(PocetPracovnikov1, PocetPracovnikov2,SG1_PriemernyCasCakania(), SG2_PrimernyPocet(), SG3_PriemernyCasVServise(), SG4_PriemernyCasOpravy(), SG11_PrimernyPocetNaKonciDna(), IS_NaZadanieObjednavky()[0],IS_NaZadanieObjednavky()[1], IS_NaOpravu()[0],IS_NaOpravu()[1]);

            Gui?.RefreshGui();
            //ResultSkupina1.Add(PocetPracovnikov1, SG2_PrimernyPocet());
            //ResultSkupina2.Add(PocetPracovnikov2, SG3_PriemernyCasVServise());
        }

        public override void ScheduleFirstEvent()
        {
            ScheduleEvent(new ZaciatokReplikacieEvent(0, this, null));
        }

        #endregion

        #region PRACOVNICI 1 SKUPINA

        public int PocetPracovnikov1 { get; set; }
        public int PocetVolnychPracovnikov1 { get; private set; }

        public bool JeVolnyPracovnik1()
        {
            return PocetVolnychPracovnikov1 > 0;
        }

        public void UvolniPracovnikaSkupiny1()
        {
            if (PocetPracovnikov1 < PocetVolnychPracovnikov1)
                throw new Exception("Exception - pocet pracovnikov je vvacsi");
            PocetVolnychPracovnikov1++;
        }

        public bool ObsadPracovnikaSkupiny1()
        {
            if (PocetPracovnikov1 < PocetVolnychPracovnikov1)
                throw new Exception("Exception - pocet pracovnikov je vvacsi");

            if (PocetVolnychPracovnikov1 == 0)
                return false;
            PocetVolnychPracovnikov1--;
            return true;
        }

        private void ResetPracovnikov1()
        {
            PocetVolnychPracovnikov1 = PocetPracovnikov1;
        }

        #endregion

        #region PRACOVNICI 2 SKUPINA

        public int PocetVolnychPracovnikov2 { get; private set; }
        public int PocetPracovnikov2 { get; set; }

        public bool JeVolnyPracovnik2()
        {
            return PocetVolnychPracovnikov2 > 0;
        }

        public bool ObsadPracovnikaSkupiny2()
        {
            if (  (PocetVolnychPracovnikov2-1)>PocetPracovnikov2)
                throw new Exception("Exception - pocet pracovnikov je vvacsi");

            if (PocetVolnychPracovnikov2 == 0)
                return false;
            PocetVolnychPracovnikov2--;
            return false;
            //todo statistiky
        }

        public void UvolniPracovnikaSkupiny2()
        {
            if (PocetPracovnikov2 <= PocetVolnychPracovnikov2 )
                throw new Exception("Exception - pocet pracovnikov je vvacsi");
            PocetVolnychPracovnikov2++;
        }

        private void ResetPracovnikov2()
        {
            PocetVolnychPracovnikov2 = PocetPracovnikov2;
        }

        #endregion

        #region QUEUES

        public void InitializeQueues()
        {
            _frontCakajuciZakaznik = new Queue<Zakaznik>();
            _frontPokazeneAuto = new Queue<Zakaznik>();
            _frontOpraveneAuto = new Queue<Zakaznik>();
        }

        public void ClearQueues()
        {
            _frontCakajuciZakaznik.Clear();
            _frontPokazeneAuto.Clear();
            _frontOpraveneAuto.Clear();
            CelkovyPocetOpravenychAut = 0;
            CelkovyPocetPokazenychAut = 0;
            CelkovyPocetZakaznikov = 0; 
            Front_CakajuciZakaznici_NaKonciDna = 0;
            Front_CakajuciZakaznici_CelkovyNaKonciDna = 0;
        }

        #endregion

        #region FRONT CAKAJUCICH ZAKAZNIKOV

        private Queue<Zakaznik> _frontCakajuciZakaznik;

        public bool JeFrontZakaznikovPrazdny()
        {
            return _frontCakajuciZakaznik.Count == 0;
        }

        public int PocetCakajucichZakaznikov()
        {
            return _frontCakajuciZakaznik.Count;
        }

        public Zakaznik Front_CakajuciZakaznici_VyberZakaznika()
        {
            if (_frontCakajuciZakaznik.Count==0) return null;
            return _frontCakajuciZakaznik.Dequeue();
        }

        public void Front_CakajuciZakaznici_PridajZakaznika(Zakaznik zakaznik)
        {
            _frontCakajuciZakaznik.Enqueue(zakaznik);
            CelkovyPocetZakaznikov++;
        }

        public int Front_CakajuciZakaznici_NaKonciDna { get; private set; }
        public int Front_CakajuciZakaznici_CelkovyNaKonciDna { get; private set; }

        public void Front_CakajuciZakaznici_Reset()
        {
            Front_CakajuciZakaznici_NaKonciDna = PocetCakajucichZakaznikov();
            Front_CakajuciZakaznici_CelkovyNaKonciDna += PocetCakajucichZakaznikov();
            S11_AddValue();
            _frontCakajuciZakaznik.Clear();
        }
        public int CelkovyPocetZakaznikov { get; private set; }


        #endregion

        #region FRONT POKAZENYCH AUT

        private Queue<Zakaznik> _frontPokazeneAuto;

        public int PocetPokazenychAut()
        {
            return _frontPokazeneAuto.Count;
        }

        public bool JeFrontPokazenychAutPrazdny()
        {
            return _frontPokazeneAuto.Count == 0;
        }

        public void Front_PokazeneAuta_Pridaj(Zakaznik zakaznik)
        {
            _frontPokazeneAuto.Enqueue(zakaznik);
            CelkovyPocetPokazenychAut++;
        }

        public Zakaznik Front_PokazeneAuta_Vyber()
        {
            if (_frontPokazeneAuto.Count == 0) return null;
            return _frontPokazeneAuto.Dequeue();
        }
        public int CelkovyPocetPokazenychAut { get; private set; }


        #endregion

        #region FRONT OPRAVENYCH AUT

        private Queue<Zakaznik> _frontOpraveneAuto;

        public bool JeFrontOpravenychAutPrazdny()
        {
            return _frontOpraveneAuto.Count == 0;
        }

        public int PocetOpravenychAut()
        {
            return _frontOpraveneAuto.Count;
        }

        public void Front_OpraveneAuta_Pridaj(Zakaznik zakaznik)
        {
            _frontOpraveneAuto.Enqueue(zakaznik);
            //CelkovyPocetOpravenychAut++;
        }

        public Zakaznik Front_OpraveneAuta_Vyber()
        {
            if (_frontOpraveneAuto.Count == 0)
                return null;

            return _frontOpraveneAuto.Dequeue();
        }

        public int CelkovyPocetOpravenychAut { get; set;}

        #endregion

        #region S1 – Priemerný čas čakania zákazníka

        //S1 – Priemerný čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky..
        public int S1PocetZakaznikov { get; private set; }
        public double S1_celkovy_cas_cakania { get; private set; }

        public void S1_AddValue(double time)
        {
            S1PocetZakaznikov++;
            S1_celkovy_cas_cakania += time;
        }

        /// <summary>
        ///     S1 – Priemerný čas čakania zákazníka v rade čakajúcich zákazníkov na zadanie objednávky..
        /// </summary>
        /// <returns></returns>
        public double S1_PriemernyCasCakania()
        {
            return S1PocetZakaznikov != 0 ? S1_celkovy_cas_cakania/S1PocetZakaznikov : 0.0;
        }

        private void S1_Reset()
        {
            S1PocetZakaznikov = 0;
            S1_celkovy_cas_cakania = 0.0;
        }
        

        #endregion

        #region S2 – Priemerný počet zákazníkov

        //S2 – Priemerný počet zákazníkov v rade čakajúcich zákazníkov. (vážený)
        private int _S2_posledny;
        private double _S2_dlzka_frontu;
        private double _S2_posledna_zmena;

        /// <summary>
        ///     Zmena poctu zakaznikov vo fronte.
        /// </summary>
        public void S2_AddValue()
        {
            if (_S2_posledny >= 0)
                _S2_dlzka_frontu += PocetCakajucichZakaznikov()*(CurrentTime - _S2_posledna_zmena);
            _S2_posledny = PocetCakajucichZakaznikov();
            _S2_posledna_zmena = CurrentTime;
        }

        private void S2_Reset()
        {
            _S2_posledny = 0;
            _S2_dlzka_frontu = 0.0;
            _S2_posledna_zmena = 0.0;
        }

        /// <summary>
        ///     S2 – Priemerný počet zákazníkov v rade čakajúcich zákazníkov. (vážený)
        /// </summary>
        /// <returns></returns>
        public double S2_PrimernyPocet()
        {
            return _S2_dlzka_frontu/CurrentTime;
        }

        #endregion

        #region S3 – Priemerný čas strávený zákazníkom v servise.

        //	S3 – Priemerný čas strávený zákazníkom v servise.
        private int _S3_pocet_zakaznikov;
        private double _S3_celkovy_cas_cakania;

        public void S3_AddValue(double time)
        {
            _S3_pocet_zakaznikov++;
            _S3_celkovy_cas_cakania += time;
        }

        /// <summary>
        ///     S3 – Priemerný čas strávený zákazníkom v servise.
        /// </summary>
        /// <returns></returns>
        public double S3_PriemernyCasVServise()
        {
            return _S3_pocet_zakaznikov != 0 ? _S3_celkovy_cas_cakania/_S3_pocet_zakaznikov : 0.0;
        }

        private void S3_Reset()
        {
            _S3_pocet_zakaznikov = 0;
            _S3_celkovy_cas_cakania = 0.0;
        }
        
        #endregion

        #region S4 – Priemerný čas strávený zákazníkom čakaním na opravu.

        //	S4 – Priemerný čas strávený zákazníkom čakaním na opravu.
        ///(čas začína plynúť okamihom ukončenia prevzatia auta do servisu a končí prevzatím opraveného auta)
        private int _S4_pocet_zakaznikov;

        private double _S4_celkovy_cas_cakania;

        public void S4_AddValue(double time)
        {
            _S4_pocet_zakaznikov++;
            _S4_celkovy_cas_cakania += time;
        }

        /// <summary>
        ///     S4 – Priemerný čas strávený zákazníkom čakaním na opravu.
        /// </summary>
        /// <returns></returns>
        public double S4_PriemernyCasOpravy()
        {
            return _S4_pocet_zakaznikov != 0 ? _S4_celkovy_cas_cakania/_S4_pocet_zakaznikov : 0.0;
        }

        private void S4_Reset()
        {
            _S4_pocet_zakaznikov = 0;
            _S4_celkovy_cas_cakania = 0.0;
        }

        #endregion

        #region S11 – Priemerný počet zákazníkov  na konci dna

        //S11 – Priemerný počet zákazníkov v rade čakajúcich zákazníkov na konci dna. (vážený)
        private int _S11_posledny;
        private double _S11_dlzka_frontu;
        private double _S11_posledna_zmena;

        /// <summary>
        ///     Zmena poctu zakaznikov vo fronte.
        /// </summary>
        public void S11_AddValue()
        {
            if (_S11_posledny >= 0)
                _S11_dlzka_frontu += PocetCakajucichZakaznikov()*(CurrentTime - _S11_posledna_zmena);
            _S11_posledny = PocetCakajucichZakaznikov();
            _S11_posledna_zmena = CurrentTime;
        }

        private void S11_Reset()
        {
            _S11_posledny = 0;
            _S11_dlzka_frontu = 0.0;
            _S11_posledna_zmena = 0.0;
        }

        /// <summary>
        ///     S11 – Priemerný počet zákazníkov v rade čakajúcich zákazníkov. (vážený)
        /// </summary>
        /// <returns></returns>
        public double S11_PrimernyPocet()
        {
            return _S11_dlzka_frontu/CurrentTime;
        }
       
        #endregion

        #region GLOBALNE STATISTIKY

        private double _sg1Global;
        private double _sg2Global;
        private double _sg3Global;
        private double _sg4Global;
        private double _sg11Global;

        private int _SG_pocetReplikacii;

        private void SG_AddValue()
        {
            _SG_pocetReplikacii++;
            _sg1Global += S1_PriemernyCasCakania();
            _sg2Global += S2_PrimernyPocet();
            _sg3Global += S3_PriemernyCasVServise();
            _sg4Global += S4_PriemernyCasOpravy();
            _sg11Global += S11_PrimernyPocet();
        }

        private void ResetGlobalStatistics()
        {
            _sg1Global = 0;
            _sg2Global = 0;
            _sg3Global = 0;
            _sg4Global = 0;
            _sg11Global = 0;
        }

        public double SG1_PriemernyCasCakania()
        {
            return _sg1Global/_SG_pocetReplikacii;
        }

        public double SG2_PrimernyPocet()
        {
            return _sg2Global/_SG_pocetReplikacii;
        }

        public double SG3_PriemernyCasVServise()
        {
            return _sg3Global/_SG_pocetReplikacii;
        }

        public double SG4_PriemernyCasOpravy()
        {
            return _sg4Global/_SG_pocetReplikacii;
        }

        public double SG11_PrimernyPocetNaKonciDna()
        {
            return _sg11Global/_SG_pocetReplikacii;
        }

        #endregion

        #region INTERVAL SPOLAHLIVOSTI - cas cakanie na opravu

        private int _isCount;
        private double _isSumSquare;
        private double _isSum;
        private const double T90 = 1.645;

        private void IS_AddValue(double value)
        {
            _isCount++;
            _isSum += value;
            _isSumSquare += Math.Pow(value, 2);
        }

        public double[] IS_NaOpravu()
        {
            var priemer = _isSum/_isCount;
            var smerodajnaOdchylka = Math.Sqrt(_isSumSquare/_isCount - Math.Pow(_isSum/_isCount, 2));

            var interval = T90*smerodajnaOdchylka/Math.Sqrt(_isCount - 1);
            if (double.IsNaN(interval))
            {
                return new[] { priemer, priemer };
            }
            return new[] { priemer -interval, priemer + interval};
        }

        public string IS()
        {
            var a = IS_NaOpravu();
            return "<" + a[0] + ",  " + a[1] + ">";
        }

        private void IS_Reset()
        {
            _isCount = 0;
            _isSumSquare = 0;
            _isSum = 0;
        }

        #endregion

        #region INTERVAL SPOLAHLIVOSTI - cas cakanie na opravu

        private int _isCount2;
        private double _isSumSquare2;
        private double _isSum2;

        private void IS_AddValue2(double value)
        {
            _isCount2++;
            _isSum2 += value;
            _isSumSquare2 += Math.Pow(value, 2);
        }

        public double[] IS_NaZadanieObjednavky()
        {
            var priemer = _isSum2 / _isCount2;
            var smerodajnaOdchylka = Math.Sqrt(_isSumSquare2 / _isCount2 - Math.Pow(_isSum2 / _isCount2, 2));

            var interval = T90 * smerodajnaOdchylka / Math.Sqrt(_isCount2 - 1);
            if (double.IsNaN(interval))
            {
                return new[] { priemer , priemer  };
            }
            return new[] { priemer- interval, interval+ priemer};
        }

        private void IS_CakanieZadanieObjednavky_Reset()
        {
            _isCount2 = 0;
            _isSumSquare2 = 0;
            _isSum2 = 0;
        }

        #endregion

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }


    public struct ResultAutoservice
    {
        public ResultAutoservice(int pocetPracovnikov1, int pocetPracovnikov2, double sg1PriemernyCasCakania, double sg2PrimernyPocet, double sg3PriemernyCasVServise, double sg4PriemernyCasOpravy, double sg11PrimernyPocetNaKonciDna, double isNaZadanieObjednavkyMin, double isNaZadanieObjednavkyMax, double isNaOpravyMin, double isNaOpravyMax)
        {
            PocetPracovnikov1 = pocetPracovnikov1;
            PocetPracovnikov2 = pocetPracovnikov2;
            Sg1PriemernyCasCakania = sg1PriemernyCasCakania;
            Sg2PrimernyPocet = sg2PrimernyPocet;
            Sg3PriemernyCasVServise = sg3PriemernyCasVServise;
            Sg4PriemernyCasOpravy = sg4PriemernyCasOpravy;
            Sg11PrimernyPocetNaKonciDna = sg11PrimernyPocetNaKonciDna;
            IS_NaZadanieObjednavkyMin = isNaZadanieObjednavkyMin;
            IS_NaZadanieObjednavkyMax = isNaZadanieObjednavkyMax;
            IS_NaOpravyMin = isNaOpravyMin;
            IS_NaOpravyMax = isNaOpravyMax;
        }

        public int PocetPracovnikov1 { get; set; }
        public int PocetPracovnikov2 { get; set; }
        public double Sg1PriemernyCasCakania { get; set; }

        public double Sg2PrimernyPocet { get; set; }
        public double Sg3PriemernyCasVServise { get; set; }
        public double Sg4PriemernyCasOpravy { get; set; }

        public double Sg11PrimernyPocetNaKonciDna { get; set; }

        public double IS_NaZadanieObjednavkyMin { get; set; }
        public double IS_NaZadanieObjednavkyMax { get; set; }
        public double IS_NaOpravyMin { get; set; }
        public double IS_NaOpravyMax { get; set; }
    }
}


   