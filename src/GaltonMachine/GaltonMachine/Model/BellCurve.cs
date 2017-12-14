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

        public double DEFAULT_DEVIATION_COUNT { get; set; }

        #endregion

        #region ================== Attributi & proprietà =================

        private BitmapImage image;
        private List<double> data;

        public double Mean { get; set; }
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

                DrawCurve(DEFAULT_DEVIATION_COUNT, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
            }

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
        private void DrawCurve(double stddev_multiple, int wid, int hgt, double mean, double stddev, double var)
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
                RectangleF world = new RectangleF(wxmin, wymin, wwid, whgt);
                PointF[] device_points =
                {
                    new PointF(0, GDeviceSize.Height),
                    new PointF(GDeviceSize.Width, GDeviceSize.Height),
                    new PointF(0, 0),
                };
                Matrix transform = new Matrix(world, device_points);

                // Crea una penna sottile da poter usare
                using (Pen pen = new Pen(Color.Red, 0))
                {
                    using (Font font = new Font("Arial", 8))
                    {
                        // Disegna l'asse X
                        gr.Transform = transform;
                        pen.Color = Color.Black;
                        gr.DrawLine(pen, wxmin, 0, wxmax, 0);
                        for (int x = (int)wxmin; x <= wxmax; x++)
                        {
                            gr.DrawLine(pen, x, -0.05f, x, 0.05f);
                            gr.DrawLine(pen, x + 0.25f, -0.025f, x + 0.25f, 0.025f);
                            gr.DrawLine(pen, x + 0.50f, -0.025f, x + 0.50f, 0.025f);
                            gr.DrawLine(pen, x + 0.75f, -0.025f, x + 0.75f, 0.025f);
                        }

                        // Disegna l'asse Y
                        gr.Transform = transform;
                        pen.Color = Color.Black;
                        gr.DrawLine(pen, 0, wymin, 0, wymax);
                        for (int y = (int)wymin; y <= wymax; y++)
                        {
                            gr.DrawLine(pen, -0.2f, y, 0.2f, y);
                            gr.DrawLine(pen, -0.1f, y + 0.25f, 0.1f, y + 0.25f);
                            gr.DrawLine(pen, -0.1f, y + 0.50f, 0.1f, y + 0.50f);
                            gr.DrawLine(pen, -0.1f, y + 0.75f, 0.1f, y + 0.75f);
                        }

                        // Disegna la curva
                        gr.Transform = transform;
                        List<PointF> points = new List<PointF>();

                        float dx = (wxmax - wxmin) / GDeviceSize.Width;
                        for (float x = wxmin; x <= wxmax; x += dx)
                        {
                            double z_score = (x - mean) / stddev;
                            double y = F(z_score, mean, stddev, var);
                            points.Add(new PointF(x, (float)y));
                        }
                        
                        pen.Color = Color.Red;
                        Console.WriteLine("ASAADFSDFSDFSDF {0}", points.Count);
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
