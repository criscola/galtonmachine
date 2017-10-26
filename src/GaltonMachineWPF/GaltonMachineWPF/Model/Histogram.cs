using GaltonMachineWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class Histogram : BindableBase
    {
        private double x;
        private double y;
        private double width;
        private double height;
        private int value;

        public double X {
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged(() => x);
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged(() => y);
            }
        }
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                OnPropertyChanged(() => width);
            }
        }
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged(() => height);
            }
        }
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged(() => this.value);
            }
        }

        public Histogram() : this (0, 0, 0, 0)
        {
        }
            
        public Histogram(double x, double y, double w, double h) : this (x, y, w, h, 0)
        {
        }

        public Histogram(double x, double y, double w, double h, int value)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            this.value = value;
        }   
    }
}
