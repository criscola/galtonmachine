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
        public ObservableCollection<Histogram> HistogramsList { get; set; }

        private NormalCurve curve;

        public HistogramChart()
        {
            HistogramsList = new ObservableCollection<Histogram>();
        }

        // DEBUG
        public void PrintHistogramValues()
        {
            Console.WriteLine("---------------------------------------------------");
            for (int i = 0; i < HistogramsList.Count; i++)
            {   
                Console.WriteLine(HistogramsList[i].Value);
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }
}
