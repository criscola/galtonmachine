using GaltonMachine.Helper;
using GaltonMachine.Model;
using System;
using System.Drawing;
using System.Threading;
using System.Windows;
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
        public string CurveDimensions { get { return "0, 0, " + CanvasWidth + ", " + CanvasHeight; } }
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
            GaltonSim = new GaltonSimulation(SimulationSize, gDeviceSize);
            DisChart = new DistributionChart(SimulationSize, gDeviceSize, CANVAS_VOFFSET);

            SimulationItemsCollection = new CompositeCollection();
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.Sticks });
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.FallingBalls });

            ChartItemsCollection = new CompositeCollection();
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Histograms });
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Labels });

            GenerateSticks();

            // TODO: Adattare pure questo
            //GenerateFirstBall();

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
                // Loop dei cicli della simulazione
                for (int i = 0; i < SimulationLength - 1; i++)
                {
                    // Genera la prima pallina che cade
                    Ball firstFallingBall = new Ball(0, 0, 0, 0, BALL_DIAMETER);
                    PlaceBallOnTopStick(firstFallingBall);
                    Application.Current.Dispatcher.Invoke(() => GaltonSim.FallingBalls.Add(firstFallingBall));
                    Thread.Sleep(SimulationSpeed);

                    // Loop delle palline che cadono
                    for (int j = 0; j < GaltonSim.FallingBalls.Count; j++)
                    {
                        Ball currentBall = GaltonSim.FallingBalls[j];

                        // Fa rimbalzare la pallina se la pallina non cade fuori dalla riga
                        Random rnd = new Random();
                        if (rnd.Next(0 , 1) == 0 && currentBall.Column < j + 1)
                        {
                            currentBall.Column++;
                        }
                        // Se la pallina non è all'ultima riga, falla scendere, altrimenti rimuovila e aggiorna il grafico
                        if (currentBall.Row < SimulationSize - 1)
                        {
                            currentBall.Row++;
                            PlaceBallOnStick(GaltonSim.GetStick(currentBall.Row, currentBall.Column), currentBall);                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => GaltonSim.FallingBalls.Remove(currentBall));

                            // Incrementa di 1 l'istogramma alla stessa posizione della pallina corrente
                            DisChart.IncrementValue(currentBall.Column);

                            // Aggiorna la curva
                            if (DisChart.GetDataCount() > 2)
                            { 
                                Curve = DisChart.GetCurveImage();
                            }
                        }
                    }

                    /*Ball nextBall = new Ball(0, 0, 0, 0, BALL_DIAMETER);
                    PlaceBallOnTopStick(nextBall);
                    Application.Current.Dispatcher.Invoke(() => GaltonSim.FallingBalls.Add(nextBall));*/
                    IterationCount++;
                    
                    //Thread.Sleep(SimulationSpeed);
                    
                }
                // Resetta la simulazione quando la pallina completa la simulazione
                //IsSimulationRunning = false;

                // Essendo un altro thread che processa la chiamata, è necessario
                // usare il Dispatcher per "passare" l'invocazione al thread della UI,
                // altrimenti Execute lancerebbe una InvalidOperationException

                //Application.Current.Dispatcher.Invoke(() =>
                //{
                //    StopSimulationCommand.RaiseCanExecuteChanged();
                //    StartSimulationCommand.RaiseCanExecuteChanged();
                //});
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        }
        
        private void GenerateSticks()
        {
            GaltonSim.Sticks.Clear();
            DisChart.Reset();

            // Larghezza e altezza canvas 
            double cw = CanvasWidth - CANVAS_HOFFSET;
            double ch = CanvasHeight - CANVAS_VOFFSET;

            // Distanza fra le stecche
            double dx = (cw - STICKS_DIAMETER) / (GaltonSim.SimulationSize - 1) / 2;
            double dy = (ch - STICKS_DIAMETER) / (GaltonSim.SimulationSize - 1);

            // Coordinate x y delle stecche iniziali
            double x = CanvasWidth / 2 - STICKS_DIAMETER;
            double y = 0;

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

                    GaltonSim.Sticks.Add(new Stick(x + (CANVAS_HOFFSET / 2), y + (CANVAS_VOFFSET / 2), STICKS_DIAMETER, STICKS_COLOR));
                }
                y += dy;
            }
        }

        private void PlaceBallOnStick(Stick stick, Ball ball)
        {
            ball.X = stick.X + stick.Diameter / 2 - ball.Diameter / 2;
            ball.Y = stick.Y - ball.Diameter - 0.5;
        }

        private void PlaceBallOnTopStick(Ball ball)
        {
            PlaceBallOnStick(GaltonSim.Sticks[0], ball);
        }

        private void StartSimulation()
        {
            IsSimulationRunning = true;

            //model.Reset();
            //// Generazione del grafico
            //GenerateChart();

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
            DisChart.Reset();

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
