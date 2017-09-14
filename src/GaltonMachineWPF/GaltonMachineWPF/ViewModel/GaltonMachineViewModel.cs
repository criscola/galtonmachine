using GaltonMachineWPF;
using GaltonMachineWPF.Model;
using GaltonMachineWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.ViewModel
{
    public class GaltonMachineViewModel : BindableBase
    {
        private GaltonMachine model;
        private int x;
        private int y;

        public int X { get { return model.Grid. } }
        public GaltonMachineViewModel()
        {
            model = new GaltonMachine(4);
            //model.NewCoordinates += Model_NewCoordinates;
        }

        private void Model_NewCoordinates(int[,] coordinates)
        {
            
        }

        
    }
}
