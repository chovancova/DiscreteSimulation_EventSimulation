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
            t_l_double_simulacny.Content = "Double simulačný čas v sekundách:  "  + Math.Round(_app.CurrentTime,4).ToString();
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

            t_s_s1.Text = FormatToTime(_app.S1_PriemernyCasCakania());
            t_s_s2.Text = Math.Round(_app.S2_PrimernyPocet(),4).ToString();
            t_s_s3.Text = FormatToTime(_app.S3_PriemernyCasVServise());
            t_s_s4.Text = FormatToTime(_app.S4_PriemernyCasOpravy());
            t_s_s11.Text = (_app.S11_PrimernyPocet()).ToString();

            t_s_s1_Copy.Text = Math.Round(_app.S1_PriemernyCasCakania(), 4).ToString();
            t_s_s2_Copy.Text = Math.Round(_app.S2_PrimernyPocet(), 4).ToString();
            t_s_s3_Copy.Text = Math.Round(_app.S3_PriemernyCasVServise(), 4).ToString(); ;
            t_s_s4_Copy.Text = Math.Round(_app.S4_PriemernyCasOpravy(), 4).ToString();
            t_s_s11_Copy.Text = Math.Round(_app.S11_PrimernyPocet(), 4).ToString();
            }
          

            if (UltraMode)
            {
                t_s_repl.Text = _app.ActualReplication.ToString();
                t_g_s1.Text = FormatToTime(_app.SG1_PriemernyCasCakania());
                t_g_s2.Text = Math.Round(_app.SG2_PrimernyPocet(), 4).ToString();
                t_g_s3.Text = FormatToTime(_app.SG3_PriemernyCasVServise());
                t_g_s4.Text = FormatToTime(_app.SG4_PriemernyCasOpravy());
                t_g_s1_Copy.Text = Math.Round(_app.SG1_PriemernyCasCakania(), 4).ToString();
                t_g_s2_Copy.Text = Math.Round(_app.SG2_PrimernyPocet(), 4).ToString();
                t_g_s3_Copy.Text = Math.Round(_app.SG3_PriemernyCasVServise(),4).ToString();
                t_g_s4_Copy.Text = Math.Round(_app.SG4_PriemernyCasOpravy(), 4).ToString();
                a = _app.IS_CakanieNaOpravu();
                //t_g_is_0.Text = FormatToTime(a[0]);
                //t_g_is_1.Text = FormatToTime(a[1]);
                //t_g_is_2.Text = Math.Round((a[0]), 4).ToString();
                //t_g_is_3.Text = Math.Round((a[1]), 4).ToString();
      
   
                // t_s_s11.Text = (_app.S11_PrimernyPocet()).ToString();
            }
        }

        private double[] a;
        private string FormatToTime(double seconds)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromSeconds(seconds);
                return string.Format("{0} h {1} m {2} s", ts.Hours, ts.Minutes, ts.Seconds);
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

        }
        private void InitializeGraphsComponents()
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
            //AutoserviceGenerators generators = _initializeGenerators();

            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text)*8*60*60;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            double maxSimulationTime = dlzkaReplikacie*pocetReplikacii;

            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text), new AutoserviceGenerators(), this);
        }

        public void RunUltraMode()
        {
            if(_app==null) _initializeApp();

            UltraMode = true; //gui - what to render

            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text) * AppCore.DlzkaDnaSekundy;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            int pocetPR1 = int.Parse(t_pocetPracovnikov3.Text);
            int pocetPR2 = int.Parse(t_pocetPracovnikov4.Text);

            _app.UltraModeSimulation(pocetReplikacii, dlzkaReplikacie, pocetPR1, pocetPR2);
        }

        public void RunNormalMode()
        {
            if (_app == null) _initializeApp();

            _app.RefreshRate = int.Parse(t_refreshRAte.Text);
            _app.SleepingTime = int.Parse(t_sleepMs.Text);


            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text) * AppCore.DlzkaDnaSekundy;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            int pocetPR1 = int.Parse(t_pocetPracovnikov1_normal.Text);
            int pocetPR2 = int.Parse(t_pocetPracovnikov2_normal.Text);

            _app.NormalModeSimulation(pocetReplikacii, dlzkaReplikacie, pocetPR1, pocetPR2);

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

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            RunUltraMode();
            button_Copy.IsEnabled = false;
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = true;
            button_Copy1.IsEnabled = false;
            button_Copy5.IsEnabled = true;
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            _app.Paused = false;
            button_Copy1.IsEnabled = true;
            button_Copy5.IsEnabled = false;
        }

        private void button_Copy6_Click(object sender, RoutedEventArgs e)
        {
            _app.Stopped = true; 
            button_Copy6.IsEnabled = false;
            button_Copy.IsEnabled = true; 
        }
    }
}
