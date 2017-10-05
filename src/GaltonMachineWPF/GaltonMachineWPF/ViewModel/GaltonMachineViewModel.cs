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
        private GaltonMachine model;
        private DispatcherTimer dispTimer;
        private Canvas canvas;
        private bool run = true;

        private const double REFRESH_RATE = 100.0;

        public int CanvasWidth = 440;
        public int CanvasHeight = 400;
        public const int STICKS_DIAMETER = 20;

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
        public ObservableCollection<Ball> SticksList { get; set; }
        public double BallDiameter { get; private set; }

        public GaltonMachineViewModel()
        {
            // Inizializzazione variabili/model
            model = new GaltonMachine(5);
            

            SticksList = new ObservableCollection<Ball>();
            GenerateSticks();

            // Aggiunta della pallina che cade alla lista di elementi da renderizzare
            BallDiameter = 15;
            /*
            model.Ball.Radius = STICKS_DIAMETER;
            SticksList.Add(model.Ball);
            */

            // Inizializzazione dispatcher timer

            dispTimer = new DispatcherTimer();
            dispTimer.Interval = TimeSpan.FromMilliseconds(REFRESH_RATE);
            dispTimer.Tick += Timer_Tick;
            dispTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < SticksList.Count; i++)
            {
                Thread.Sleep(1000);
                Ball currentBall = SticksList.ElementAt(i);
                BallX = currentBall.X + currentBall.Radius / 2 - BallDiameter / 2;
                BallY = currentBall.Y - BallDiameter;
                if (model.Ball.Bounce())
                {
                    
                }
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

            // Conteggio delle stecche
            double n = model.Grid.GetSize();

            // Distanza fra le stecche
            double dx = (cw - STICKS_DIAMETER) / (n - 1);
            double dy = (ch - STICKS_DIAMETER) / (n - 1);

            // Coordinate x y delle stecche 
            double x = 0;
            double y = ch - STICKS_DIAMETER;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    SticksList.Add(new Ball(x + (hoffset / 2), y + (voffset / 2), STICKS_DIAMETER));

                    x += dx;
                }
                x = dx / 2 * (i + 1);
                y -= dy;
            }
        }
    }
}