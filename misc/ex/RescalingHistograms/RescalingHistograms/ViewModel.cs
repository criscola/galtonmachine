using RescalingHistograms.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RescalingHistograms
{
    public class ViewModel
    {
        public string Coords { get; set; }
        public string InitialCoords { get; set; }
        public float[] data = { 0.1f, 0.4f, 0.7f, 0.8f, 0.7f, 0.4f, 0.2f };
        public int GDeviceWidth { get { return 300; } }
        public int GDeviceHeight { get { return 300; } }
        public BitmapImage Bm { get; set; }
        public ViewModel()
        {
            List<float> f = new List<float> { 0.2f, 0.4f, 0.7f, 0.4f, 0.2f };
            BellCurve curve = new BellCurve(f, new Size(GDeviceWidth, GDeviceHeight));

            Bm = curve.Image;

        }
    }
}
