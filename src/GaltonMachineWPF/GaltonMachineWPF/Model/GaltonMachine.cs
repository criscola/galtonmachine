using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class GaltonMachine
    {

        public QuincunxGrid Grid { get; private set; }
        public Ball Ball { get; private set; }
        public int BallRow { get; set; }
        public int BallColumn { get; set; }
        public HistogramSet Results { get; set; }

        public GaltonMachine(int width)
        {
            Ball = new Ball();
            Grid = new QuincunxGrid(width);
            BallRow = 0;
            BallColumn = 0;
        }

        public void Reset()
        {
            Ball = new Ball();
            BallRow = 0;
            BallColumn = 0;
        }
    }
}
