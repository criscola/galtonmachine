    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaltonMachineWPF.Model
{
    public class BellCurve
    {
        // Ampiezza orizzontale
        const float wxmin = -5.1f;
        // Traslazione verticale
        const float wymin = -0.2f;
        const float wxmax = -wxmin;
        // Ampiezza verticale
        const float wymax = 1.1f;

        public BellCurve()
        {

        }

        // The normal distribution function.
        private static float F(float x, float one_over_2pi, float mean, float stddev, float var)
        {
            return (float) (one_over_2pi * Math.Exp(-(x - mean) * (x - mean) / (2 * var)));
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
