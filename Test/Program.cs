using System;
using System.Numerics;
using oneMKL.FFT.NET;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This will run a short test of the oneMKL.FFT.NET library.");
            Test1();
            Test2(GenerateData(512));

            Console.WriteLine();
            Console.ReadKey();
        }

        private static bool Test1()
        {
            Console.WriteLine();
            Console.WriteLine("###############");
            Console.WriteLine("# Test 1      #");
            Console.WriteLine("###############");
            Console.WriteLine("Forward and backward fft with length 6.");

            IntPtr desc = new IntPtr();
            int length = 6;

            /* The data to be transformed */
            double[] x_normal = new double[length];
            double[] x_transformed = new double[length];

            /* Create new DFTI descriptor */
            int ret = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.REAL,
                1,
                length);

            /* Setup the scale factor */
            long transform_size = length;
            double scale_factor = 1.0 / transform_size;
            Console.WriteLine("Backward transform scale: " + scale_factor);

            /* Setup the transform parameters */
            ret += DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PLACEMENT, (int)DFTI.CONFIG_VALUE.NOT_INPLACE);
            ret += DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PACKED_FORMAT, (int)DFTI.CONFIG_VALUE.PACK_FORMAT);

            /* Commit the descriptor */
            ret += DFTI.DftiCommitDescriptor(desc);

            /* Initialize the data array */
            Console.WriteLine("Initial data:");
            for (int i = 0; i < length; i++)
            {
                x_normal[i] = i;
                Console.Write("\t" + i);
            }
            Console.WriteLine();

            /* Forward, then backward transform */
            ret += DFTI.DftiComputeForward(desc, x_normal, x_transformed);
            ret += DFTI.DftiComputeBackward(desc, x_transformed, x_normal);

            /* Free the descriptor after everything is done */
            ret += DFTI.DftiFreeDescriptor(ref desc);

            Utils.Scale(ref x_normal, scale_factor);

            /* Check the data array */
            Console.WriteLine("Resulting data:");
            for (int i = 0; i < length; i++)
            {
                Console.Write("\t" + x_normal[i]);
            }

            Console.WriteLine();
            if (ret != DFTI.NO_ERROR)
            {
                Console.WriteLine("TEST NOT PASSED!");
                return false;
            }
            
            Console.WriteLine("Test passed.");
            return true;
        }

        static bool Test2(double[] input)
        {
            Console.WriteLine();
            Console.WriteLine("###############");
            Console.WriteLine("# Test 2      #");
            Console.WriteLine("###############");
            Console.WriteLine("Forward and backward fft with length " + input.Length + ".");

            IntPtr desc = new IntPtr();
            int res = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.COMPLEX,
                1,
                input.Length);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't initialize");
                return false;
            }

            res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't set placement.");
                return false;
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't commit descriptor.");
                return false;
            }

            Complex[] data1 = Utils.ToComplex(input);
            Complex[] data2 = new Complex[input.Length];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't run fft.");
                return false;
            }

            res = DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                Console.WriteLine("Can't run inverse fft.");
                return false;
            }

            DFTI.DftiFreeDescriptor(ref desc);

            Console.WriteLine("Test executed successfully.");
            return true;
        }

        static double[] GenerateData(int length)
        {
            double[] data = new double[length];

            double step = Math.PI * 2 / (length >> 1);
            for (int i = 0; i < length; i++)
            {
                data[i] = Math.Sin(step * i);
            }

            return data;
        }
    }
}
