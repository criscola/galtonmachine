using GaltonMachine.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace GaltonMachine.Model
{
    public class BellCurve : BindableBase
    {
        #region ================== Costanti =================
        
        #endregion

        #region ================== Attributi & proprietà =================

        private BitmapImage image;
        private List<double> data;

        public double Mean { get; private set; }
        public double Variance { get; private set; }
        public double StdDev { get; private set; }
        public Size GDeviceSize { get; set; }
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            private set
            {
                image = value;
                OnPropertyChanged(() => Image);
            }
        }

        #endregion

        #region ================== Delegati=================
        #endregion

        #region ================== Costruttori =================

        public BellCurve(int size, Size gDeviceSize)
        {
            GDeviceSize = gDeviceSize;
            data = new List<double>();
            Mean = 0;
            Variance = 0;
            StdDev = 0;
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void UpdateData(int index)
        {
            data.Add(index);

            if (data.Count > 2)
            {
                Mean = GetMean();
                Variance = GetVariance();
                StdDev = (double)Math.Sqrt(Variance);

                DrawCurve();
            }

        }

        public void Reset()
        {
            Image = null;
            Mean = 0;
            Variance = 0;
            StdDev = 0;
        }

        #endregion

        #region ================== Metodi privati ==================

        private double F(double x, double mean, double stddev, double var)
        {
            return (double)(1.0 / stddev * Math.Sqrt(2 * Math.PI)) * (double)(Math.Exp(-(((x - mean) * (x - mean)) / (2 * var))));
        }
        private double GetMean()
        {
            // Calcola la media dei numeri
            double mean = 0;
            foreach (var v in data)
            {
                mean += v;
            }

            mean /= data.Count;

            return mean;
        }

        private double GetVariance()
        {
            double dist = 0;
            foreach (var v in data)
            {
                dist += (v - Mean) * (v - Mean);
            }

            return dist;
        }

        // Fonte: Rod Stephens - http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/
        private void DrawCurve()
        {
            Bitmap bm = new Bitmap(GDeviceSize.Width, GDeviceSize.Height);
            using (System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Definisce la mappatura di world (la matrice rappresentante l'area di disegno)
                // Ampiezza orizzontale
                const float wxmin = -5.1f;
                // Traslazione verticale
                const float wymin = -0.2f;
                const float wxmax = -wxmin;
                // Ampiezza verticale
                const float wymax = 1.1f;
                const float wwid = wxmax - wxmin;
                const float whgt = wymax - wymin;

                // Crea il rettangolo rappresentate l’area di disegno
                RectangleF world = new RectangleF(wxmin, wymin, wwid, whgt);

                // Crea i punti degli angoli dell’area di disegno
                PointF[] device_points =
                {
                    new PointF(0, GDeviceSize.Height),
                    new PointF(GDeviceSize.Width, GDeviceSize.Height),
                    new PointF(0, 0),
                };

                // Crea la matrice sulla base del rettangolo e dei punti
                Matrix transform = new Matrix(world, device_points);

                // Crea una penna sottile da poter usare
                using (Pen pen = new Pen(Color.Red, 0))
                {
                    using (Font font = new Font("Arial", 8))
                    {
                        // Disegna la curva
                        gr.Transform = transform;
                        List<PointF> points = new List<PointF>();

                        float dx = (wxmax - wxmin) / GDeviceSize.Width;
                        for (float x = wxmin; x <= wxmax; x += dx)
                        {
                            double y = F(x, Mean, StdDev, Variance);
                            points.Add(new PointF(x, (float)y));
                        }
                        
                        pen.Color = Color.Red;
                        gr.DrawLines(pen, points.ToArray());

                    } // Font
                } // Pen
                Image = BitmapToImageSource(bm);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                
                return bitmapimage;
            }
        }
        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
