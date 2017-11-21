using System;
using System.Collections.Generic;

namespace GaltonMachineWPF.Model
{
    public class HistogramChart
    {
        private Histogram[] Histograms { get; set; }
        public BellCurve Curve { get; set; }

        private int size;

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                GenerateChart();
            }
        }

        public HistogramChart()
        {

        }

        public HistogramChart(int size, System.Drawing.Size gDeviceSize)
        {
            GenerateChart();
        }

        public int GetValue(int index)
        {
            return Histograms[index].Value;
        }

        public void SetValue(int index, int value)
        {
            Histograms[index].Value = value;
        }
        public Histogram GetHistogram(int index)
        {
            if (Histograms.Length > index)
            {
                return Histograms[index];
            }
            return null;
        }

        public void SetHistogram(int index, Histogram h)
        {
            Histograms[index] = h;
            Curve.UpdateData(index, h.Value);
        }

        public void IncrementValue(int index)
        {
            Histograms[index].Value++;
        }

        public void GenerateChart()
        {
            Histograms = new Histogram[size];
            for (int i = 0; i < Histograms.Length; i++)
            {
                Histograms[i] = new Histogram();
            }
        }
    }
}
