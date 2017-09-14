using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GaltonMachine gm = new GaltonMachine(10);
            gm.StartSimulation(15);
        }
    }
}
