using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AutoserviceLibrary;

namespace AutoserviceGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppCore  _app;

  

        public MainWindow()
        {
            InitializeComponent();
            _app = new AppCore(0, 0, 0, 0, 0 , null);
        }

        private void textBox1_Copy7_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

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
            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text),
                int.Parse(t_dlzkaJednejReplikacie.Text),
                int.Parse(t_pocetReplikacii.Text),
                double.Parse(t_maximalnyPocetReplikacii.Text),
                generators);
        }

        
        private void b_quickSimulation_Click(object sender, RoutedEventArgs e)
        {
            _initializeApp();
            
            _app.UltraSimulation();
        }

        private void b_runSimulation_Click(object sender, RoutedEventArgs e)
        {
            AutoserviceGenerators generators = _initializeGenerators();
            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text),
                int.Parse(t_dlzkaJednejReplikacie.Text),
                int.Parse(t_pocetReplikacii.Text),
                double.Parse(t_maximalnyPocetReplikacii.Text),
                generators);
            _app.NormalSimulation();
        }

        private void b_analyticSimulation_Click(object sender, RoutedEventArgs e)
        {
            AutoserviceGenerators generators = _initializeGenerators();
            _app = new AppCore(int.Parse(t_pocetPracovnikov1.Text),
                int.Parse(t_pocetPracovnikov2.Text),
                int.Parse(t_dlzkaJednejReplikacie.Text),
                int.Parse(t_pocetReplikacii.Text),
                double.Parse(t_maximalnyPocetReplikacii.Text),
                generators);
            _app.AnalyticSimulation();
            }


        public static void RefreshWindowDispatcher()
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background, 
                    new Action(delegate {})
                );
            }
        }







    }
}
