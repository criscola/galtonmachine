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

        public QuincunxGrid Grid { get; private set; }
        public Ball Ball { get; set; }

        #endregion

        #region =================== costruttori ================

        public GaltonMachine(int width)
        {
            Ball = new Ball();
            Grid = new QuincunxGrid(width);
        }

        #endregion

        #region =================== metodi privati ===============

        #endregion

        #region =================== metodi pubblici ============

        #endregion


    }
}
