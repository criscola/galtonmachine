﻿using System;
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
        public HistogramChart HistogramChart { get; set; }
        public int[] ResultSet { get; set; }

        public GaltonMachine(int width)
        {
            Ball = new Ball();
            Grid = new QuincunxGrid(width);
            BallRow = 0;
            BallColumn = 0;
            HistogramChart = new HistogramChart(width);
        }

        public void Reset()
        {
            Ball = new Ball();
            BallRow = 0;
            BallColumn = 0;
            for (int i = 0; i < HistogramChart.Size; i++)
            {
                HistogramChart.SetValue(i, 0);
            }
        }
    }
}
