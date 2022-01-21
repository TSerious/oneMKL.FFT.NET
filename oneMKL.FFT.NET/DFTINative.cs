using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace oneMKL.FFT.NET
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class DFTINative
    {
        /// <summary>
        /// DFTI native DftiCreateDescriptor declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiCreateDescriptor(ref IntPtr desc, int precision, int domain, int dimention, int length);

        /// <summary>
        /// DFTI native DftiCommitDescriptor declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiCommitDescriptor(IntPtr desc);

        /// <summary>
        /// DFTI native DftiFreeDescriptor declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiFreeDescriptor(ref IntPtr desc);

        /// <summary>
        /// DFTI native DftiSetValue declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiSetValue(IntPtr desc, int config_param, int config_val);

        /// <summary>
        /// DFTI native DftiSetValue declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiSetValue(IntPtr desc, int config_param, double config_val);

        /// <summary>
        /// DFTI native DftiGetValue declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiGetValue(IntPtr desc, int config_param, ref double config_val);

        /// <summary>
        /// DFTI native DftiComputeForward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out);

        /// <summary>
        /// DFTI native DftiComputeForward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc, double[] x);

        /// <summary>
        /// DFTI native DftiComputeForward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc, [In] float[] x_in, [Out] float[] x_out);

        /// <summary>
        /// DFTI native DftiComputeForward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc, [In] Complex[] x_in, [Out] Complex[] x_out);

        /// <summary>
        /// DFTI native DftiComputeForward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeForward(IntPtr desc, [In] ComplexF[] x_in, [Out] ComplexF[] x_out);

        /// <summary>
        /// DFTI native DftiComputeBackward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out);

        /// <summary>
        /// DFTI native DftiComputeBackward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc, [In] float[] x_in, [Out] float[] x_out);

        /// <summary>
        /// DFTI native DftiComputeBackward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc, [In] Complex[] x_in, [Out] Complex[] x_out);

        /// <summary>
        /// DFTI native DftiComputeBackward declaration.
        /// </summary>
        [DllImport("mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        internal static extern int DftiComputeBackward(IntPtr desc, [In] ComplexF[] x_in, [Out] ComplexF[] x_out);
    }
}
