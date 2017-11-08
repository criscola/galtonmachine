using GaltonMachineWPF.Model;
using GaltonMachineWPF.Helpers;
using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.Windows.Data;

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
        public const int CANVAS_HOFFSET = 50;
        public const int CANVAS_VOFFSET = 50;

        public int DEFAULT_SIMULATION_SIZE { get { return 7; } }
        public int DEFAULT_SIMULATION_LENGTH { get { return 50; } }
        public int DEFAULT_SIMULATION_SPEED { get { return 500; } }
        public int DEFAULT_HISTOGRAM_STEP { get { return 50; } }
        public int MIN_SIMULATION_SIZE { get { return 3; } }
        public int MIN_SIMULATION_LENGTH { get { return 1; } }
        public int MIN_SIMULATION_SPEED { get { return 20; } }
        public int MAX_SIMULATION_SIZE { get { return 12; } }
        public int MAX_SIMULATION_LENGTH { get { return 100; } }
        public int MAX_SIMULATION_SPEED { get { return 2000; } }
        public FontFamily DEFAULT_HLABELS_FONTFAMILY { get { return new FontFamily("Arial"); } }
        public FontStyle DEFAULT_HLABELS_FONTSTYLE { get { return FontStyles.Normal; } }
        public FontWeight DEFAULT_HLABELS_FONTWEIGHT { get { return FontWeights.Bold; } }
        public FontStretch DEFAULT_HLABELS_FONTSTRETCH { get { return FontStretches.Normal; } }
        public double DEFAULT_HLABELS_FONTSIZE { get { return 13; } }

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
        public ObservableCollection<Label> HistogramsLabels { get; private set; }
        public CompositeCollection ChartItemsCollection { get; private set; }
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
            ChartItemsCollection = new CompositeCollection();

            ChartItemsCollection.Add(HistogramsList);
            ChartItemsCollection.Add(HistogramsLabels);
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

                    Histogram maxHistogram = HistogramsList.Aggregate((i1, i2) => i1.Value > i2.Value ? i1 : i2);

                    for (int j = 0; j < HistogramsList.Count; j++)
                    {
                        Histogram h = HistogramsList.ElementAt(j);
                        int value = h.Value;
                        float perc = value / (float)maxHistogram.Value;
                        double barHeight = Math.Round(perc * (CanvasHeight - CANVAS_VOFFSET));
                        h.Height = barHeight;
                        h.Y = CanvasHeight - barHeight - (CANVAS_VOFFSET / 2);
                    }
                    
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
                        // Se è la prima stecca   
                        if (j == 0)
                        {
                            x = dx * (n - i - 1);
                        }
                        else
                        {
                            x += dx * 2;
                        }

                        SticksList.Add(new Ball(x + (CANVAS_HOFFSET / 2), y + (CANVAS_VOFFSET/ 2), STICKS_DIAMETER));
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
                    Histogram currentHistogram = new Histogram(x, y, HISTOGRAM_WIDTH, 0);
                    //currentHistogram.ValueX = currentHistogram.X;
                    //currentHistogram.ValueY = currentHistogram.Y + currentHistogram.Height;
                    
                    HistogramsList.Add(currentHistogram);
                }

                // Inserisce gli elementi di HistogramList nel model.HistogramChart
                for (int i = 0; i < model.HistogramChart.Size; i++)
                {
                    model.HistogramChart.SetHistogram(i, HistogramsList.ElementAt(i));
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

        #endregion

        #region ================== Metodi helper ===================


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

        private Size MeasureLabelsUISize(string str)
        {
            var formattedText = new FormattedText(
                str,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(DEFAULT_HLABELS_FONTFAMILY, DEFAULT_HLABELS_FONTSTYLE, DEFAULT_HLABELS_FONTWEIGHT, DEFAULT_HLABELS_FONTSTRETCH), 
                DEFAULT_HLABELS_FONTSIZE,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
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