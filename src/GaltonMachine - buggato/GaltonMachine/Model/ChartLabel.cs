using GaltonMachineWPF.Helpers;
using System.Windows;
using System.Windows.Media;

namespace GaltonMachineWPF.Model
{
    public class ChartLabel : BindableBase
    {
        public FontFamily DEFAULT_FONTFAMILY { get { return new FontFamily("Arial"); } }
        public FontStyle DEFAULT_FONTSTYLE { get { return FontStyles.Normal; } }
        public FontWeight DEFAULT_FONTWEIGHT { get { return FontWeights.Bold; } }
        public FontStretch DEFAULT_FONTSTRETCH { get { return FontStretches.Normal; } }
        public double DEFAULT_FONTSIZE { get { return 13; } }
        public FontFamily FontFamily { get; set; }
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontStretch FontStretch { get; set; }
        public double FontSize { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }

        private string text;
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
    }
}
