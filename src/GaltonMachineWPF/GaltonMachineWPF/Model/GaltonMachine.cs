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

        public const int BALL_PRESENT = 1;
        public const int BALL_EMPTY = 0;

        #endregion

        #region =================== membri statici =============
        #endregion

        #region =================== membri e proprietà ===========

        public int[][] Grid { get; private set; }

        #endregion

        #region =================== costruttori ================

        public GaltonMachine(int width)
        {
            // TODO: Forse rimuovere if quando si saranno aggiunti dei controlli alla fonte dell'input
            if (width > 2)
            {
                // Inizializza l'array della griglia
                Grid = new int[width][];

                for (int i = 0; i < width; i++)
                {
                    Grid[i] = new int[width + 1];
                }
            }
            else
            {
                Debug.WriteLine("Specified width must be >= 2.");
            }
        }

        #endregion

        #region =================== metodi privati ===============

        #endregion

        #region =================== metodi pubblici ============

        #endregion


    }

    public class GaltonMachineAnimator
    {
        public void Animation()
        {

        }
    }
}
