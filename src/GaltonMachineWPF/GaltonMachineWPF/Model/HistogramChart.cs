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
        private BellCurve curve;

        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public HistogramChart(int size)
        {
            Histograms = new Histogram[size];
        }

        public int GetValue(int index)
        {
            return Histograms[index].Value;
        }

        public void IncrementValue(int index)
        {
            Histograms[index].Value++;
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
