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

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        private GaltonMachine model;
        private DispatcherTimer dispTimer;

        private const double REFRESH_RATE = 1500.0;

        public int CanvasWidth = 440;
        public int CanvasHeight = 400;
        public const int STICKS_DIAMETER = 20;

        public double BallX { get { return model.Ball.X; } private set { model.Ball.X = value; } }
        public double BallY { get { return model.Ball.Y; } private set { model.Ball.X = value; } }
        public ObservableCollection<Ball> SticksList { get; set; }


        public GaltonMachineViewModel()
        {
            // Inizializzazione variabili/model
            model = new GaltonMachine(4);

            //BallX = CanvasWidth / 2;
            SticksList = new ObservableCollection<Ball>();
            GeneraStecche();
            
            // Inizializzazione dispatcher timer
            dispTimer = new DispatcherTimer();
            dispTimer.Interval = TimeSpan.FromMilliseconds(REFRESH_RATE);
            dispTimer.Tick += Timer_Tick;
            dispTimer.Start();  
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            /*
            if (model.Ball.Bounce())
            {
                model.Ball.X += 50;
                model.Ball.Y += 50;
            }
            else
            {
                model.Ball.Y += 50;
            }
            OnPropertyChanged(() => BallX);
            OnPropertyChanged(() => BallY);*/
        }

        private void GeneraStecche()
        {
            // Grandezza della base
            double n = model.Grid.GetSize();
            // Distanza fra le stecche
            double dx = (CanvasWidth - STICKS_DIAMETER) / (n - 1);
            double dy = (CanvasHeight - STICKS_DIAMETER) / (n - 1);
            // Coordinate x y delle stecche iniziali
            double x = 0;
            double y = CanvasHeight - STICKS_DIAMETER;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    Ball currentBall = new Ball();
                    currentBall.Radius = STICKS_DIAMETER;
                    currentBall.X = x;
                    currentBall.Y = y;
                    SticksList.Add(currentBall);

                    x += dx;
                }
                x = dx / 2 * (i + 1);
                y -= dy;
            }
        }
    }
}