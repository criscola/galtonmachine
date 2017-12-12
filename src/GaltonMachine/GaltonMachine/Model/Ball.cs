using GaltonMachine.Helper;

namespace GaltonMachine.Model
{
    public class Ball : BindableBase
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        private double x;
        private double y;

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged(() => X);
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged(() => Y);
            }
        }

        private int row;
        private int column;

        public int Row
        {
            get { return row; }
            set
            {
                row = value;
            }
        }
        public int Column
        {
            get { return column; }
            set
            {
                column = value;
            }
        }

        public double Diameter { get; set; }
        public string Color { get; set; }
        
        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public Ball() : this(0, "black")
        {
        }

        public Ball(double radius, string color) : this(0, 0, 0, 0, radius, color)
        {

        }

        public Ball(int row, int column, double x, double y, double diameter, string color)
        {
            Row = row;
            Column = column;
            X = x;
            Y = y;
            Diameter = diameter;
            Color = color;
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void Reset()
        {
            Row = 0;
            Column = 0;
            X = 0;
            Y = 0;
        }

        #endregion

        #region ================== Metodi privati ==================
        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
