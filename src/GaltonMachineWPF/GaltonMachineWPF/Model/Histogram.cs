using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class Histogram
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Value { get; set; }

        public Histogram() : this (0, 0, 0, 0)
        {
        }
            
        public Histogram(double x, double y, double w, double h) : this (x, y, w, h, 0)
        {
        }

        public Histogram(double x, double y, double w, double h, int value)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            Value = value;
        }   
    }
}
