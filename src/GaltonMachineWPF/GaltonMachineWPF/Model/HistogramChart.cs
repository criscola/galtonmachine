using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /*public HistogramChart()
        {

        }*/

        public HistogramChart(int size, int gDeviceWidth)
        {
            GenerateChart();
            List<float> l = new List<float>();
            foreach (Histogram h in Histograms)
            {
                l.Add(h.Value);
            }
            //curve = new BellCurve(l, gDeviceWidth);
            
        }

        public int GetValue(int index)
        {
            return Histograms[index].Value;
        }

        public void SetValue(int index, int value)
        {
            Histograms[index].Value = value;
        }

        public void SetHistogram(int index, Histogram h)
        {
            Histograms[index] = h;
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

        // DEBUG
        public void PrintHistogramValues()
        {
            Console.WriteLine("---------------------------------------------------");
            for (int i = 0; i < size; i++)
            {   
                Console.WriteLine(Histograms[i]);
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }
}
