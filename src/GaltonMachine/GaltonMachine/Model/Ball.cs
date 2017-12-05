namespace GaltonMachine.Model
{
    public class Ball : Stick
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private int row;
        private int column;

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
                OnPropertyChanged(() => Row);
            }
        }
        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                OnPropertyChanged(() => Column);
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public Ball()
        {

        }

        public Ball(int row, int column, double x, double y, double diameter)
        {
            Row = row;
            Column = column;
            base.X = x;
            base.Y = y;
            base.Diameter = diameter;
        }

        #endregion

        #region ================== Metodi pubblici =================
        #endregion

        #region ================== Metodi privati ==================
        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }

}
