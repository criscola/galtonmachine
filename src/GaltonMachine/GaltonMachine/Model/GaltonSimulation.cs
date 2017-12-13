using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace GaltonMachine.Model
{
    public class GaltonSimulation
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private int simulationSize;

        public Size GDeviceSize { get; set; }
        public double HorizontalOffset { get; set; }
        public double VerticalOffset { get; set; }
        public double SticksDiameter { get; set; }
        public string SticksColor { get; set; }
        public Ball FallingBall { get; private set; }
        public ObservableCollection<Ball> Sticks { get; private set; }

        public int SimulationSize
        {
            get { return simulationSize; }
            set
            {
                simulationSize = value;
                if (Sticks.Count > 0) PlaceBallOnTopStick();
                GenerateSticks();
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public GaltonSimulation()
        {

        }

        public GaltonSimulation(int simulationSize, Ball fallingBall, Size gDeviceSize, double hOffset, double vOffset,
            double sticksDiameter, string sticksColor)
        {
            GDeviceSize = gDeviceSize;
            Sticks = new ObservableCollection<Ball>();
            FallingBall = fallingBall;
            HorizontalOffset = hOffset;
            VerticalOffset = vOffset;
            SticksDiameter = sticksDiameter;
            SticksColor = sticksColor;
            SimulationSize = simulationSize;
        }

        #endregion

        #region ================== Metodi pubblici =================

        public Ball GetStick(int row, int column)
        {
            foreach (var Stick in Sticks)
            {
                if (Stick.Column == column && Stick.Row == row)
                {
                    return Stick;
                }
            }
            throw new ArgumentOutOfRangeException("row and/or column", "Row and/or column arguments are out of bounds!");
        }

        public void PlaceBallOnStick(Ball s)
        {
            FallingBall.X = s.X + s.Diameter / 2 - FallingBall.Diameter / 2;
            FallingBall.Y = s.Y - FallingBall.Diameter - 0.5;
        }

        public void PlaceBallOnTopStick()
        {
            PlaceBallOnStick(Sticks[0]);
        }

        private void GenerateSticks()
        {
            Sticks.Clear();

            // Larghezza e altezza canvas 
            double cw = GDeviceSize.Width - HorizontalOffset;
            double ch = GDeviceSize.Height - VerticalOffset;

            // Distanza fra le stecche
            double dx = (cw - SticksDiameter) / (SimulationSize - 1) / 2;
            double dy = (ch - SticksDiameter) / (SimulationSize - 1);

            // Coordinate x y delle stecche iniziali
            double x = GDeviceSize.Width / 2 - SticksDiameter;
            double y = 0;

            for (int i = 0; i < SimulationSize; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    // Se non è la prima stecca
                    if (j != 0)
                    {
                        x += dx * 2;

                    }
                    else
                    {
                        x = dx * (SimulationSize - i - 1);
                    }

                    Sticks.Add(new Ball(i, j, x + (HorizontalOffset / 2), y + (VerticalOffset / 2), SticksDiameter, SticksColor));
                }
                y += dy;
            }
        }

        #endregion

        #region ================== Metodi privati ==================

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
