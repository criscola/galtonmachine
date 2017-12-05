using GaltonMachine.Helper;
using GaltonMachine.Model;
using System.Threading;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GaltonMachine.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        #region ================== Costanti =================

        public int STICKS_DIAMETER { get { return 20; } }
        public string STICKS_COLOR { get { return "red"; } }
        public int BALL_DIAMETER { get { return 15; } }
        public int HISTOGRAM_WIDTH { get { return 20; } }
        public int CANVAS_WIDTH { get { return 440; } }
        public int CANVAS_HEIGHT { get { return 400; } }
        public int CANVAS_HOFFSET { get { return 50; } }
        public int CANVAS_VOFFSET { get { return 50; } }
        public int DEFAULT_SIMULATION_SIZE { get { return 5; } }
        public int DEFAULT_SIMULATION_LENGTH { get { return 50; } }
        public int DEFAULT_SIMULATION_SPEED { get { return 500; } }
        public int DEFAULT_HISTOGRAM_STEP { get { return 50; } }
        public int MIN_SIMULATION_SIZE { get { return 3; } }
        public int MIN_SIMULATION_LENGTH { get { return 1; } }
        public int MIN_SIMULATION_SPEED { get { return 20; } }
        public int MAX_SIMULATION_SIZE { get { return 12; } }
        public int MAX_SIMULATION_LENGTH { get { return 100; } }
        public int MAX_SIMULATION_SPEED { get { return 2000; } }

        #endregion

        #region ================== Attributi privati =================

        private volatile bool isSimulationRunning;
        private int iterationCount;
        private int simulationSpeed;
        private int simulationSize;
        private int simulationLength;
        private BitmapImage curve;
        private Thread animationThread;

        #endregion

        #region ================== Attributi pubblici ================

        public int CanvasWidth { get; private set; }
        public int CanvasHeight { get; private set; }
        public GaltonSimulation GaltonSim { get; set; }
        public DistributionChart DisChart { get; private set; }
        public CompositeCollection ChartItemsCollection { get; private set; }
        public CompositeCollection SimulationItemsCollection { get; private set; }

        #endregion

        #region ================== Getter & setter =================

        public int SimulationSpeed
        {
            get
            {
                return simulationSpeed;
            }
            set
            {
                simulationSpeed = value;
                OnPropertyChanged(() => SimulationSpeed);
            }
        }
        public int SimulationSize
        {
            get
            {
                return simulationSize;
            }
            set
            {
                simulationSize = value;
                /*
                // Cambiando SimulationSize di GaltonSim, essa verrà propagata per il resto della simulazione
                if (GaltonSim != null) GaltonSim.SimulationSize = value;
                

                GenerateSticks();*/
                //GenerateChart();
                // TODO: Adattare questo codice alla nuova architettura
                OnPropertyChanged(() => SimulationSize);
                //if (model != null) GenerateFirstBall();
            }
        }
        public int SimulationLength
        {
            get
            {
                return simulationLength;
            }
            set
            {
                simulationLength = value;
                OnPropertyChanged(() => SimulationLength);
            }
        }
        public int IterationCount
        {
            get
            {
                return iterationCount;
            }
            private set
            {
                iterationCount = value;
                OnPropertyChanged(() => IterationCount);
            }
        }
        public bool IsSimulationRunning
        {
            get
            {
                return isSimulationRunning;
            }
            set
            {
                isSimulationRunning = value;
                OnPropertyChanged(() => IsSimulationRunning);
            }
        }

        #endregion

        #region ================== Delegati=================


        #endregion

        #region ================== Costruttori =================

        public GaltonMachineViewModel()
        {
            // Inizializzazione valori di default
            CanvasWidth = CANVAS_WIDTH;
            CanvasHeight = CANVAS_HEIGHT;

            SimulationSize = DEFAULT_SIMULATION_SIZE;
            SimulationSpeed = DEFAULT_SIMULATION_SPEED;
            SimulationLength = DEFAULT_SIMULATION_LENGTH;

            // Inizializzazione model/proprietà vm
            GaltonSim = new GaltonSimulation(SimulationSize, new System.Drawing.Size(CanvasWidth, CanvasHeight));

            SimulationItemsCollection = new CompositeCollection();
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.Sticks });
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.FallingBalls });

            ChartItemsCollection = new CompositeCollection();
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Histograms });
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Labels });

            GenerateSticks();

            //GenerateChart();

            // TODO: Adattare pure questo
            //GenerateFirstBall();

            //RegisterCommands();
        }

        #endregion

        #region ================== Metodi pubblici =================
        #endregion

        #region ================== Metodi privati ==================


        private void GenerateSticks()
        {
            GaltonSim.ClearSticks();

            // Larghezza e altezza canvas 
            double cw = CanvasWidth - CANVAS_HOFFSET;
            double ch = CanvasHeight - CANVAS_VOFFSET;

            // Distanza fra le stecche
            double dx = (cw - STICKS_DIAMETER) / (GaltonSim.SimulationSize - 1) / 2;
            double dy = (ch - STICKS_DIAMETER) / (GaltonSim.SimulationSize - 1);

            // Coordinate x y delle stecche iniziali
            double x = CanvasWidth / 2 - STICKS_DIAMETER;
            double y = 0;

            int column = 0;

            for (int i = 0; i < GaltonSim.SimulationSize; i++)
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
                        x = dx * (GaltonSim.SimulationSize - i - 1);
                    }

                    GaltonSim.SetStick(i, column, new Stick(x + (CANVAS_HOFFSET / 2), y + (CANVAS_VOFFSET / 2), STICKS_DIAMETER, STICKS_COLOR));
                    column++;
                }
                y += dy;
            }
        }

        #endregion

        #region ================== Metodi dei delegati =================

        #endregion
    }
}
