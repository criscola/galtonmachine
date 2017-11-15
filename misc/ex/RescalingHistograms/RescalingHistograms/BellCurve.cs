﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RescalingHistograms
{
    class BellCurve
    {
        public List<float> Data { get; private set; }
        public float Mean { get; private set; }
        public float Variance { get; private set; }
        public float Stdd { get; private set; }
        public List<Point> CurvePoints { get; private set; }

        public BellCurve(List<float> data, float gDeviceWidth)
        {
            Data = data;
            Mean = GetMean(Data);
            Variance = GetVariance(Data, Mean);
            Stdd = (float)Math.Sqrt(Variance);

            float one_over_2pi = (float)(1.0 / (Stdd * Math.Sqrt(2 * Math.PI)));

            const float wxmin = -5.1f;
            // Traslazione verticale
            const float wxmax = -wxmin;

            float dx = (wxmax - wxmin) / gDeviceWidth;
            for (float x = wxmin; x <= wxmax; x += dx)
            {
                float y = F(x, one_over_2pi, Mean, Stdd, Variance);
                Console.WriteLine(y);
                CurvePoints = new List<Point>();
                CurvePoints.Add(new Point(x, y));
            }
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
                dist = (Math.Abs(values[i] - mean)) * 2;
            }

            return dist /= values.Count;
        }
    }
}
