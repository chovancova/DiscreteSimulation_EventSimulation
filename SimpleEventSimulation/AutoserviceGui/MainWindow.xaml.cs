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
        public List<DataPoint> DataGrafStrategia1 { get; private set; }
        public List<DataPoint> DataGrafStrategia2 { get; private set; }

        ////////////////////////////vlakno, ktore vykonava simulaciu. 
        //////////////////////////private readonly BackgroundWorker _workerSimulation = new BackgroundWorker();
        public void RefreshGui()
        {
            RefreshWindowDispatcher();
            //tuto vytiahnem vsetky udaje z _app... 
            //v pripade tretieho rezumu updatnem grafy a poodobne..  
            t_s_sim_double_ak_cas1.Text = _app.CurrentTime.ToString();
            t_s_sim_replikacia1.Text = _app.ActualReplication.ToString();
            t_s_pocet_cakajucich_na_vybavenie.Text = _app.PocetCakajucichZakaznikov().ToString();
            t_s_pocet_v_p1.Text = _app.PocetVolnychPracovnikov1.ToString();
            t_s_pocet_v_p2.Text = _app.PocetVolnychPracovnikov2.ToString();
            t_s_pocet_opravenych_.Text = _app.PocetOpravenychAut().ToString();
            t_s_pocet_pokazenych_aut.Text = _app.PocetPokazenychAut().ToString();
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
            AutoserviceGenerators generators = _initializeGenerators();
            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text)*8*60*60;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            double maxSimulationTime = dlzkaReplikacie*pocetReplikacii;

            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text), generators, this);
        }

        
        private void b_quickSimulation_Click(object sender, RoutedEventArgs e)
        {
            _initializeApp();
            
           // _app.UltraSimulation();
        }

        private void b_runSimulation_Click(object sender, RoutedEventArgs e)
        {
            _initializeApp();
            _app.Refresh = true;
            int dlzkaReplikacie = int.Parse(t_dlzkaJednejReplikacie.Text) * 8 * 60 * 60;
            int pocetReplikacii = int.Parse(t_pocetReplikacii.Text);
            _app.Simulate(pocetReplikacii, dlzkaReplikacie);
            // _app.NormalSimulation();
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
    }
}
