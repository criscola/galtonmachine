using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class GaltonMachine
    {
        #region =================== costanti ===================
        

        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri e proprietà ===========

        public int[][] Grid { get; private set; }

        #endregion

        #region =================== costruttori ================

        public GaltonMachine(int width)
        {
            
        }

        #endregion

        #region =================== metodi privati ===============

        #endregion

        #region =================== metodi pubblici ============

        public int[][] ComputatePath()
        {
            // Computa se la direzione è sinistra o destra
            int direction = new Random().Next(0, 2);
        }

        #endregion


    }

    public class GaltonMachineAnimator
    {
        public GaltonMachine model;
        public event Action<int[,]> NewCoordinates;
        public void Animation()
        {

        }
    }
}
