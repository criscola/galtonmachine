namespace GaltonMachine.Model
{
    public class Circle
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

        public Circle() : this (0, "black")
        {
        }

        public Circle(double radius, string color) : this (0, 0, radius, color)
        {

        }

        public Circle(double x, double y, double diameter, string color)
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
