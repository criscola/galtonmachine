using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonConsole
{
    public class Ball
    {
        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
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
