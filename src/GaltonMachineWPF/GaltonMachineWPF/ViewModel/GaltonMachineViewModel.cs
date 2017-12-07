using GaltonMachineWPF.Model;
using GaltonMachineWPF.Helpers;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Windows.Threading;
using System.Drawing;

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        #region ================== Costanti =================

        public const int STICKS_DIAMETER = 20;
        public const string STICKS_COLOR = "red";
        public const string BALL_COLOR = "blue";
        public const int BALL_DIAMETER = 15;
        public const int HISTOGRAM_WIDTH = 20;

        public const int CANVAS_WIDTH = 440;
        public const int CANVAS_HEIGHT = 400;
        public const int CANVAS_HOFFSET = 50;
        public const int CANVAS_VOFFSET = 50;

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

        #region ================== Attributi & proprietà =================

        private GaltonMachine model;
        private volatile bool isSimulationRunning;
        private int iterationCount;
        private int simulationSpeed;
        private int simulationSize;
        private int simulationLength;
        private BitmapImage curve;

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
                    model.HistogramChart.Size = value;
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
        public ObservableCollection<Ball> FallingBallsList { get; private set; }
        public ObservableCollection<Histogram> HistogramsList { get; private set; }
        public ObservableCollection<ChartLabel> HistogramsLabels { get; private set; }
        public CompositeCollection ChartItemsCollection { get; private set; }
        public CompositeCollection SimulationItemsCollection { get; private set; }
        public BitmapImage Curve
        {
            get
            {
                return curve;
            }
            private set
            {
                curve = value;
                OnPropertyChanged(() => Curve);
            }
        }
        public string CurveDimensions { get { return "0, 0, " + CanvasWidth + ", " + CanvasHeight; } }
        public double BallDiameter { get; private set; }
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

            // Inizializzazione model/proprietà vm
            model = new GaltonMachine(SimulationSize, new System.Drawing.Size(CanvasWidth, CanvasHeight));

            SticksList = new ObservableCollection<Ball>();
            FallingBallsList = new ObservableCollection<Ball>();
            HistogramsList = new ObservableCollection<Histogram>();
            HistogramsLabels = new ObservableCollection<ChartLabel>();

            SimulationItemsCollection = new CompositeCollection();
            SimulationItemsCollection.Add(new CollectionContainer { Collection = SticksList });
            SimulationItemsCollection.Add(new CollectionContainer { Collection = FallingBallsList });

            ChartItemsCollection = new CompositeCollection();
            ChartItemsCollection.Add(new CollectionContainer { Collection = HistogramsList });
            ChartItemsCollection.Add(new CollectionContainer { Collection = HistogramsLabels });

            GenerateSticks();
            GenerateChart();
            
            // Aggiunta della pallina che cade alla lista di elementi da renderizzare
            BallDiameter = BALL_DIAMETER;

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
                // Genera la prima pallina che cade
                Cell newCell = new Cell(0, 0, new Ball(BALL_DIAMETER, BALL_COLOR));
                PlaceBallOnTopStick(newCell);
                model.FallingBallCells.Add(newCell);
                Application.Current.Dispatcher.Invoke(() => FallingBallsList.Add(newCell.Content));

                Thread.Sleep(SimulationSpeed);

                // Loop dei cicli della simulazione
                for (int i = 0; i < SimulationLength - 1; i++)
                {
                    // Loop delle palline che cadono
                    for (int j = 0; j < model.FallingBallCells.Count; j++)
                    {
                        Cell currentCell  = model.FallingBallCells.ElementAt(j);
                            
                        // Fa rimbalzare la pallina se la pallina non cade fuori dalla riga
                        if (currentCell.Content.Bounce() && currentCell.Column < model.Grid.GetRowSize(j))
                        {
                            Console.WriteLine("BALL at column-row: {0}-{1} max row: {2}",
                               currentCell.Column, currentCell.Row, model.Grid.GetRowSize(j));
                            currentCell.Column++;
                        }
                        // Se la pallina non è all'ultima riga, falla scendere, altrimenti rimuovila e aggiorna il grafico
                        if (currentCell.Row < model.Grid.Size - 1)
                        {
                            currentCell.Row++;
                        }
                        else
                        {
                            // Incrementa di 1 il valore dell'istogramma e modifica l'altezza di conseguenza
                            Histogram currentHistogram = HistogramsList.ElementAt(currentCell.Column);
                            currentHistogram.Value++;

                            // Aggiorna la curva
                            model.HistogramChart.Curve.UpdateData(currentCell.Column, currentHistogram.Value);
                            model.HistogramChart.Curve.Image?.Freeze();
                            Dispatcher.CurrentDispatcher.Invoke(() => Curve = model.HistogramChart.Curve.Image);

                            // Prende l'istogramma con valore più grande
                            Histogram maxHistogram = HistogramsList.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2);

                            // Aggiorna altezza degli istogrammi
                            for (int k = 0; k < HistogramsList.Count; k++)
                            {
                                Histogram h = HistogramsList.ElementAt(k);
                                int value = h.Value;
                                float perc = value / (float)maxHistogram.Value;
                                double barHeight = Math.Round(perc * (CanvasHeight - CANVAS_VOFFSET));
                                h.Height = barHeight;
                                h.Y = CanvasHeight - barHeight - (CANVAS_VOFFSET / 2);
                            }

                            // Aggiorna la label dell'istogramma
                            HistogramsLabels.ElementAt(currentCell.Column).Text = currentHistogram.Value.ToString();

                            model.FallingBallCells.Remove(currentCell);
                            Application.Current.Dispatcher.Invoke(() => this.FallingBallsList.Remove(currentCell.Content));
                        }
                        
                        PlaceBallOnStick(currentCell, model.Grid.GetCell(currentCell));
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.FallingBallsList.Clear();
                            foreach (Cell cell in model.FallingBallCells)
                            {
                                FallingBallsList.Add(cell.Content);
                            }
                        });
                    }
                    CurrentIteration++;

                    model.FallingBallCells.Add(new Cell(0, 0, new Ball(BALL_DIAMETER, BALL_COLOR)));

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

                // Larghezza e altezza canvas 
                double cw = CanvasWidth - CANVAS_HOFFSET;
                double ch = CanvasHeight - CANVAS_VOFFSET;

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
                        // Se non è la prima stecca
                        if (j != 0)
                        {
                            x += dx * 2;
                            
                        }
                        else
                        {
                            x = dx * (n - i - 1);
                        }

                        SticksList.Add(new Ball(x + (CANVAS_HOFFSET / 2), y + (CANVAS_VOFFSET / 2), STICKS_DIAMETER, STICKS_COLOR));
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
                HistogramsLabels.Clear();

                // Larghezza e altezza canvas 
                double cw = CanvasWidth;
                double ch = CanvasHeight;

                // Conteggio degli istogrammi
                double n = SimulationSize;

                // Distanza fra gli istogrammi
                double dx = (cw - (HISTOGRAM_WIDTH * n)) / (n + 1);

                // Coordinate iniziali x y degli istogrammi 
                double x = dx;
                double y = SticksList.Last().Y + STICKS_DIAMETER;

                // Crea i nuovi istogrammi
                for (int i = 0; i < n; i++)
                {
                    Histogram currentHistogram = new Histogram(x, y, HISTOGRAM_WIDTH, 0);
                    HistogramsList.Add(currentHistogram);
                    HistogramsLabels.Add(new ChartLabel(x, y, HISTOGRAM_WIDTH, "0"));

                    x += dx + HISTOGRAM_WIDTH;
                }
                // Genera la curva normale
                model.HistogramChart.Curve = new BellCurve(SimulationSize, new System.Drawing.Size(CanvasWidth, CanvasHeight));
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

        #endregion

        #region ================== Metodi helper ===================

        private void PlaceBallOnStick(Cell cell, Ball stick)
        {
            cell.Content.X = stick.X + stick.Radius / 2 - BallDiameter / 2;
            cell.Content.Y = stick.Y - BallDiameter - 0.5;
        }

        private void PlaceBallOnTopStick(Cell cell)
        {
            PlaceBallOnStick(cell, model.Grid.GetCell(0, 0));
        }

        private void GenerateFirstBall()
        {
            if (model.FallingBallCells.Count == 0)
            {
                // Genera la prima pallina che cade
                Cell newCell = new Cell(0, 0, new Ball(BALL_DIAMETER, BALL_COLOR));
                PlaceBallOnTopStick(newCell);
                model.FallingBallCells.Add(newCell);
                FallingBallsList.Add(newCell.Content);
            }
            else
            {
                model.FallingBallCells.Clear();
            }
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
            GenerateFirstBall();
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