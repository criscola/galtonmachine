using GaltonMachine.Helper;
using GaltonMachine.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GaltonMachine.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        #region ================== Costanti =================

        public int STICKS_DIAMETER { get { return 20; } }
        public string STICKS_COLOR { get { return "red"; } }
        public int BALL_DIAMETER { get { return 15; } }
        public string BALL_COLOR { get { return "black"; } }
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
        public string CurveDimensions { get { return "0, 0, " + CanvasWidth + ", " + CanvasHeight; } }
        public double CurveMean { get { if (DisChart != null) return Math.Round(DisChart.Mean, 3); else return 0; } }
        public double CurveVariance { get { if (DisChart != null) return Math.Round(DisChart.Variance, 3); else return 0; } }
        public double CurveStdDev { get { if (DisChart != null) return Math.Round(DisChart.StdDev, 3); else return 0; } }
        public GaltonSimulation GaltonSim { get; set; }
        public DistributionChart DisChart { get; private set; }
        public CompositeCollection ChartItemsCollection { get; private set; }
        public CompositeCollection SimulationItemsCollection { get; private set; }
        public Ball FallingBall { get { return GaltonSim.FallingBall; } }
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
                // Aggiorna a cascata la grandezza della simulazione
                if (GaltonSim != null) GaltonSim.SimulationSize = value;
                if (DisChart != null) DisChart.Size = value;
                OnPropertyChanged(() => SimulationSize);
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
                OnPropertyChanged(() => IsSimulationNotRunning);
            }
        }

        public bool IsSimulationNotRunning
        {
            get
            {
                return !isSimulationRunning;
            }
        }

        public BitmapImage Curve
        {
            get
            {
                return curve;
            }
            set
            {
                curve = value;
                OnPropertyChanged(() => Curve);
            }
        }

        #endregion

        #region ================== Delegati=================

        public IDelegateCommand StartSimulationCommand { get; protected set; }
        public IDelegateCommand StopSimulationCommand { get; protected set; }
        public IDelegateCommand ResetSimulationCommand { get; protected set; }
        public IDelegateCommand CloseApplicationCommand { get; protected set; }
        public IDelegateCommand AboutApplicationCommand { get; protected set; }

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

            System.Drawing.Size gDeviceSize = new System.Drawing.Size(CanvasWidth, CanvasHeight);

            // Inizializzazione model/proprietà vm
            GaltonSim = new GaltonSimulation(SimulationSize, 
                new Ball(BALL_DIAMETER, BALL_COLOR), 
                gDeviceSize, 
                CANVAS_HOFFSET,
                CANVAS_VOFFSET,
                STICKS_DIAMETER,
                STICKS_COLOR);

            DisChart = new DistributionChart(SimulationSize, 
                gDeviceSize, 
                HISTOGRAM_WIDTH, 
                GaltonSim.Sticks[GaltonSim.Sticks.Count - 1].Y);

            SimulationItemsCollection = new CompositeCollection();
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.Sticks });

            ChartItemsCollection = new CompositeCollection();
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Histograms });
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Labels });

            GaltonSim.PlaceBallOnTopStick();

            RegisterCommands();
           
        }

        #endregion

        #region ================== Metodi pubblici =================
        #endregion

        #region ================== Metodi privati ==================
        
        private void AnimateFallingBall()
        {
            try
            {
                for (int i = 0; i < SimulationLength - 1; i++)
                {
                    // Piazza la pallina in cima
                    FallingBall.Reset();
                    GaltonSim.PlaceBallOnTopStick();

                    // Simulazione caduta pallina
                    for (int j = 0; j < SimulationSize - 1; j++)
                    {
                        Thread.Sleep(SimulationSpeed);

                        FallingBall.Row++;

                        Random rnd = new Random();
                        if (rnd.Next(0, 2) == 1 && FallingBall.Column < j + 1)
                        {
                            FallingBall.Column++;
                        }

                        GaltonSim.PlaceBallOnStick(GaltonSim.GetStick(FallingBall.Row, FallingBall.Column));

                    }
                    
                    // Aggiorna istogrammi e curva
                    DisChart.IncrementValue(FallingBall.Column);
                    DisChart.GetCurveImage()?.Freeze();
                    Application.Current.Dispatcher.Invoke(() => Curve = DisChart.GetCurveImage());

                    // Aggiorna le etichette
                    OnPropertyChanged(() => CurveMean);
                    OnPropertyChanged(() => CurveVariance);
                    OnPropertyChanged(() => CurveStdDev);
                    IterationCount++;

                    Thread.Sleep(SimulationSpeed);
                }
                // Resetta la simulazione quando la pallina completa la simulazione
                IsSimulationRunning = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    StopSimulationCommand.RaiseCanExecuteChanged();
                    StartSimulationCommand.RaiseCanExecuteChanged();
                });
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        }

        private void StartSimulation()
        {
            IsSimulationRunning = true;
            DisChart.Reset();

            // Istanza thread pallina che cade animata
            animationThread = new Thread(new ThreadStart(AnimateFallingBall));
            // Necessario per far chiudere la thread quando si chiude l'applicazione
            animationThread.IsBackground = true;
            animationThread.Start();

            IterationCount = 1;

            StartSimulationCommand.RaiseCanExecuteChanged();
            StopSimulationCommand.RaiseCanExecuteChanged();
        }

        private void StopSimulation()
        {
            IsSimulationRunning = false;
            IterationCount = 0;

            StopSimulationCommand.RaiseCanExecuteChanged();
            StartSimulationCommand.RaiseCanExecuteChanged();

            animationThread?.Interrupt();
        }

        #endregion

        #region ================== Metodi dei delegati =================

        private void RegisterCommands()
        {
            StartSimulationCommand = new DelegateCommand(OnStart, CanStart);
            StopSimulationCommand = new DelegateCommand(OnStop, CanStop);
            ResetSimulationCommand = new DelegateCommand(OnReset);
            CloseApplicationCommand = new DelegateCommand(OnClose);
            AboutApplicationCommand = new DelegateCommand(OnAbout);
        }

        private void OnStart(object obj)
        {
            StartSimulation();
        }

        private bool CanStart(object obj)
        {
            return !IsSimulationRunning;
        }

        private void OnStop(object obj)
        {
            StopSimulation();
        }

        private void OnReset(object obj)
        {
            OnStop(obj);
            SimulationLength = DEFAULT_SIMULATION_LENGTH;
            SimulationSize = DEFAULT_SIMULATION_SIZE;
            SimulationSpeed = DEFAULT_SIMULATION_SPEED;
            Curve = null;
        }

        private void OnAbout(object obj)
        {
            MessageBox.Show("Creato da Cristiano Colangelo I4AC", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanStop(object obj)
        {
            return IsSimulationRunning;
        }

        private void OnClose(object obj)
        {
            IsSimulationRunning = false;
            animationThread?.Interrupt();
            animationThread?.Join();
            Application.Current.Shutdown();
        }

        #endregion
    }
}
