using System;

namespace GaltonMachineWPF.Model
{
    public class Ball
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Radius { get; set; }

        public Ball() : this (0)
        {
        }

        public Ball(double radius) : this (0, 0, radius)
        {

        }

        public Ball(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        /// <summary>
        /// Fa "rimbalzare" la pallina. Se ritorna false va sinistra, se ritorna true va a destra.
        /// </summary>
        /// <returns>La direzione sottoforma di boolean se a sx o dx</returns>
        public bool Bounce()
        {
            return new Random().Next(0, 2) == 1;
        }
    }
}
