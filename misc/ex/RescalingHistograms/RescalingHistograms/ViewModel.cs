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
        public float GDeviceWidth { get { return 300; } }
        public float GDeviceHeight { get { return 300; } }
        public BitmapImage Bm { get; set; }
        public ViewModel()
        {
            float mean = GetMean(data);
            float var = GetVariance(data, mean);
            float stddev = (float)Math.Sqrt(var);

            // Make a bitmap.
            Bitmap bm = new Bitmap((int)GDeviceWidth, (int)GDeviceHeight);
            
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
                    new PointF(0, GDeviceHeight),
                    new PointF(GDeviceWidth, GDeviceHeight),
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

                        // Draw the curve.
                        gr.Transform = transform;
                        List<PointF> points = new List<PointF>();
                        float one_over_2pi = (float)(1.0 / (stddev * Math.Sqrt(2 * Math.PI)));

                        string str = "";

                        float dx = (wxmax - wxmin) / GDeviceWidth;
                        for (float x = wxmin; x <= wxmax; x += dx)
                        {
                            float y = F(x, one_over_2pi, mean, stddev, var);
                            points.Add(new PointF(x, y));
                            str += x + "," + y + " ";
                        }
                        pen.Color = Color.Red;
                        gr.DrawLines(pen, points.ToArray());
                        Console.WriteLine(str);
                    } // Font
                } // Pen

                Bm = BitmapToImageSource(bm);
            }

            /*
            List<float> data = new List<float>();
            data.Add(0.1f);
            data.Add(0.2f);
            data.Add(0.5f);
            data.Add(0.7f);
            data.Add(0.5f);
            data.Add(0.2f);
            data.Add(0.1f);

            InitialCoords = "";
            Coords = "";
            BellCurve curve = new BellCurve(data, 200);

            InitialCoords = curve.CurvePoints[0].X + "," + curve.CurvePoints[0].Y + " ";
            for (int i = 1; i < curve.CurvePoints.Count; i++)
            {
                Coords += curve.CurvePoints[i].X + "," + curve.CurvePoints[i].Y + " ";
            }   */

        }
        public BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        // The normal distribution function.
        private static float F(float x, float one_over_2pi, float mean, float stddev, float var)
        {
            return (float)(one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * var)));
        }

        private static float GetMean(float[] values)
        {
            // Calcola la media dei numeri
            float mean = 0;

            for (int i = 0; i < values.Length; i++)
            {
                mean += values[i];
            }

            return mean /= values.Length;
        }

        private static float GetVariance(float[] values, float mean)
        {
            float dist = 0;

            for (int i = 0; i < values.Length; i++)
            {
                dist = (Math.Abs(values[i] - mean)) * 2;
            }

            return dist /= values.Length;
        }
    }
}
