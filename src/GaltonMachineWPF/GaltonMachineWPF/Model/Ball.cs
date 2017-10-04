    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class Ball
    {
        public Ball()
        {

        }

        public Ball(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Radius { get; set; }

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
