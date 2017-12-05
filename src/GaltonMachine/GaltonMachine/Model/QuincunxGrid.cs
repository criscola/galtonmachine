namespace GaltonMachine.Model
{
    public class QuincunxGrid
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private Stick[][] sticksGrid;
        private int size;

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                GenerateGrid();
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public QuincunxGrid(int size)
        {
            this.size = size;
            GenerateGrid();
        }

        #endregion

        #region ================== Metodi pubblici =================

        public Stick GetStick(int row, int column)
        {
            return sticksGrid[row][column];
        }

        public void SetStick(int row, int column, Stick stick)
        {
            sticksGrid[row][column] = stick;
        }

        public int GetRowSize(int rowIndex)
        {
            return sticksGrid[rowIndex].Length;
        }

        #endregion

        #region ================== Metodi privati ==================

        private void GenerateGrid()
        {
            if (size > 2)
            {
                int count = 1;

                sticksGrid = new Stick[size][];
                for (int i = 0; i < size; i++)
                {
                    sticksGrid[i] = new Stick[count];
                    count++;
                }
            }
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
