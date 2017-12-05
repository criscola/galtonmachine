using GaltonMachine.Model.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachine.Model
{
    public class DistributionChart
    {

        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private Histogram[] histograms;
        private BellCurve normalCurve;
        private int size;
        
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

        public DistributionChart(int size, Size GDeviceSize)
        {
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

        public void GenerateChart(Size gDeviceSize)
        {
            histograms = new Histogram[Size];
            for (int i = 0; i < histograms.Length; i++)
            {
                histograms[i] = new Histogram();
            }
            normalCurve = new BellCurve(Size, gDeviceSize);
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion

    }
}
