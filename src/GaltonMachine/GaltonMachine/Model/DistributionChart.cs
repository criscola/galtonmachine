using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;

namespace GaltonMachine.Model
{
    public class DistributionChart
    {

        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private ObservableCollection<Histogram> histograms;
        private ObservableCollection<ChartLabel> labels;
        private BellCurve normalCurve;
        private int size;

        public Size GDeviceSize { get; set; }
        public double HistogramWidth { get; set; }
        public double VerticalAnchor { get; set; }

        public ObservableCollection<Histogram> Histograms
        {
            get { return histograms; }
            set { histograms = value; }
        }
        public ObservableCollection<ChartLabel> Labels
        {
            get { return labels; }
            set { labels = value; }
        }
        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                GenerateChart();
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public DistributionChart()
        {

        }

        public DistributionChart(int size, Size gDeviceSize, double histogramWidth, double verticalAnchor)
        {
            GDeviceSize = gDeviceSize;
            HistogramWidth = histogramWidth;
            VerticalAnchor = verticalAnchor;
            Labels = new ObservableCollection<ChartLabel>();
            Histograms = new ObservableCollection<Histogram>();
            Size = size;
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void IncrementValue(int index)
        {
            if (Histograms.Count > index)
            {
                Histograms[index].Value++;
                normalCurve.UpdateData(index, Histograms[index].Value);

                Histogram maxHistogram = Histograms.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2);

                // Aggiorna altezza degli istogrammi
                for (int i = 0; i < Histograms.Count; i++)
                {
                    Histogram h = Histograms[i];
                    int value = h.Value;
                    float perc = value / (float)maxHistogram.Value;
                    double barHeight = Math.Round(perc * (GDeviceSize.Height));
                    if (barHeight < 0) barHeight = 1;
                    h.Height = barHeight;
                    h.Y = VerticalAnchor - barHeight;
                }
                Labels[index].Text = Histograms[index].Value.ToString();
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "param index is greater than histograms count.");
            }
            
        }

        public BitmapImage GetCurveImage()
        {
            if (normalCurve.Image != null)
            {
                return normalCurve.Image;
            }
            return null;
        }

        public int GetDataCount()
        {
            return normalCurve.GetDataCount();
        }

        public void Reset()
        {
            GenerateChart();
        }

        #endregion

        #region ================== Metodi privati ==================

        public void GenerateChart()
        {
            if (Size > 0)
            {
                Histograms.Clear();
                Labels.Clear();

                // Distanza fra gli istogrammi
                double dx = (GDeviceSize.Width - (HistogramWidth * Size)) / (Size + 1);

                // Coordinate iniziali x y degli istogrammi 
                double x = dx;

                for (int i = 0; i < Size; i++)
                {   
                    Histograms.Add(new Histogram(x, VerticalAnchor, HistogramWidth, 0));
                    Labels.Add(new ChartLabel(x, VerticalAnchor, HistogramWidth, "0"));

                    x += dx + HistogramWidth;
                }
                normalCurve = new BellCurve(Size, GDeviceSize);
                
            }
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion

    }
}
