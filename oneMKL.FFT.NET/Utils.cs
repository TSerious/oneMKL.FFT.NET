using System.Numerics;

namespace oneMKL.FFT.NET
{
    /// <summary>
    /// Some utilities for the fft.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Converts the packed complex number to an array of complex numbers.
        /// </summary>
        /// <param name="packed">The packed complex numbers.</param>
        /// <returns>The unpacked complex numbers.</returns>
        public static Complex[] PackedRealToComplex(double[] packed)
        {
            Complex[] complex = new Complex[packed.Length];

            for (int k = 1; k <= packed.Length; k++)
            {
                if (k == 1)
                {
                    complex[k - 1] = new Complex(packed[k - 1], 0);
                }
                else if (k - 1 == packed.Length - k + 1)
                {
                    complex[k - 1] = new Complex(packed[((k - 1) << 1) - 1], 0);
                }
                else if (k <= (packed.Length >> 1) + 1)
                {
                    complex[k - 1] = new Complex(
                        packed[(((k - 1) << 1) + 0) - 1],
                        packed[(((k - 1) << 1) + 1) - 1]);
                }
                else
                {
                    complex[k - 1] = new Complex(
                        packed[(((packed.Length - k + 1) << 1) + 0) - 1],
                        -1 * packed[(((packed.Length - k + 1) << 1) + 1) - 1]);
                }
            }

            return complex;
        }

        /// <summary>
        /// Converts the packed complex number to an array of complex numbers.
        /// </summary>
        /// <param name="packed">The packed complex numbers.</param>
        /// <returns>The unpacked complex numbers.</returns>
        public static Complex[] PackedRealToComplex(float[] packed)
        {
            Complex[] complex = new Complex[packed.Length];

            for (int k = 1; k <= packed.Length; k++)
            {
                if (k == 1)
                {
                    complex[k - 1] = new Complex(packed[k - 1], 0);
                }
                else if (k - 1 == packed.Length - k + 1)
                {
                    complex[k - 1] = new Complex(packed[((k - 1) << 1) - 1], 0);
                }
                else if (k <= (packed.Length >> 1) + 1)
                {
                    complex[k - 1] = new Complex(
                        packed[(((k - 1) << 1) + 0) - 1],
                        packed[(((k - 1) << 1) + 1) - 1]);
                }
                else
                {
                    complex[k - 1] = new Complex(
                        packed[(((packed.Length - k + 1) << 1) + 0) - 1],
                        -1 * packed[(((packed.Length - k + 1) << 1) + 1) - 1]);
                }
            }

            return complex;
        }

        /// <summary>
        /// Converts the real <paramref name="data"/>
        /// to complex numbers.
        /// </summary>
        /// <param name="data">The real data.</param>
        /// <returns>Complex numbers, all with 0i.</returns>
        public static Complex[] ToComplex(double[] data)
        {
            Complex[] result = new Complex[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new Complex(data[i], 0.0);
            }

            return result;
        }

        /// <summary>
        /// Multiplies each element in <paramref name="data"/>
        /// with <paramref name="scaleFactor"/>.
        /// </summary>
        /// <remarks>
        /// It seems like DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.BACKWARD_SCALE, scale_factor) doesn't work.
        /// Instead the scaling can be done with this function.
        /// </remarks>
        /// <param name="data">The data.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        public static void Scale(ref double[] data, double scaleFactor)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] *= scaleFactor;
            }
        }
    }
}
