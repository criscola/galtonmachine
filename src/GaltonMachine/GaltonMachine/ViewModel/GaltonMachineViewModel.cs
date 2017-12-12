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
            GaltonSim = new GaltonSimulation(SimulationSize, gDeviceSize, new Ball(BALL_DIAMETER, BALL_COLOR));

            DisChart = new DistributionChart(SimulationSize, gDeviceSize, CANVAS_VOFFSET);

            SimulationItemsCollection = new CompositeCollection();
            SimulationItemsCollection.Add(new CollectionContainer { Collection = GaltonSim.Sticks });

            ChartItemsCollection = new CompositeCollection();
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Histograms });
            ChartItemsCollection.Add(new CollectionContainer { Collection = DisChart.Labels });

            GenerateSticks();
            PlaceBallOnTopStick();

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
                    FallingBall.Reset();
                    PlaceBallOnTopStick();
                    
                    for (int j = 0; j < SimulationSize - 1; j++)
                    {
                        Thread.Sleep(SimulationSpeed);

                        FallingBall.Row++;

                        Random rnd = new Random();
                        if (rnd.Next(0, 2) == 0 && FallingBall.Column < j + 1)
                        {
                            FallingBall.Column++;
                        }
                        Ball currentStick = GaltonSim.GetStick(FallingBall.Row, FallingBall.Column);
                        PlaceBallOnStick(currentStick);
                    }
                    /*
                    // Incrementa di 1 il valore dell'istogramma e modifica l'altezza di conseguenza
                    Histogram currentHistogram = HistogramsList.ElementAt(model.BallColumn);
                    currentHistogram.Value++;

                    // Aggiorna la curva
                    model.HistogramChart.Curve.UpdateData(model.BallColumn, (float)currentHistogram.Value);
                    Console.WriteLine("Aggiornati i dati all'indice {0} con valore {1}", model.BallColumn, currentHistogram.Value);
                    model.HistogramChart.Curve.Image?.Freeze();
                    Dispatcher.CurrentDispatcher.Invoke(() => Curve = model.HistogramChart.Curve.Image);

                    // Prende l'istogramma con valore più grande
                    Histogram maxHistogram = HistogramsList.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2);

                    // Aggiorna altezza degli istogrammi
                    for (int j = 0; j < HistogramsList.Count; j++)
                    {
                        Histogram h = HistogramsList.ElementAt(j);
                        int value = h.Value;
                        float perc = value / (float)maxHistogram.Value;
                        double barHeight = Math.Round(perc * (CanvasHeight - CANVAS_VOFFSET));
                        h.Height = barHeight;
                        h.Y = CanvasHeight - barHeight - (CANVAS_VOFFSET / 2);
                    }

                    // Aggiorna la label dell'istogramma
                    HistogramsLabels.ElementAt(model.BallColumn).Text = currentHistogram.Value.ToString();
                    */
                    IterationCount++;

                    Thread.Sleep(SimulationSpeed);
                }
                // Resetta la simulazione quando la pallina completa la simulazione
                IsSimulationRunning = false;

                // Essendo un altro thread che processa la chiamata, è necessario
                // usare il Dispatcher per "passare" l'invocazione al thread della UI,
                // altrimenti Execute lancerebbe una InvalidOperationException

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

                    GaltonSim.Sticks.Add(new Ball(i, j, x + (CANVAS_HOFFSET / 2), y + (CANVAS_VOFFSET / 2), STICKS_DIAMETER, STICKS_COLOR));
                }
                y += dy;
            }
        }

        private void PlaceBallOnStick(Ball stick)
        {
            FallingBall.X = stick.X + stick.Diameter / 2 - FallingBall.Diameter / 2;
            FallingBall.Y = stick.Y - FallingBall.Diameter - 0.5;
        }

        private void PlaceBallOnTopStick()
        {
            PlaceBallOnStick(GaltonSim.Sticks[0]);
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
