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
        public Ball FallingBall { get; private set; }
        public ObservableCollection<Ball> Sticks { get; private set; }
        public int SimulationSize
        {
            get { return simulationSize; }
            set
            {
                simulationSize = value;
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public GaltonSimulation()
        {

        }

        public GaltonSimulation(int simulationSize, Size gDeviceSize, Ball fallingBall)
        {
            SimulationSize = simulationSize;
            GDeviceSize = gDeviceSize;
            Sticks = new ObservableCollection<Ball>();
            FallingBall = fallingBall;
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

        #endregion

        #region ================== Metodi privati ==================

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
