using System;
using System.Drawing;

namespace GaltonMachineWPF.Model
{
    public class Ball
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Radius { get; set; }

        public string Color { get; set; }

        public Ball() : this (0, "black")
        {
        }

        public Ball(double radius, string color) : this (0, 0, radius, color)
        {

        }

        public Ball(double x, double y, double radius, string color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Color = color;
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
