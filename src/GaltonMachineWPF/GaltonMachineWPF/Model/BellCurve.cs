using GaltonMachineWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GaltonMachineWPF.Model
{
    public class BellCurve : BindableBase
    {
        public const float DEVIATIONS = 3;

        private float[] Data { get; set; }
        public float Mean { get; private set; }
        public float Variance { get; private set; }
        public float StdDev { get; private set; }
        public System.Drawing.Size GDeviceSize { get; set; }
        private BitmapImage image;
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

        public BellCurve(int size, System.Drawing.Size gDeviceSize)
        {
            GDeviceSize = gDeviceSize;
            Data = new float[size];
        }

        public void UpdateData(int index, float value)
        {
            Data[index] = value / 10;

            int count = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] != 0)
                {
                    count++;
                }
            }
            if (count > 2)
            {
                /*
                List<float> f = new List<float>();
                for (int i = 0; i < Data.Length; i++)
                {
                    f.Add(Data[i] * (i + 1));
                }
                Data = f.ToArray();*/

                Mean = GetMean(Data);
                Variance = GetVariance(Data, Mean);
                StdDev = (float)Math.Sqrt(Variance);

                //Console.WriteLine("Mean: {0} Variance: {1} StdDev {2}", Mean, Variance, StdDev);

                DrawCurve(DEVIATIONS, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
            }
            else
            {
                //Console.WriteLine("Sono necessari almeno 3 dati");
            }
            
        }

        private void DrawCurve(float stddev_multiple, int wid, int hgt, float mean, float stddev, float var)
        {
            // Make a bitmap.
            Bitmap bm = new Bitmap(GDeviceSize.Width, GDeviceSize.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Define the mapping from world
                // coordinates onto the PictureBox.
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

                // Make a thin Pen to use.
                using (Pen pen = new Pen(Color.Red, 0))
                {
                    using (Font font = new Font("Arial", 8))
                    {

                        // Draw the X axis.
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

                        // Label the X axis.
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

                        // Draw the Y axis.
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

                        // Label the Y axis.
                        /*
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
                        */
                        // Disegno curva di bezier
                        pen.Color = Color.Red;
                        /*
                        PointF[] bezierPoints = new PointF[Data.Length+2];
                        float step = 10 / 5;
                        bezierPoints[0] = new PointF(-5f, Data[0]);
                        float bezierX = -5 + step;
                        for (int i = 0; i < Data.Length; i++)
                        {
                            bezierPoints[i+1] = new PointF(bezierX, Data[i]);
                            bezierX += step;
                        }
                        bezierPoints[bezierPoints.Length - 1] = new PointF(bezierX, Data[Data.Length - 1]);

                        foreach(PointF f in bezierPoints)
                        {
                            Console.WriteLine("POINTFX: {0} POINTFY: {1}", f.X, f.Y);
                        }

                        // Draw arc to screen.
                        gr.DrawBeziers(pen, bezierPoints);

                        PointF start = new PointF(-1.0F, 1.0F);
                        PointF control1 = new PointF(-2.0F, 1.0F);
                        PointF control2 = new PointF(-35.0F, 5.0F);
                        PointF end1 = new PointF(-5.0F, 1.0F);
                        PointF control3 = new PointF(-6.0F, 15.0F);
                        PointF control4 = new PointF(-65.0F, 25.0F);
                        PointF end2 = new PointF(-5.0F, 3.0F);
                        PointF[] bezierPoints = { start, control1, control2, end1,
         control3, control4, end2 };

                        // Draw arc to screen.
                        gr.DrawBeziers(pen, bezierPoints);
                        */
                        pen.Color = Color.DarkGreen;

                        // Draw the curve.
                        gr.Transform = transform;
                        List<PointF> points = new List<PointF>();
                        /*
                        for (int i = 0; i < Data.Length; i++)
                        {
                            float z_score = (Data[i] - mean) / stddev;
                            float y = F(z_score, mean, stddev, var);
                            points.Add(new PointF(z_score, y));
                        }*/

                        
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


        // The normal distribution function.
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
                //Console.WriteLine(i + " - " + values[i]);
            }

            mean = mean / values.Length;
            //Console.WriteLine("mesia linq"+mean + " - " + values.Average());
            mean = values.Average();
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

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
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
        public float[] SubArray<T>(float[] data, int index, int length)
        {
            float[] result = new float[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
