using GaltonMachine.Helper;

namespace GaltonMachine.Model
{
    public class Stick : BindableBase
    {
        #region ================== Costanti =================
        #endregion

        #region ================== Attributi & proprietà =================

        public double X { get; set; }
        public double Y { get; set; }
        public double Diameter { get; set; }
        public string Color { get; set; }
        
        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public Stick() : this(0, "black")
        {
        }

        public Stick(double radius, string color) : this(0, 0, radius, color)
        {

        }

        public Stick(double x, double y, double diameter, string color)
        {
            X = x;
            Y = y;
            Diameter = diameter;
            Color = color;
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
