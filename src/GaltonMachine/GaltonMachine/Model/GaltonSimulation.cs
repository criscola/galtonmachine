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

        private ObservableCollection<Ball> fallingBalls;
        private int simulationSize;

        public Size GDeviceSize { get; set; }
        public ObservableCollection<Stick> Sticks { get; private set; }
        public ObservableCollection<Ball> FallingBalls
        {
            get
            {
                return fallingBalls;
            }
            private set
            {
                fallingBalls = value;
            }
        }
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

        public GaltonSimulation(int simulationSize, Size gDeviceSize)
        {
            SimulationSize = simulationSize;
            GDeviceSize = gDeviceSize;
            Sticks = new ObservableCollection<Stick>();
            FallingBalls = new ObservableCollection<Ball>();
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void StartSimulation()
        {

        }

        public void StopSimulation()
        {

        }

        public void PauseSimulation()
        {

        }

        public Stick GetStick(int row, int column)
        {
            return Sticks[2 * row + column];   
        }

        #endregion

        #region ================== Metodi privati ==================

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
