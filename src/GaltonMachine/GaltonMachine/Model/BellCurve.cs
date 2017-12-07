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
        private double[] data;
        private List<double> valori;

        public double Mean { get; private set; }
        private double media = double.NaN;

        public double Media
        {
            get
            {
                //if (media == double.NaN)
                    GetMedia();
                return media;
            }
            private set { media = value; }
        }

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
            data = new double[size];

            valori = new List<double>();
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void UpdateData(int index, double value)
        {
            valori.Add(index);

            data[index] = value / 10;

            int count = GetDataCount();

            if (count > 2)
            {
                Mean = GetMean();
                Variance = GetVariance();
                StdDev = (double)Math.Sqrt(Variance);

                DrawCurve(DEFAULT_DEVIATION_COUNT, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
            }

        }

        public double F(double x, double mean, double stddev, double var)
        {
            return (double)(1.0 / stddev * Math.Sqrt(2 * Math.PI)) * (double)(Math.Exp(-(((x - mean) * (x - mean)) / (2 * var))));
        }
        public double GetMedia()
        {
            // Calcola la media dei numeri
            double mean = 0;
            foreach (var v in valori)
            {
                mean += v;
            }

            mean = mean / valori.Count;

            //// Guarda se i valori sono distribuiti più verso sinistra o verso destra o idealmente distribuiti
            //int half = data.Length / 2;
            //double[] n1 = new double[half];
            //double[] n2 = new double[half];
            //bool isValuesCountEven = true;

            //// Calcola se la lunghezza del set è dispari
            //if (data.Length % 2 == 1)
            //{
            //    isValuesCountEven = false;
            //}

            //n1 = SubArray<double>(data, 0, half);
            //if (isValuesCountEven)
            //{
            //    n2 = SubArray<double>(data, half, half);
            //}
            //else
            //{
            //    n2 = SubArray<double>(data, half + 1, half);
            //}
            //double n1Sum = 0;
            //double n2Sum = 0;
            //for (int i = 0; i < n1.Length; i++)
            //{
            //    n1Sum += n1[i];
            //    n2Sum += n2[i];
            //}

            //if (n1Sum > n2Sum)
            //{
            //    mean = -mean;
            //}
            //else if (n1Sum == n2Sum)
            //{
            //    mean = 0;
            //}
            Media = mean;
            return mean;
        }


        public double GetMean()
        {
            // Calcola la media dei numeri
            double mean = 0;

            for (int i = 0; i < data.Length; i++)
            {
                mean += data[i];
            }

            mean = mean / data.Length;

            // Guarda se i valori sono distribuiti più verso sinistra o verso destra o idealmente distribuiti
            int half = data.Length / 2;
            double[] n1 = new double[half];
            double[] n2 = new double[half];
            bool isValuesCountEven = true;

            // Calcola se la lunghezza del set è dispari
            if (data.Length % 2 == 1)
            {
                isValuesCountEven = false;
            }

            n1 = SubArray<double>(data, 0, half);
            if (isValuesCountEven)
            {
                n2 = SubArray<double>(data, half, half);
            }
            else
            {
                n2 = SubArray<double>(data, half + 1, half);
            }
            double n1Sum = 0;
            double n2Sum = 0;
            for (int i = 0; i < n1.Length; i++)
            {
                n1Sum += n1[i];
                n2Sum += n2[i];
            }

            if (n1Sum > n2Sum)
            {
                mean = -mean;
            }
            else if (n1Sum == n2Sum)
            {
                mean = 0;
            }
            Media = mean;
            return mean;
        }

        public double GetVariance()
        {
            double dist = 0;

            for (int i = 0; i < data.Length; i++)
            {
                dist += (data[i] - Media) * (data[i] - Media);
            }

            return dist;
        }
        public double GetVarianza()
        {
            double dist = 0;
            foreach (var v in valori)
            {
                dist += (v - Media) * (v - Media);
            }

            return dist;
        }

        #endregion

        #region ================== Metodi privati ==================

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

                        // Etichetta l'asse X
                        gr.Transform = new Matrix();
                        gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        List<PointF> ints = new List<PointF>();
                        for (int x = (int)wxmin; x <= wxmax; x++)
                            ints.Add(new PointF(x, -0.07f));
                        PointF[] ints_array = ints.ToArray();
                        transform.TransformPoints(ints_array);

                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                            int index = 0;
                            for (int x = (int)wxmin; x <= wxmax; x++)
                            {
                                gr.DrawString(x.ToString(), font, Brushes.Black,
                                    ints_array[index++], sf);
                            }
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

                        // Etichetta l'asse Y
                        gr.Transform = new Matrix();
                        gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        ints = new List<PointF>();
                        for (float y = 0.25f; y < 1.01; y += 0.25f)
                            ints.Add(new PointF(0.2f, y));
                        ints_array = ints.ToArray();
                        transform.TransformPoints(ints_array);

                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            int index = 0;
                            foreach (double y in new double[] { 0.25f, 0.5f, 0.75f, 1.0f })
                            {
                                gr.DrawString(y.ToString("0.00"), font, Brushes.Black,
                                    ints_array[index++], sf);
                            }
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

        private double[] SubArray<T>(double[] data, int index, int length)
        {
            double[] result = new double[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public int GetDataCount()
        {
            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0)
                {
                    count++;
                }
            }
            return count;
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
