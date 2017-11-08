using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GaltonMachineWPF.Model
{
    public class Label
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Text { get; set; }

        public Label()
        {

        }

        public Label(double x, double y, string text)
        {
            X = x;
            Y = y;
            Text = text;
        }
    }
}
