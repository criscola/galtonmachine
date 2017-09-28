using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        public Ball Ball { get; set; }

        #endregion

        #region =================== costruttori ================

        public GaltonMachine(int width)
        {
            ThreadStart tOra = new ThreadStart(ComputatePath);
            Thread thread = new Thread(tOra);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        #endregion

        #region =================== metodi privati ===============

        #endregion

        #region =================== metodi pubblici ============

        public void ComputatePath()
        {
            // Computa se la direzione è sinistra o destra
            Ball.Bounce();
        }

        #endregion


    }
}
