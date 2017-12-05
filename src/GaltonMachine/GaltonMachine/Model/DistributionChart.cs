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
        }
        public ObservableCollection<ChartLabel> Labels
        {
            get { return labels; }
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
            histograms[index].Value = value;
            normalCurve.UpdateData(index, value);
        }

        #endregion

        #region ================== Metodi privati ==================

        public void GenerateChart(Size GDeviceSize)
        {
            if (Size > 0)
            {
                histograms = new ObservableCollection<Histogram>();
                for (int i = 0; i < Size; i++)
                {
                    histograms[i] = new Histogram();
                }
                normalCurve = new BellCurve(Size, GDeviceSize);
            }
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion

    }
}
