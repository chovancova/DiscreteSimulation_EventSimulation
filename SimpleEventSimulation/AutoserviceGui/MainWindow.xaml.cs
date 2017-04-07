using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using AutoserviceLibrary;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using SimulationLibrary;
using LinearAxis = OxyPlot.Wpf.LinearAxis;

namespace AutoserviceGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ISimulationGui
    {
      private AppCore  _app;
        private bool UltraMode = false;
        public List<DataPoint> DataGrafStrategia1 { get; private set; }
        public List<DataPoint> DataGrafStrategia2 { get; private set; }

        ////////////////////////////vlakno, ktore vykonava simulaciu. 
        //////////////////////////private readonly BackgroundWorker _workerSimulation = new BackgroundWorker();
        public void RefreshGui()
        {
            RefreshWindowDispatcher();
          
            if (!UltraMode)
            {
            //tuto vytiahnem vsetky udaje z _app... 
            //v pripade tretieho rezumu updatnem grafy a poodobne..  
            t_l_double_simulacny.Content = "Double simulačný čas v sekundách:  "  + Math.Round(_app.CurrentTime,1).ToString();
            //t_l_replikacia.Content = "Replikácia:  "+ _app.ActualReplication;
            TimeSpan ts = TimeSpan.FromSeconds((_app.CurrentTime*3));
            TimeSpan ts2 = TimeSpan.FromSeconds((_app.CurrentTime));
            string format = string.Format("{0} d {1} h {2}m {3}s",ts.Days, (int) (ts.Hours/3+7), ts2.Minutes, ts2.Seconds);
            t_l_aktualny_cas.Content = "Aktuálny čas:  " + format;
            t_s_pocet_cakajucich_na_vybavenie.Text = _app.PocetCakajucichZakaznikov().ToString();
            t_s_pocet_v_p1.Text = _app.PocetVolnychPracovnikov1.ToString();
            t_s_pocet_v_p2.Text = _app.PocetVolnychPracovnikov2.ToString();
            t_s_pocet_opravenych_.Text = _app.PocetOpravenychAut().ToString();
            t_s_pocet_pokazenych_aut.Text = _app.PocetPokazenychAut().ToString();

                //t_s_s1.Text = FormatToTime(_app.S1_PriemernyCasCakania());
                //t_s_s2.Text = Math.Round(_app.S2_PrimernyPocet(),4).ToString();
                //t_s_s3.Text = FormatToTime(_app.S3_PriemernyCasVServise());
                //t_s_s4.Text = FormatToTime(_app.S4_PriemernyCasOpravy());
                //t_s_s11.Text = (_app.S11_PrimernyPocet()).ToString();
                

                t_s_pocet_zakaznikov_v_rade.Text =""+ _app.CelkovyPocetZakaznikov;
                t_s_pocet_opustiliSystem.Text = "" + _app.PocetLudiOdisli;
                t_s_pocet_opravenych_v_rade.Text = "" + _app.CelkovyPocetOpravenychAut;
                t_s_pocet_zakaznikov_na_konci.Text = "" + _app.Front_CakajuciZakaznici_NaKonciDna;
                t_s_pocet_celkovy_na_konci_dna.Text = "" + _app.Front_CakajuciZakaznici_CelkovyNaKonciDna;
                t_s_pocet_zak_v_rade_cakajucich.Text = "" + _app.S1PocetZakaznikov;
                t_s_pocet_zak_v_rade_cakajucich_Copy.Text = "" + _app.S1_celkovy_cas_cakania;
                //t_s_s1_Copy.Text = Math.Round(_app.S1_PriemernyCasCakania(), 4).ToString();
                //t_s_s2_Copy.Text = Math.Round(_app.S2_PrimernyPocet(), 4).ToString();
                //t_s_s3_Copy.Text = Math.Round(_app.S3_PriemernyCasVServise(), 4).ToString(); ;
                //t_s_s4_Copy.Text = Math.Round(_app.S4_PriemernyCasOpravy(), 4).ToString();
                //t_s_s11_Copy.Text = Math.Round(_app.S11_PrimernyPocet(), 4).ToString();
            }
          

            if (UltraMode)
            {
                t_s_repl.Text = ""+ _app.ActualReplication;
            }

            if (_app.Done)
            {
                double[] is1 = _app.IS_NaZadanieObjednavky();
                t_u_is_cas_zadania_objednavky.Content = "90 % Interval spoľahlivosti - čas čakania v rade na zadanie objednávky < " +
                     Math.Round(is1[0], 3) + ", " + Math.Round(is1[1], 3) + ">,    alebo <"
                    + FormatToTime(Math.Round(is1[0], 4)) + ", " + FormatToTime(Math.Round(is1[1], 4)) + ">.";

                double[] is2 = _app.IS_NaOpravu();
                t_s_is_oprava.Content = "90 % Interval spoľahlivosti - čas čakania na opravu je < " +
                      Math.Round(is2[0], 3) + "  ,  " + Math.Round(is2[1], 3) + " >,    alebo <"
                    + FormatToTime(Math.Round(is2[0], 4)) + ", " + FormatToTime(Math.Round(is2[1], 4)) + ">.";

                double[] is3 = _app.IS_VSysteme();
                t_s_is_systeme.Content = "90 % Interval spoľahlivosti - čas strávený zákazníkov v systéme je < " +
                      Math.Round(is3[0], 3) + "  ,  " + Math.Round(is3[1], 3) + " >,    alebo <"
                    + FormatToTime(Math.Round(is3[0], 3)) + ", " + FormatToTime(Math.Round(is3[1], 3)) + ">.";

                t_u_pr_servise.Content = "Priemerný čas strávený zákazníkom v servise je " +
                                         FormatToTime(_app.SG3_PriemernyCasVServise()) + ", alebo " +
                                         Math.Round(_app.SG3_PriemernyCasVServise(), 4) + " sekúnd. ";

                t_u_pr_cakanie_zakaznik.Content = "Priemerný čas čakania v rade čakajúcich zákazníkov na zadanie objednávky je "
                    + FormatToTime(_app.SG1_PriemernyCasCakania()) + ", alebo " +
                     Math.Round(_app.SG1_PriemernyCasCakania(), 4)
                    + " sekúnd. ";

                t_u_pr_pocet_v_rade.Content = "Priemerný počet zákazníkov v rade čakajúcich zákazníkov je "
                                              + Math.Round(_app.SG2_PrimernyPocet(), 4);

                t_u_pr_pocet_v_rade_na_konci_dna.Content =
                    "Priemerný počet zákazníkov v rade čakajúcich zákazníkov na konci dňa je " +
                    Math.Round(_app.SG11_PrimernyPocetNaKonciDna(), 4);

                t_u_pr_na_opravu.Content = "Priemerný čas strávený zákazníkov čakaním na opravu je " +
                                           FormatToTime(_app.SG4_PriemernyCasOpravy()) + ", alebo " +
                                           Math.Round(_app.SG4_PriemernyCasOpravy(), 4)
                                           + " sekúnd. ";

                t_u_pr_na_opravu.Content = "Priemerný čas strávený zákazníkom v systéme je " +
                                           FormatToTime(_app.SG5_PriemernyCasVSysteme()) + ", alebo " +
                                           Math.Round(_app.SG5_PriemernyCasVSysteme(), 4)
                                           + " sekúnd. ";
                

                if (Math.Round(is1[1], 4) >= 180)
                {
                    label_odporucanie2.Content =
                        "Konfigurácia nie je vhodná, pretože priemerný čas čakania zákazníkov v rade na zadanie objednávky je väčší než 3 minúty (presne o  " +
                        FormatToTimeMinutes((Math.Round(_app.SG1_PriemernyCasCakania(), 0) - 180)) + "). ";

                }
                else
                {
                    label_odporucanie2.Content =
                   "Konfigurácia spĺňa podmienku -  priemerný čas čakania zákazníkov v rade na zadanie objednávky je menej než 3 minúty (" +
                   FormatToTimeMinutes((Math.Round(_app.SG1_PriemernyCasCakania(), 0) )) + "). ";

                }

                if (Math.Round(is2[1], 4) >= 18000)
                {
                    label_odporucanie.Content =
                        "Konfigurácia nie je vhodná, pretože priemerný čas čakania zákazníkov na opravu je väčší než 5 hodín, presne o " +
                        FormatToTimeMinutes((Math.Round(_app.SG4_PriemernyCasOpravy(), 4) - 18000)) + ". ";
                }
                else
                {
                    label_odporucanie.Content = "Konfigurácia spĺňa podmienku - priemerný čas čakania na opravu je menší než 5 hodín (" +
                    FormatToTime((Math.Round(_app.SG4_PriemernyCasOpravy(), 4))) + ").";
                }

                if (!(Math.Round(is2[1], 4) > 18000)
                    && !(Math.Round(is1[1], 4) > 180))
                {
                    label2.Content = "Odporúčanie: ÁNO.";
                }
                else
                {
                    label2.Content = "Odporúčanie: NIE.";
                }
            }
        }

        private string FormatToTime(double seconds)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromSeconds(seconds);
                return string.Format("{0} h {1} m {2} s", ts.Hours, ts.Minutes, ts.Seconds);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        private string FormatToTimeMinutes(double seconds)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromSeconds(seconds);
                return string.Format("{0} m {1} s",  ts.Minutes, ts.Seconds);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static void RefreshWindowDispatcher()
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new Action(delegate { })
                );
            }
        }
        private void UpdateGraph1(int x, double y)
        {
            DataGrafStrategia1.Add(new DataPoint(x, y));
            GraphStrategia1.InvalidatePlot();
        }

        private void UpdateGraph2(int x, double y)
        {
            DataGrafStrategia2.Add(new DataPoint(x, y));
            GraphStrategia2.InvalidatePlot();
        }

        public MainWindow()
        {

            InitializeComponent();
            _initializeApp();
            _ultra_mode_set_enable_buttons(true, false, false, false);
            InitializeGraphsComponents();
            InitializeGraph1();
            InitializeGraph2();

            RunAllResults();
        }

        private void ResetGraph1()
        {
            DataGrafStrategia1.Clear();
            GraphStrategia1.InvalidatePlot();
        }
         private void ResetGraph2()
        {
            DataGrafStrategia2.Clear();
            GraphStrategia2.InvalidatePlot();
        }

        private void InitializeGraph1()
        {
            DataGrafStrategia1 = new List<DataPoint>();
            GraphStrategia1.Series.Add(new LineSeries { ItemsSource = DataGrafStrategia1 });

            GraphStrategia1.Axes.Add(new LinearAxis
            {
                Title = "Počet pracovníkov skupiny 1",
                Position = AxisPosition.Bottom,
                TitleFontSize = 14,
                AxisTitleDistance = 1
            });
            GraphStrategia1.Axes.Add(new LinearAxis
            {
                Title = "Priemerný počet čakajúcich v rade",
                Position = AxisPosition.Left,
                TitleFontSize = 14,
                AxisTitleDistance = 20
            });
        }

        private void InitializeGraph2()
        {
            DataGrafStrategia2= new List<DataPoint>();
            GraphStrategia2.Series.Add(new LineSeries { ItemsSource = DataGrafStrategia2 });
            GraphStrategia2.Axes.Add(new LinearAxis
            {
                Title = "Počet pracovníkov skupiny 2",
                Position = AxisPosition.Bottom,
                TitleFontSize = 14,
                AxisTitleDistance = 1
            });

            GraphStrategia2.Axes.Add(new LinearAxis
            {
                Title = "Priemerný čas strávený zákazníkom v servise",
                Position = AxisPosition.Left,
                TitleFontSize = 14,
                AxisTitleDistance = 20
            });
        }

        private void InitializeGraphsComponents()
        {
         

            
            //////////////////////////////_workerSimulation.DoWork += WorkerProcess;
            //////////////////////////////_workerSimulation.RunWorkerCompleted += WorkerCompleted;
        }
        //////////////////////////////druhe vlakno
        ////////////////////////////private void WorkerProcess(object sender, DoWorkEventArgs e)
        ////////////////////////////{
        ////////////////////////////    //here will be _app.DoSomething() 
        ////////////////////////////    //spustenie simulacie... 
        ////////////////////////////}
        ////////////////////////////void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        ////////////////////////////{
        ////////////////////////////    //todo co sa stane ked skonci .. 
        ////////////////////////////}
        ////////////////////////////public void StartSimulation()
        ////////////////////////////{
        ////////////////////////////    List<DataPoint>[] GrafPriebehuSimulacie = new[]
        ////////////////////////////       {DataGrafStrategia1, DataGrafStrategia2};
        ////////////////////////////    _workerSimulation.RunWorkerAsync();
        ////////////////////////////}

        private AutoserviceGenerators _initializeGenerators()
        {
            return  new AutoserviceGenerators(
               int.Parse(gen1.Text), 
               double.Parse(gen213.Text), 
               double.Parse(gen223.Text), 
               double.Parse(gen233.Text), 
               double.Parse(gen243.Text), 
               double.Parse(gen253.Text), 
               double.Parse(gen263.Text),
                int.Parse(gen31.Text), int.Parse(gen32.Text),
            int.Parse(gen41.Text), int.Parse(gen42.Text),
            int.Parse(gen51.Text), int.Parse(gen52.Text), int.Parse(gen53.Text),
            int.Parse(gen61.Text), int.Parse(gen62.Text),
            double.Parse(gen71.Text), double.Parse(gen72.Text), double.Parse(gen73.Text),
            int.Parse(gen711.Text), int.Parse(gen712.Text),
            int.Parse(gen721.Text), int.Parse(gen722.Text), double.Parse(gen723.Text),
            int.Parse(gen731.Text), int.Parse(gen732.Text), double.Parse(gen733.Text),
            int.Parse(gen741.Text), int.Parse(gen742.Text), double.Parse(gen743.Text),
            int.Parse(gen751.Text), int.Parse(gen752.Text)
                );
        }

        private void _initializeApp()
        {
            AutoserviceGenerators generators = _initializeGenerators();

            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text)*8*60*60;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            double maxSimulationTime = dlzkaReplikacie*pocetReplikacii;

           // _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
           //     int.Parse(t_pocetPracovnikov2.Text), new AutoserviceGenerators(), this);
            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text), generators, this);
        }

        public void RunUltraMode()
        {
           _initializeApp();

            UltraMode = true; //gui - what to render

            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text) * AppCore.DlzkaDnaSekundy;
            int pocetReplikacii = int.Parse(t_s_repl.Text);
            int pocetPR1 = int.Parse(t_pocetPracovnikov3.Text);
            int pocetPR2 = int.Parse(t_pocetPracovnikov4.Text);

            _app.UltraModeSimulation(pocetReplikacii, dlzkaReplikacie, pocetPR1, pocetPR2);
        }

        public void RunNormalMode()
        {
            _initializeApp();

            _app.RefreshRate = int.Parse(t_refreshRAte.Text);
            _app.SleepingTime = int.Parse(t_sleepMs.Text);
            
            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text) * AppCore.DlzkaDnaSekundy;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            int pocetPR1 = int.Parse(t_pocetPracovnikov1_normal.Text);
            int pocetPR2 = int.Parse(t_pocetPracovnikov2_normal.Text);

            _app.NormalModeSimulation(1, dlzkaReplikacie, pocetPR1, pocetPR2);
        }
        
        private void b_quickSimulation_Click(object sender, RoutedEventArgs e)
        {
            RunUltraMode();
        }

        private void b_runSimulation_Click(object sender, RoutedEventArgs e)
        {
            RunNormalMode();

            b_runSimulation.IsEnabled = false;
            b_continue1.IsEnabled = false;
            b_runSimulation_Copy.IsEnabled = false;

        }

        private void b_analyticSimulation_Click(object sender, RoutedEventArgs e)
        {
            _initializeApp();

            InitializeGraphsComponents();

            //  _app.AnalyticSimulation();
        }
        
        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            _app.Stopped = true;
            b_runSimulation_Copy.IsEnabled = true;
        }

        private void b_pause1_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = true;
            b_pause1.IsEnabled = false;
            b_continue1.IsEnabled = true;
        }

        private void b_continue1_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = false;
            b_pause1.IsEnabled = true;
            b_continue1.IsEnabled = false;
        }

        private void b_changeSpeed_Click(object sender, RoutedEventArgs e)
        {
            _app.SleepingTime = int.Parse(t_sleepMs.Text);
            _app.RefreshRate = int.Parse(t_refreshRAte.Text);
        }

        private void s_sleepMs_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void s_refreshRAte_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void t_sleepMs_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void t_refreshRAte_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _app.SleepingTime = int.Parse(t_sleepMs.Text);
            _app.RefreshRate = int.Parse(t_refreshRAte.Text);
        }

        private void b_u_start_Click(object sender, RoutedEventArgs e)
        {
            _ultra_mode_set_enable_buttons(false, true, false, true);

            label_odporucanie.Content = "-";
            label_odporucanie2.Content = "-";
            label2.Content = "Odporúčanie: -";

            t_u_is_cas_zadania_objednavky.Content = "90 % Interval spoľahlivosti - čas čakania v rade na zadanie objednávky ";
             t_s_is_oprava.Content = "90 % Interval spoľahlivosti - čas čakania na opravu je" ;
            t_u_pr_servise.Content = "Priemerný čas strávený zákazníkov v servise je " ;
            t_u_pr_cakanie_zakaznik.Content = "Priemerný čas čakania v rade čakajúcich zákazníkov na zadanie objednávky je ";
            t_u_pr_pocet_v_rade.Content = "Priemerný počet zákazníkov v rade čakajúcich zákazníkov je ";
            t_u_pr_pocet_v_rade_na_konci_dna.Content ="Priemerný počet zákazníkov v rade čakajúcich zákazníkov na konci dňa je " ;
            t_u_pr_na_opravu.Content = "Priemerný čas strávený zákazníkov čakaním na opravu je " ;

            RunUltraMode();
        }

        private void b_u_pause_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = true;
            _ultra_mode_set_enable_buttons(false, false, true, true);
          }

        private void b_u_continue_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = false;
            _ultra_mode_set_enable_buttons(false, true, false, true);
        }

        private void b_u_stop_Click(object sender, RoutedEventArgs e)
        {
            _app.Stopped = true;
            _ultra_mode_set_enable_buttons(true, false, false, false);
        }

        private void _ultra_mode_set_enable_buttons(bool start, bool pause, bool continuebutton, bool stop)
        {
            b_u_pause.IsEnabled = pause;
            b_u_stop.IsEnabled = stop;
            b_u_continue.IsEnabled = continuebutton;
            b_u_start.IsEnabled = start;
        }

        private bool Graph1Break = false;
        private bool Graph2Break = false;

        //private List<DataPoint> _data_graf1;

        public void RunGraphsResultStrategy1(int min, int max, int pracovnikov2, int replikacii, int dlzka)
        {
           // _data_graf1 = new List<DataPoint>(max - min + 1);

            if (!Graph1Break)
            {
                for (int i = min; i <= max; i++)
                {
                    var a = new AppCore(i, pracovnikov2, new AutoserviceGenerators(), null);
                    
                        a.Refresh = false;
                        a.Simulate(replikacii, dlzka);
                       // _data_graf1.Add(new DataPoint(i, a.SG2_PrimernyPocet()));
                        // v core vyvolat ... obnovenie grafu
                         UpdateGraph1(i, a.SG2_PrimernyPocet());
                       // DataGrafStrategia1.Add(new DataPoint(i, a.SG2_PrimernyPocet()));
                       // GraphStrategia1.InvalidatePlot();

                    
                }
            }
        }

      //  private List<DataPoint> _data_graf2;
        public void RunGraphsResultStrategy2(int min, int max, int pracovnikov1, int replikacii, int dlzka)
        {
            //_data_graf2 = new List<DataPoint>(max-min+1);

            for (int i = min; i <= max; i++)
            {
                if (!Graph2Break)
                {
                    var a = new AppCore(pracovnikov1, i, new AutoserviceGenerators(), null);
                    a.Refresh = false;
                    a.Simulate(replikacii, dlzka);
                        // v core vyvolat ... obnovenie grafu

                        // _data_graf2.Add(new DataPoint(i, a.SG5_PriemernyCasVSysteme()));
                        UpdateGraph2(i, a.SG5_PriemernyCasVSysteme());
                       // DataGrafStrategia2.Add(new DataPoint(i, a.SG5_PriemernyCasVSysteme()));
                       // GraphStrategia2.InvalidatePlot();
                }
            }
        }

        private void b_analyticSimulation_start_1_graf1_Click(object sender, RoutedEventArgs e)
        {
            Graph1Break = false;
            ResetGraph1();
            int min = int.Parse(t_g1_interval1_Copy.Text);
            int max = int.Parse(t_g1_interval2_Copy.Text);
            int pracovnikov2 = int.Parse(t_g1_pocetPracovnikov_Copy.Text);
            int replikacie = int.Parse( t_g1_pocet_replikacii.Text);
            int dlzka  = int.Parse(t_dlzkaJednejReplikacie.Text) * 8 * 60 * 60;
            RunGraphsResultStrategy1(min, max, pracovnikov2, replikacie, dlzka);
        }

        private void b_analyticSimulation_stop_1_graf1_Click(object sender, RoutedEventArgs e)
        {
            Graph1Break = false;
        }

        private void b_analyticSimulation_start_1_graf2_Click(object sender, RoutedEventArgs e)
        {
            Graph2Break = false;
            ResetGraph2();
            int min = int.Parse(t_g2_interval1_Copy1.Text);
            int max = int.Parse(t_g2_interval2_Copy1.Text);
            int pracovnikov1 = int.Parse(t_g2_pocetPracovnikov_Copy1.Text);
            int replikacie = int.Parse(t_g1_pocet_replikacii_Copy.Text);
            int dlzka = int.Parse(t_dlzkaJednejReplikacie.Text) * 8 * 60 * 60;
            RunGraphsResultStrategy2(min, max, pracovnikov1, replikacie, dlzka);

        }

        private void b_analyticSimulation_stop_1_graf2_Click(object sender, RoutedEventArgs e)
        {
            Graph2Break = false;

        }

        List<ResultAutoservice> results = new List<ResultAutoservice>();
        public void RunAllResults(int replikacii=35, int dlzka=2592000)
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 16; j <= 26; j++)
                {
                    var a = new AppCore(i, j, new AutoserviceGenerators(), null);
                    
                        a.Refresh = false;
                        a.Simulate(replikacii, dlzka);
                        results.Add(a.Results);
                }
            }
        }

    }
}
