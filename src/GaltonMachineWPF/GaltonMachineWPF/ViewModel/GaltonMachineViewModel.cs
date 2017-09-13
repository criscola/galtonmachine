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

        public GaltonMachineViewModel()
        {
            model = new GaltonMachine(4);
            
        }
    }
}
