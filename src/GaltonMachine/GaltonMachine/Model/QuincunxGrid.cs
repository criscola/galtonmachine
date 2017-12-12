using System;

namespace GaltonMachine.Model
{
    public class QuincunxGrid
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private Ball[][] sticksGrid;
        private int size;

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public QuincunxGrid(int size)
        {
            Size = size;
        }

        #endregion

        #region ================== Metodi pubblici =================
        
        public int GetRowSize(int rowIndex)
        {
            return sticksGrid[rowIndex].Length;
        }

        #endregion

        #region ================== Metodi privati ==================
        
        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
