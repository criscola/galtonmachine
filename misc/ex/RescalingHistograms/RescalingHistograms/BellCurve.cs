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

namespace RescalingHistograms
{
    public class BellCurve
    {
        public const float DEVIATIONS = 3;

        private List<float> Data { get; set; }
        public float Mean { get; private set; }
        public float Variance { get; private set; }
        public float StdDev { get; private set; }
        public System.Drawing.Size GDeviceSize { get; set; }
        public BitmapImage Image { get; private set; }

        public BellCurve(List<float> data, System.Drawing.Size gDeviceSize)
        {
            Mean = GetMean(data);
            Variance = GetVariance(data, Mean);
            StdDev = (float)Math.Sqrt(Variance);
            GDeviceSize = gDeviceSize;

            Console.WriteLine("mean {0} variance {1} stdev {2} gdevw {3} gdevh {4}", Mean, Variance, StdDev, GDeviceSize.Width, GDeviceSize.Height);

            DrawCurve(DEVIATIONS, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
        }

        public void UpdateData(int index, float value)
        {
            Data[index] = value;

            Mean = GetMean(Data);
            Variance = GetVariance(Data, Mean);
            StdDev = (float)Math.Sqrt(Variance);

            DrawCurve(DEVIATIONS, GDeviceSize.Width, GDeviceSize.Height, Mean, StdDev, Variance);
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
                float wxmin = mean - stddev * stddev_multiple;
                float wxmax = mean + stddev * stddev_multiple;
                float one_over_2pi = (float)(1.0 / (stddev * Math.Sqrt(2 * Math.PI)));
                float wymax = F(mean, one_over_2pi, mean, stddev_multiple, var) * 1.1f;
                float wymin = -0.2f * wymax;

                float wwid = wxmax - wxmin;
                float whgt = wymax - wymin;
                RectangleF world = new RectangleF(wxmin, wymin, wwid, whgt);
                PointF[] device_points =
                {
                    new PointF(0, GDeviceSize.Height),
                    new PointF(GDeviceSize.Width, GDeviceSize.Height),
                    new PointF(0, 0),
                };
                Matrix transform = new Matrix(world, device_points);

                // Get the inverse transform.
                Matrix inverse = transform.Clone();

                Console.WriteLine("wxmin {0} wxmax {1} wymax {2} wymin {3} wwid {4} whgt {5}", wxmin.ToString(), wxmax, wymax, wymin, wwid, whgt);

                inverse.Invert();

                // Get tick mark lengths.
                PointF[] ticks = { new PointF(5, 5) };
                inverse.TransformVectors(ticks);
                float tick_dx = ticks[0].X;
                float tick_dy = -ticks[0].Y;

                // Make a thin Pen to use.
                using (Pen pen = new Pen(Color.Red, 0))
                {
                    using (Font font = new Font("Arial", 8))
                    {
                        // Draw the X axis.
                        gr.Transform = transform;
                        pen.Color = Color.Black;
                        gr.DrawLine(pen, wxmin, 0, wxmax, 0);
                        for (int x = (int)wxmin - 1; x <= wxmax; x++)
                        {
                            gr.DrawLine(pen, x, -tick_dy * 2, x, tick_dy * 2);
                            gr.DrawLine(pen, x + 0.25f, -tick_dy, x + 0.25f, tick_dy);
                            gr.DrawLine(pen, x + 0.50f, -tick_dy, x + 0.50f, tick_dy);
                            gr.DrawLine(pen, x + 0.75f, -tick_dy, x + 0.75f, tick_dy);
                        }

                        // Label the X axis.
                        gr.Transform = new Matrix();
                        gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        List<PointF> ints = new List<PointF>();
                        for (int x = (int)wxmin; x <= wxmax; x++)
                            ints.Add(new PointF(x, -2 * tick_dy));
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
                        for (int y = (int)wymin - 1; y <= wymax; y++)
                        {
                            gr.DrawLine(pen, -tick_dx * 2, y, tick_dx * 2, y);
                            gr.DrawLine(pen, -tick_dx, y + 0.25f, tick_dx, y + 0.25f);
                            gr.DrawLine(pen, -tick_dx, y + 0.50f, tick_dx, y + 0.50f);
                            gr.DrawLine(pen, -tick_dx, y + 0.75f, tick_dx, y + 0.75f);
                        }

                        // Label the Y axis.
                        gr.Transform = new Matrix();
                        gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        ints = new List<PointF>();
                        for (float y = 0.25f; y < wymax; y += 0.25f)
                            ints.Add(new PointF(2 * tick_dx, y));
                        if (ints.Count > 0)
                        {
                            ints_array = ints.ToArray();
                            transform.TransformPoints(ints_array);
                        }

                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Center;
                            int index = 0;
                            for (float y = 0.25f; y < wymax; y += 0.25f)
                            {
                                gr.DrawString(y.ToString("0.00"), font, Brushes.Black,
                                    ints_array[index++], sf);
                            }
                        }

                        // Draw the curve.
                        gr.Transform = transform;
                        List<PointF> points = new List<PointF>();

                        float dx = (wxmax - wxmin) / GDeviceSize.Width;
                        for (float x = wxmin; x <= wxmax; x += dx)
                        {
                            float y = F(x, one_over_2pi, mean, stddev, var);
                            points.Add(new PointF(x, y));
                        }
                        pen.Color = Color.Red;
                        gr.DrawLines(pen, points.ToArray());
                    } // Font
                } // Pen
            }

            Image = BitmapToImageSource(bm);
        }


        // The normal distribution function.
        private static float F(float x, float one_over_2pi, float mean, float stddev, float var)
        {
            return (float)(one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * var)));
        }

        private static float GetMean(List<float> values)
        {
            // Calcola la media dei numeri
            float mean = 0;

            for (int i = 0; i < values.Count; i++)
            {
                mean += values[i];
            }

            return mean /= values.Count;
        }

        private static float GetVariance(List<float> values, float mean)
        {
            float dist = 0;

            for (int i = 0; i < values.Count; i++)
            {
                dist += (values[i] - mean) * (values[i] - mean);
            }

            return dist /= values.Count;
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
    }
}
