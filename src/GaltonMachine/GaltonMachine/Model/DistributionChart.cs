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
        private double gDeviceVerticalOffset;

        public Size GDeviceSize { get; set; }
        public double GDeviceVerticalOffset
        {
            get { return gDeviceVerticalOffset; }
            set
            {
                gDeviceVerticalOffset = value;
            }
        }

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
            set { size = value; }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public DistributionChart()
        {

        }

        public DistributionChart(int size, Size gDeviceSize, double gDeviceVerticalOffset)
        {
            Size = size;
            GDeviceSize = gDeviceSize;
            GDeviceVerticalOffset = gDeviceVerticalOffset;
            GenerateChart();
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
                    double barHeight = Math.Round(perc * (GDeviceSize.Height - GDeviceVerticalOffset));
                    h.Height = barHeight;
                    h.Y = GDeviceSize.Height - barHeight - (gDeviceVerticalOffset / 2);
                    // TODO: Aggiornare labels   
                }

            }
            else
            {
                throw new System.ArgumentOutOfRangeException("index", "param index is greater than histograms count.");
            }
            
        }

        public BitmapImage GetCurveImage()
        {
            normalCurve.Image.Freeze();
            return normalCurve.Image;
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
            Histograms = new ObservableCollection<Histogram>();
            Labels = new ObservableCollection<ChartLabel>();
            if (Size > 0)
            {
                Histograms = new ObservableCollection<Histogram>();
                for (int i = 0; i < Size; i++)
                {
                    Histograms.Add(new Histogram());
                    Labels.Add(new ChartLabel());
                }
                normalCurve = new BellCurve(Size, GDeviceSize);
                
            }
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion

    }
}
