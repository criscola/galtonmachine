using GaltonMachine.Helper;
using System.Windows;
using System.Windows.Media;

namespace GaltonMachine.Model
{
    public class ChartLabel : BindableBase
    {
        #region ================== Costanti =================

        public FontFamily DEFAULT_FONTFAMILY { get { return new FontFamily("Arial"); } }
        public FontStyle DEFAULT_FONTSTYLE { get { return FontStyles.Normal; } }
        public FontWeight DEFAULT_FONTWEIGHT { get { return FontWeights.Bold; } }
        public FontStretch DEFAULT_FONTSTRETCH { get { return FontStretches.Normal; } }
        public double DEFAULT_FONTSIZE { get { return 13; } }

        #endregion

        #region ================== Attributi & proprietà =================

        private string text;

        public FontFamily FontFamily { get; set; }
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontStretch FontStretch { get; set; }
        public double FontSize { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged(() => text);
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public ChartLabel()
        {

        }

        public ChartLabel(double x, double y, double width, string text)
        {
            X = x;
            Y = y;
            Width = width;
            Text = text;
            FontFamily = DEFAULT_FONTFAMILY;
            FontStyle = DEFAULT_FONTSTYLE;
            FontWeight = DEFAULT_FONTWEIGHT;
            FontStretch = DEFAULT_FONTSTRETCH;
            FontSize = DEFAULT_FONTSIZE;
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
