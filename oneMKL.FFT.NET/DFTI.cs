using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace oneMKL.FFT.NET
{
    public sealed class DFTI
    {
        public const int VERSION_LENGTH = 198;
        public const int MAX_NAME_LENGTH = 10;
        public const int MAX_MESSAGE_LENGTH = 40;

        /** DFTI predefined error classes */
        public const int NO_ERROR = 0;
        public const int MEMORY_ERROR = 1;
        public const int INVALID_CONFIGURATION = 2;
        public const int INCONSISTENT_CONFIGURATION = 3;
        public const int NUMBER_OF_THREADS_ERROR = 8;
        public const int MULTITHREADED_ERROR = 4;
        public const int BAD_DESCRIPTOR = 5;
        public const int UNIMPLEMENTED = 6;
        public const int MKL_INTERNAL_ERROR = 7;
        public const int LENGTH_EXCEEDS_INT32 = 9;

        /// <summary>
        /// Descriptor configuration parameters [default values in brackets].
        /// </summary>
        public enum CONFIG_PARAM : int
        {
            /// <summary>
            /// Domain for forward transform. No default value.
            /// </summary>
            FORWARD_DOMAIN = 0,

            /// <summary>
            /// Dimensionality, or rank. No default value.
            /// </summary>
            DIMENSION = 1,

            /// <summary>
            /// Length(s) of transform. No default value.
            /// </summary>
            LENGTHS = 2,

            /// <summary>
            /// Floating point precision. No default value.
            /// </summary>
            DFTI_PRECISION = 3,

            /// <summary>
            /// Scale factor for forward transform [1.0].
            /// </summary>
            FORWARD_SCALE = 4,

            /// <summary>
            /// Scale factor for backward transform [1.0].
            /// </summary>
            BACKWARD_SCALE = 5,

            /// <summary>
            /// Exponent sign for forward transform [DFTI_NEGATIVE].
            /// DFTI_FORWARD_SIGN = 6, ## NOT IMPLEMENTED
            /// Number of data sets to be transformed [1].
            /// </summary>
            NUMBER_OF_TRANSFORMS = 7,

            /// <summary>
            /// Storage of finite complex-valued sequences in complex domain [DFTI_COMPLEX_COMPLEX].
            /// </summary>
            COMPLEX_STORAGE = 8,

            /// <summary>
            /// Storage of finite real-valued sequences in real domain [DFTI_REAL_REAL].
            /// </summary>
            REAL_STORAGE = 9,

            /// <summary>
            /// Storage of finite complex-valued sequences in conjugate-even domain [DFTI_COMPLEX_REAL].
            /// </summary>
            CONJUGATE_EVEN_STORAGE = 10,

            /// <summary>
            /// Placement of result [DFTI_INPLACE].
            /// </summary>
            PLACEMENT = 11,

            /// <summary>
            /// Generalized strides for input data layout [tigth, row-major for C].
            /// </summary>
            INPUT_STRIDES = 12,

            /// <summary>
            /// Generalized strides for output data layout [tight, row-major for C].
            /// </summary>
            OUTPUT_STRIDES = 13,

            /// <summary>
            /// Distance between first input elements for multiple transforms [0].
            /// </summary>
            INPUT_DISTANCE = 14,

            /// <summary>
            /// Distance between first output elements for multiple transforms [0].
            /// </summary>
            OUTPUT_DISTANCE = 15,

            /// <summary>
            /// Effort spent in initialization [DFTI_MEDIUM]
            /// DFTI_INITIALIZATION_EFFORT = 16, ## NOT IMPLEMENTED
            /// Use of workspace during computation [DFTI_ALLOW].
            /// </summary>
            WORKSPACE = 17,

            /// <summary>
            /// Ordering of the result [DFTI_ORDERED].
            /// </summary>
            ORDERING = 18,

            /// <summary>
            /// Possible transposition of result [DFTI_NONE].
            /// </summary>
            TRANSPOSE = 19,

            /// <summary>
            /// User-settable descriptor name [""]
            /// This is DEPRECATED.
            /// </summary>
            DESCRIPTOR_NAME = 20,

            /// <summary>
            /// Packing format for DFTI_COMPLEX_REAL storage of finite
            /// conjugate-even sequences [DFTI_CCS_FORMAT].
            /// </summary>
            PACKED_FORMAT = 21,

            /// <summary>
            /// Commit status of the descriptor - R/O parameter.
            /// </summary>
            COMMIT_STATUS = 22,

            /// <summary>
            /// Version string for this DFTI implementation - R/O parameter.
            /// </summary>
            VERSION = 23,

            /// <summary>
            /// Number of user threads that share the descriptor [1].
            /// </summary>
            NUMBER_OF_USER_THREADS = 26,

            /// <summary>
            /// Limit the number of threads used by this descriptor [0 = don't care].
            /// </summary>
            THREAD_LIMIT = 27,

            /// <summary>
            /// Possible input data destruction [DFTI_AVOID = prevent input data].
            /// </summary>
            DESTROY_INPUT = 28,
        }

        /// <summary>
        /// Values of the descriptor configuration parameters.
        /// </summary>
        public enum CONFIG_VALUE : int
        {
            /* DFTI_COMMIT_STATUS */
            COMMITTED = 30,
            UNCOMMITTED = 31,

            /* DFTI_FORWARD_DOMAIN */
            COMPLEX = 32,
            REAL = 33,
            /* DFTI_CONJUGATE_EVEN = 34,   ## NOT IMPLEMENTED */

            /* DFTI_PRECISION */
            SINGLE = 35,
            DOUBLE = 36,

            /* DFTI_FORWARD_SIGN */
            /* DFTI_NEGATIVE = 37,         ## NOT IMPLEMENTED */
            /* DFTI_POSITIVE = 38,         ## NOT IMPLEMENTED */

            /* DFTI_COMPLEX_STORAGE and DFTI_CONJUGATE_EVEN_STORAGE */
            COMPLEX_COMPLEX = 39,
            COMPLEX_REAL = 40,

            /* DFTI_REAL_STORAGE */
            REAL_COMPLEX = 41,
            REAL_REAL = 42,

            /* DFTI_PLACEMENT */
            INPLACE = 43,          /* Result overwrites input */
            NOT_INPLACE = 44,      /* Have another place for result */

            /* DFTI_INITIALIZATION_EFFORT */
            /* DFTI_LOW = 45,              ## NOT IMPLEMENTED */
            /* DFTI_MEDIUM = 46,           ## NOT IMPLEMENTED */
            /* DFTI_HIGH = 47,             ## NOT IMPLEMENTED */

            /* DFTI_ORDERING */
            ORDERED = 48,
            BACKWARD_SCRAMBLED = 49,
            /* DFTI_FORWARD_SCRAMBLED = 50, ## NOT IMPLEMENTED */

            /* Allow/avoid certain usages */
            ALLOW = 51,            /* Allow transposition or workspace */
            AVOID = 52,
            NONE = 53,

            /* DFTI_PACKED_FORMAT (for storing congugate-even finite sequence
               in real array) */
            CCS_FORMAT = 54,       /* Complex conjugate-symmetric */
            PACK_FORMAT = 55,      /* Pack format for real DFT */
            PERM_FORMAT = 56,      /* Perm format for real DFT */
            CCE_FORMAT = 57,        /* Complex conjugate-even */
        }

        /// <summary>
        /// DFTI DftiCreateDescriptor wrapper.
        /// </summary>
        public static int DftiCreateDescriptor(
            ref IntPtr desc,
            DFTI.CONFIG_VALUE precision,
            DFTI.CONFIG_VALUE domain,
            int dimension,
            int length)
        {
            try
            {
                return DFTINative.DftiCreateDescriptor(ref desc, (int)precision, (int)domain, dimension, length);
            }
            catch (Exception)
            {
                return DFTI.MKL_INTERNAL_ERROR;
            }
        }

        /// <summary>
        /// DFTI DftiFreeDescriptor wrapper.
        /// </summary>
        public static int DftiFreeDescriptor(ref IntPtr desc)
        {
            return DFTINative.DftiFreeDescriptor(ref desc);
        }

        /// <summary>
        /// DFTI DftiSetValue wrapper.
        /// </summary>
        public static int DftiSetValue(IntPtr desc, DFTI.CONFIG_PARAM config_param, int config_val)
        {
            return DFTINative.DftiSetValue(desc, (int)config_param, config_val);
        }

        /// <summary>
        /// DFTI DftiSetValue wrapper.
        /// </summary>
        public static int DftiSetValue(IntPtr desc, DFTI.CONFIG_PARAM config_param, DFTI.CONFIG_VALUE config_val)
        {
            return DFTINative.DftiSetValue(desc, (int)config_param, (int)config_val);
        }

        /// <summary>
        /// DFTI DftiSetValue wrapper.
        /// </summary>
        public static int DftiSetValue(IntPtr desc, DFTI.CONFIG_PARAM config_param, double config_val)
        {
            return DFTINative.DftiSetValue(desc, (int)config_param, config_val);
        }

        /// <summary>
        /// DFTI DftiGetValue wrapper.
        /// </summary>
        public static int DftiGetValue(IntPtr desc, DFTI.CONFIG_PARAM config_param, ref double config_val)
        {
            return DFTINative.DftiGetValue(desc, (int)config_param, ref config_val);
        }

        /// <summary>
        /// DFTI DftiGetValue wrapper.
        /// </summary>
        public static int DftiGetValue(IntPtr desc, DFTI.CONFIG_PARAM config_param, ref int config_val)
        {
            return DFTINative.DftiGetValue(desc, (int)config_param, ref config_val);
        }

        /// <summary>
        /// DFTI DftiCommitDescriptor wrapper.
        /// </summary>
        public static int DftiCommitDescriptor(IntPtr desc)
        {
            return DFTINative.DftiCommitDescriptor(desc);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper.
        /// </summary>
        public static int DftiComputeForward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper.
        /// </summary>
        public static int DftiComputeForward(IntPtr desc, double[] x)
        {
            return DFTINative.DftiComputeForward(desc, x);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper.
        /// </summary>
        public static int DftiComputeForward(IntPtr desc, [In] float[] x_in, [Out] float[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper.
        /// </summary>
        public static int DftiComputeForward(IntPtr desc, [In] Complex[] x_in, [In] Complex[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeForward wrapper.
        /// </summary>
        public static int DftiComputeForward(IntPtr desc, [In] ComplexF[] x_in, [In] ComplexF[] x_out)
        {
            return DFTINative.DftiComputeForward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper.
        /// </summary>
        public static int DftiComputeBackward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper.
        /// </summary>
        public static int DftiComputeBackward(IntPtr desc, [In] float[] x_in, [Out] float[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper.
        /// </summary>
        public static int DftiComputeBackward(IntPtr desc, [In] Complex[] x_in, [Out] Complex[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }

        /// <summary>
        /// DFTI DftiComputeBackward wrapper.
        /// </summary>
        public static int DftiComputeBackward(IntPtr desc, [In] ComplexF[] x_in, [Out] ComplexF[] x_out)
        {
            return DFTINative.DftiComputeBackward(desc, x_in, x_out);
        }
    }
}
