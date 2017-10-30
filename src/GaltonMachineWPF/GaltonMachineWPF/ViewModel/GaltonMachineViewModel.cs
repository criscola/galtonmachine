using GaltonMachineWPF;
using GaltonMachineWPF.Model;
using GaltonMachineWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;
using GaltonMachineWPF.View;
using System.Windows;
using System.Threading;
using System.Collections.Specialized;

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        #region ================== Costanti =================

        public const int STICKS_DIAMETER = 20;
        public const int BALL_DIAMETER = 15;
        public const int HISTOGRAM_WIDTH = 20;

        public const int CANVAS_WIDTH = 440;
        public const int CANVAS_HEIGHT = 400;

        public int DEFAULT_SIMULATION_SIZE { get { return 8; } }
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

        #region ================== Attributi & proprietà =================

        private GaltonMachine model;
        private volatile bool isSimulationRunning;
        private int simulationSpeed;
        private int simulationSize;
        private int simulationLength;
        private int iterationCount;

        private Thread animationThread;

        public int CanvasWidth { get; private set; }
        public int CanvasHeight { get; private set; }
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
                if (model != null)
                {
                    model.Grid.Size = value;
                }
                GenerateSticks();
                GenerateChart();
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
        public int IterationCount { get; set; }
        public int CurrentIteration
        {
            get
            {
                return iterationCount;
            }
            private set
            {
                iterationCount = value;
                OnPropertyChanged(() => CurrentIteration);
            }
        }
        public ObservableCollection<Ball> SticksList { get; private set; }
        public ObservableCollection<Histogram> HistogramsList { get; private set; }
        public double BallDiameter { get; private set; }
        public double BallX
        {
            get
            {
                return model.Ball.X;
            }
            private set
            {
                model.Ball.X = value;
                OnPropertyChanged(() => BallX);
            }
        }
        public double BallY
        {
            get
            {
                return model.Ball.Y;
            }
            private set
            {
                model.Ball.Y = value;
                OnPropertyChanged(() => BallY);
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
        public bool IsSimulationNotRunning { get { return !isSimulationRunning; } }
        //public bool IsSliderSimulationLengthEnabled { get; set; }
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

            //IsSliderSimulationLengthEnabled = true;
            // Inizializzazione model/proprietà vm
            model = new GaltonMachine(SimulationSize);

            SticksList = new ObservableCollection<Ball>();
            HistogramsList = new ObservableCollection<Histogram>();
            GenerateSticks();

            // Aggiunta della pallina che cade alla lista di elementi da renderizzare
            BallDiameter = BALL_DIAMETER;

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
                    // Piazza la pallina sulla prima stecca
                    PlaceBallOnTopStick();

                    for (int j = 0; j < model.Grid.Size - 1; j++)
                    {
                        IterationCount++;

                        Thread.Sleep(SimulationSpeed);
                        model.BallRow++;

                        // Fa rimbalzare la pallina se la pallina non cade fuori dalla riga
                        if (model.Ball.Bounce() && model.BallColumn < model.Grid.GetRowSize(j))
                        {
                            model.BallColumn++;
                        }

                        // Riceve la stecca su cui posizionare la pallina
                        PlaceBallOnStick(model.Grid.GetCell(model.BallRow, model.BallColumn));
                    }
                    // Incrementa di 1 il valore dell'istogramma e modifica l'altezza di conseguenza
                    Histogram currentHistogram = HistogramsList.ElementAt(model.BallColumn);
                    currentHistogram.Value++;
                    currentHistogram.Y -= DEFAULT_HISTOGRAM_STEP;
                    currentHistogram.Height += DEFAULT_HISTOGRAM_STEP;
                    // GenerateChart();
                    CurrentIteration++;

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
            if (SticksList != null)
            {
                SticksList.Clear();

                // Offset orizzontale e verticale
                double hoffset = 50;
                double voffset = 50;

                // Larghezza e altezza canvas 
                double cw = CanvasWidth - hoffset;
                double ch = CanvasHeight - voffset;

                // Grandezza della base
                double n = model.Grid.Size;

                // Distanza fra le stecche
                double dx = (cw - STICKS_DIAMETER) / (n - 1) / 2;
                double dy = (ch - STICKS_DIAMETER) / (n - 1);

                // Coordinate x y delle stecche iniziali
                double x = CanvasWidth / 2 - STICKS_DIAMETER;
                double y = 0;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < i + 1; j++)
                    {
                        // Se è la prima stecca   
                        if (j == 0)
                        {
                            x = dx * (n - i - 1);
                        }
                        else
                        {
                            x += dx * 2;
                        }

                        SticksList.Add(new Ball(x + (hoffset / 2), y + (voffset / 2), STICKS_DIAMETER));
                    }
                    y += dy;
                }

                // Inserisce gli elementi di SticksList nel model.Grid
                int index = 0;
                for (int i = 0; i < model.Grid.Size; i++)
                {
                    for (int j = 0; j < model.Grid.GetRowSize(i); j++)
                    {
                        model.Grid.SetCell(i, j, SticksList.ElementAt(index));
                        index++;
                    }
                }
            }
        }

        private void GenerateChart()
        {
            if (HistogramsList != null)
            {
                HistogramsList.Clear();

                // Offset orizzontale e verticale
                double hoffset = 50;

                // Larghezza e altezza canvas 
                double cw = CanvasWidth;
                double ch = CanvasHeight;

                // Conteggio degli istogrammi
                double n = SimulationSize;

                // Distanza fra gli istogrammi
                double dx = ((cw - hoffset * 2) - (HISTOGRAM_WIDTH * n)) / (n - 1);

                // Coordinate x y degli istogrammi iniziali
                double x = hoffset;
                // Garantisce che il grafico sia allineato alla base delle stecche
                double y = SticksList.Last().Y + STICKS_DIAMETER;

                HistogramsList.Add(new Histogram(x, y, HISTOGRAM_WIDTH, 0));

                // Crea i nuovi istogrammi
                for (int i = 0; i < n - 1; i++)
                {
                    x += dx + HISTOGRAM_WIDTH;
                    HistogramsList.Add(new Histogram(x, y, HISTOGRAM_WIDTH, 0));
                }           
            }
        }

        private void StartSimulation()
        {
            IsSimulationRunning = true;

            model.Reset();
            // Generazione del grafico
            GenerateChart();

            // Istanza thread pallina che cade animata
            animationThread = new Thread(new ThreadStart(AnimateFallingBall));
            // Necessario per far chiudere la thread quando si chiude l'applicazione
            animationThread.IsBackground = true;
            animationThread.Start();

            PlaceBallOnTopStick();
            CurrentIteration = 1;

            StartSimulationCommand.RaiseCanExecuteChanged();
            StopSimulationCommand.RaiseCanExecuteChanged();
        }

        private void StopSimulation()
        {
            IsSimulationRunning = false;
            CurrentIteration = 0;

            StopSimulationCommand.RaiseCanExecuteChanged();
            StartSimulationCommand.RaiseCanExecuteChanged();

            animationThread?.Interrupt();
        }

        private void PlaceBallOnStick(Ball stick)
        {
            BallX = stick.X + stick.Radius / 2 - BallDiameter / 2;
            BallY = stick.Y - BallDiameter - 0.5;
        }

        private void PlaceBallOnTopStick()
        {
            model.BallRow = 0;
            model.BallColumn = 0;
            PlaceBallOnStick(model.Grid.GetCell(0, 0));
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
            PlaceBallOnTopStick();
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