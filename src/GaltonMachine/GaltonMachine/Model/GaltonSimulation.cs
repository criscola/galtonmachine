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
        private QuincunxGrid quincunxGrid;
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
                FallingBalls = value;
            }
        }
        public int SimulationSize
        {
            get { return simulationSize; }
            set
            {
                // Aggiorna i valori delle classi gestite a cascata
                simulationSize = value;
                quincunxGrid.Size = value;
                // E' necessario riprendere le stecche dalla quincunxGrid perchè la grid è cambiata
                Sticks = GetSticks();
                GenerateSimulationInstances();
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

            GenerateSimulationInstances();
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

        public void ClearSticks()
        {
            Sticks.Clear();
        }

        public void SetStick(int row, int column, Stick s)
        {
            quincunxGrid.SetStick(row, column, s);
        }

        #endregion

        #region ================== Metodi privati ==================

        private void GenerateSimulationInstances()
        {
            quincunxGrid = new QuincunxGrid(SimulationSize);
            fallingBalls = new ObservableCollection<Ball>();
        }

        private ObservableCollection<Stick> GetSticks()
        {
            if (quincunxGrid == null) return null;
            ObservableCollection<Stick> s = new ObservableCollection<Stick>();
            for (int i = 0; i < quincunxGrid.Size; i++)
            {
                for (int j = 0; j < quincunxGrid.GetRowSize(i); j++)
                {
                    s.Add(quincunxGrid.GetStick(i, j));
                }
            }
            return s;
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
