using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ExampleChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Histogram> HistogramsList { get; set; }
        public double HISTOGRAM_WIDTH { get; set; }
        private Thread ballAnimationThread;
        public MainWindow()
        {
            InitializeComponent();
            ballAnimationThread = new Thread(new ThreadStart(GenerateHistograms));
            ballAnimationThread.Start();
            HistogramsList = new ObservableCollection<Histogram>();
            GenerateHistograms();
        }

        private void GenerateHistograms()
        {
            if (HistogramsList != null)
            {
                HistogramsList.Clear();

                // Offset orizzontale e verticale
                double hoffset = 50;

                // Larghezza e altezza canvas 
                double cw = 440;
                double ch = 400;

                // Conteggio degli istogrammi
                double n = 8;

                // Distanza fra gli istogrammi
                double dx = ((cw - hoffset * 2) - (HISTOGRAM_WIDTH * n)) / (n - 1);

                // Coordinate x y delle stecche iniziali
                double x = hoffset;
                // Valore solo per test iniziale!
                double y = ch;

                HistogramsList.Add(new Histogram(x, y, HISTOGRAM_WIDTH, 0));

                // Crea i nuovi istogrammi
                for (int i = 0; i < n - 1; i++)
                {
                    x += dx + HISTOGRAM_WIDTH;
                    HistogramsList.Add(new Histogram(x, y, HISTOGRAM_WIDTH, 0));
                }
            }
        }
    }
}
