using GaltonMachineWPF;
using GaltonMachineWPF.Model;
using GaltonMachineWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        private GaltonMachine model;
        private DispatcherTimer timer;
        private int ballX;
        private int ballY;
        private readonly double REFRESH_RATE = 1500.0;

        public int BallX { get { return model.Ball.X; } }
        public int BallY { get { return model.Ball.Y; } }

        public Ball Ball { get { return model.Ball; } }

        public GaltonMachineViewModel()
        {
            model = new GaltonMachine(4);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(REFRESH_RATE);
            timer.Tick += Timer_Tick;
            timer.Start();  
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ballY += 50;
            

                ballX = Ball.Y;
                OnPropertyChanged(() => BallY);
            

            /*
            if (ore != Hours)
            {
                ore = Hours;
                OnPropertyChanged(() => Hours);
            }
            if (minuti != Minutes)
            {
                minuti = Minutes;
                OnPropertyChanged(() => Minutes);

            }
            if (secondi != Seconds)
            {
                secondi = Seconds;
                OnPropertyChanged(() => Seconds);
            }*/
        }

    }
}
