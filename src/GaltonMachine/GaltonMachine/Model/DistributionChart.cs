using System.Collections.ObjectModel;
using System.Drawing;

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

        public DistributionChart(int size, Size gDeviceSize)
        {
            Size = size;
            GDeviceSize = gDeviceSize;

            GenerateChart(GDeviceSize);
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void SetValue(int index, int value)
        {
            if (Histograms.Count > index)
            {
                Histograms[index].Value = value;
                normalCurve.UpdateData(index, value);
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("index", "param index is greater than histograms count.");
            }
            
        }

        #endregion

        #region ================== Metodi privati ==================

        public void GenerateChart(Size GDeviceSize)
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
