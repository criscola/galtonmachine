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

        public float DEFAULT_DEVIATION_COUNT { get; set; }

        #endregion

        #region ================== Attributi & proprietà =================

        private BitmapImage image;
        private float[] data;

        public float Mean { get; private set; }
        public float Variance { get; private set; }
        public float StdDev { get; private set; }
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
            data = new float[size];
        }

        #endregion

        #region ================== Metodi pubblici =================

        public void UpdateData(int index, float value)
        {
            data[index] = value / 10;

            int count = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != 0)
                {
                    count++;
                }
            }
            if (count > 2)
            {
                Mean = GetMean(data);
                Variance = GetVariance(data, Mean);
                StdDev = (float)Math.Sqrt(Variance);

                DrawCurve(DEFAULT_DEVIATION_COUNT, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
            }

        }

        public float F(float x, float mean, float stddev, float var)
        {
            return (float)(1 / stddev * Math.Sqrt(2 * Math.PI)) * (float)(Math.Exp(-(((x - mean) * (x - mean)) / (2 * var))));
        }

        public float GetMean(float[] values)
        {
            // Calcola la media dei numeri
            float mean = 0;

            for (int i = 0; i < values.Length; i++)
            {
                mean += values[i];
            }

            mean = mean / values.Length;

            // Guarda se i valori sono distribuiti più verso sinistra o verso destra o idealmente distribuiti
            int half = values.Length / 2;
            float[] n1 = new float[half];
            float[] n2 = new float[half];
            bool isValuesCountEven = true;

            // Calcola se la lunghezza del set è dispari
            if (values.Length % 2 == 1)
            {
                isValuesCountEven = false;
            }

            n1 = SubArray<float>(values, 0, half);
            if (isValuesCountEven)
            {
                n2 = SubArray<float>(values, half, half);
            }
            else
            {
                n2 = SubArray<float>(values, half + 1, half);
            }
            float n1Sum = 0;
            float n2Sum = 0;
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

            return mean;
        }

        public float GetVariance(float[] values, float mean)
        {
            float dist = 0;

            for (int i = 0; i < values.Length; i++)
            {
                dist += (values[i] - mean) * (values[i] - mean);
            }

            return dist;
        }

        #endregion

        #region ================== Metodi privati ==================

        // Fonte: Rod Stephens - http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/
        private void DrawCurve(float stddev_multiple, int wid, int hgt, float mean, float stddev, float var)
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
                            foreach (float y in new float[] { 0.25f, 0.5f, 0.75f, 1.0f })
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
                            float z_score = (x - mean) / stddev;
                            float y = F(z_score, mean, stddev, var);
                            points.Add(new PointF(x, y));
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

        private float[] SubArray<T>(float[] data, int index, int length)
        {
            float[] result = new float[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        #endregion

        #region ================== Metodi dei delegati =================
        #endregion
    }
}
