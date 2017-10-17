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

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        #region ================== Costanti =================

        public const int STICKS_DIAMETER = 20;
        public const int BALL_DIAMETER = 15;
        public const int ANIMATION_SPEED = 500;
        public const int SIMULATION_SIZE = 8;
        public const int CANVAS_WIDTH = 440;
        public const int CANVAS_HEIGHT = 400;
        public const int SIMULATION_LENGTH = 10;
        #endregion

        #region ================== Attributi & proprietà =================

        private GaltonMachine model;
        private bool simulationRunning;
        private Thread ballAnimationThread;

        public int CanvasWidth { get; private set; }
        public int CanvasHeight { get; private set; }

        public ObservableCollection<Ball> SticksList { get; private set; }
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

        public bool SimulationRunning
        {
            get
            {
                return simulationRunning;
            }
            set
            {
                simulationRunning = value;
                OnPropertyChanged(() => SimulationRunning);
            }
        }
        #endregion

        #region ================== Delegati=================

        public IDelegateCommand StartSimulationCommand { get; protected set; }
        public IDelegateCommand ResetSimulationCommand { get; protected set; }
        public IDelegateCommand CloseApplicationCommand { get; protected set; }
        public IDelegateCommand AboutApplicationCommand { get; protected set; }

        #endregion

        #region ================== Costruttori =================

        public GaltonMachineViewModel()
        {
            CanvasWidth = CANVAS_WIDTH;
            CanvasHeight = CANVAS_HEIGHT;
            // Inizializzazione variabili/model
            model = new GaltonMachine(SIMULATION_SIZE);

            SticksList = new ObservableCollection<Ball>();
            GenerateSticks();

            // Aggiunta della pallina che cade alla lista di elementi da renderizzare
            BallDiameter = BALL_DIAMETER;

            PlaceBallOnTopStick();
            // Generazione del dataset dei risultati
            model.Results = new HistogramSet(SIMULATION_SIZE);

            RegisterCommands();
        }

        #endregion

        #region ================== Metodi pubblici =================

        #endregion

        #region ================== Metodi privati ==================

        private void AnimateFallingBall()
        {
            for (int i = 0; i < SIMULATION_LENGTH; i++) {
                // Piazza la pallina sulla prima stecca
                PlaceBallOnTopStick();

                for (int j = 0; j < model.Grid.GetSize() - 1; j++)
                {
                    Thread.Sleep(ANIMATION_SPEED);

                    model.BallRow++;

                    // Fa rimbalzare la pallina se la pallina non cade fuori dalla riga
                    if (model.Ball.Bounce() && model.BallColumn < model.Grid.GetRowSize(j))
                    {
                        model.BallColumn++;
                    }

                    // Riceve la stecca su cui posizionare la pallina
                    PlaceBallOnStick(model.Grid.GetCell(model.BallRow, model.BallColumn));
                }
                Thread.Sleep(ANIMATION_SPEED);
            }
        }
        private void GenerateSticks()
        {
            // Offset orizzontale e verticale
            double hoffset = 50;
            double voffset = 50;

            // Larghezza e altezza canvas 
            double cw = CanvasWidth - hoffset;
            double ch = CanvasHeight - voffset;

            // Grandezza della base
            double n = model.Grid.GetSize();

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
            for (int i = 0; i < model.Grid.GetSize(); i++)
            {
                for (int j = 0; j < model.Grid.GetRowSize(i); j++)
                {
                    model.Grid.SetCell(i, j, SticksList.ElementAt(index));
                    index++;
                }
            }

        }

        private void PlaceBallOnStick(Ball stick)
        {
            BallX = stick.X + stick.Radius / 2 - BallDiameter / 2;
            BallY = stick.Y - BallDiameter - 0.5;
        }

        private void StartSimulation()
        {
            // Istanza thread pallina che cade animata
            ballAnimationThread = new Thread(new ThreadStart(AnimateFallingBall));
            ballAnimationThread.Start();
            PlaceBallOnTopStick();
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
            ResetSimulationCommand = new DelegateCommand(OnReset, CanReset);
            CloseApplicationCommand = new DelegateCommand(OnClose);
            AboutApplicationCommand = new DelegateCommand(OnAbout);
        }

        private void OnStart(object obj)
        {
            SimulationRunning = true;
            StartSimulationCommand.RaiseCanExecuteChanged();
            // Segnala che può resettare la simulazione
            ResetSimulationCommand.RaiseCanExecuteChanged();
            StartSimulation();
        }

        private bool CanStart(object obj)
        {
            return !SimulationRunning;
        }

        private void OnReset(object obj)
        {
            SimulationRunning = false;
            ResetSimulationCommand.RaiseCanExecuteChanged();
            // Segnala che può startare la simulazione
            StartSimulationCommand.RaiseCanExecuteChanged();

            ballAnimationThread.Abort();
            model.Reset();
            PlaceBallOnTopStick();

        }

        private void OnAbout(object obj)
        {
            MessageBox.Show("Creato da Cristiano Colangelo I4AC", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanReset(object obj)
        {
            return SimulationRunning;
        }

        private void OnClose(object obj)
        {
            Application.Current.Shutdown();
        }

        #endregion

    }
}