using System.Collections.Generic;

namespace GaltonMachineWPF.Model
{
    public class GaltonMachine
    {
        public QuincunxGrid Grid { get; private set; }
        public List<Cell> FallingBallCells { get; private set; }
        public HistogramChart HistogramChart { get; set; }

        public GaltonMachine(int width, System.Drawing.Size gDeviceSize)
        {
            FallingBallCells = new List<Cell>();
            Grid = new QuincunxGrid(width);
            HistogramChart = new HistogramChart(width, gDeviceSize);
        }

        public void Reset()
        {
            for (int i = 0; i < HistogramChart.Size; i++)
            {
                HistogramChart.SetValue(i, 0);
            }
        }
    }
}
