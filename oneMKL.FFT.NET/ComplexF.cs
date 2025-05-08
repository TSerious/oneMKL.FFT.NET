using System;

namespace oneMKL.FFT.NET
{
    /// <summary>
    /// Complex number with single precision.
    /// </summary>
    public readonly struct ComplexF
    {
        /// <summary>
        /// Creates the number 0+0i.
        /// </summary>
        public static readonly ComplexF Zero = new ComplexF(0.0F, 0.0F);

        /// <summary>
        /// Creates the number 1+0i.
        /// </summary>
        public static readonly ComplexF One = new ComplexF(1.0F, 0.0F);

        /// <summary>
        /// Creates the number 0+1i.
        /// </summary>
        public static readonly ComplexF ImaginaryOne = new ComplexF(0.0F, 1.0F);

        private readonly float real;
        private readonly float imaginary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexF"/> struct.
        /// Constructor to create a complex number with rectangular co-ordinates.
        /// </summary>
        /// <param name="real">Sets <see cref="Real"/>.</param>
        /// <param name="imaginary">Sets <see cref="Imaginary"/>.</param>
        public ComplexF(float real, float imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        /// <summary>
        /// Gets the real part of the complex number.
        /// </summary>
        public float Real => this.real;

        /// <summary>
        /// Gets the imaginary part of the complex number.
        /// </summary>
        public float Imaginary => this.imaginary;

        public static ComplexF operator -(ComplexF value)
        {
            return new ComplexF(-value.real, -value.imaginary);
        }

        public static ComplexF operator +(ComplexF left, ComplexF right)
        {
            return new ComplexF(left.real + right.real, left.imaginary + right.imaginary);
        }

        public static ComplexF operator -(ComplexF left, ComplexF right)
        {
            return new ComplexF(left.real - right.real, left.imaginary - right.imaginary);
        }

        public static ComplexF operator *(ComplexF left, ComplexF right)
        {
            // Multiplication:  (a + bi)(c + di) = (ac -bd) + (bc + ad)i
            float result_Realpart = (left.real * right.real) - (left.imaginary * right.imaginary);
            float result_Imaginarypart = (left.imaginary * right.real) + (left.real * right.imaginary);
            return new ComplexF(result_Realpart, result_Imaginarypart);
        }

        public static ComplexF operator /(ComplexF left, ComplexF right)
        {
            // Division : Smith's formula.
            float a = left.real;
            float b = left.imaginary;
            float c = right.real;
            float d = right.imaginary;

            if (Math.Abs(d) < Math.Abs(c))
            {
                double doc = d / c;
                return new ComplexF((float)((a + (b * doc)) / (c + (d * doc))), (float)((b - (a * doc)) / (c + (d * doc))));
            }
            else
            {
                double cod = c / d;
                return new ComplexF((float)((b + (a * cod)) / (d + (c * cod))), (float)((-a + (b * cod)) / (d + (c * cod))));
            }
        }

        /// <summary>
        /// Creates a complex number from polar coordinates.
        /// </summary>
        /// <param name="magnitude">The magnituede of the complex number.</param>
        /// <param name="phase">The phase of the complex number.</param>
        /// <returns>The number.</returns>
        public static ComplexF FromPolarCoordinates(double magnitude, double phase)
        {
            return new ComplexF((float)(magnitude * Math.Cos(phase)), (float)(magnitude * Math.Sin(phase)));
        }

        /// <summary>
        /// Negates the given number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The negated number.</returns>
        public static ComplexF Negate(ComplexF value)
        {
            return -value;
        }

        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="left">The first number.</param>
        /// <param name="right">The second number.</param>
        /// <returns>The sum of <paramref name="left"/> + <paramref name="right"/>.</returns>
        public static ComplexF Add(ComplexF left, ComplexF right)
        {
            return left + right;
        }

        /// <summary>
        /// Substracts two numbers.
        /// </summary>
        /// <param name="left">The first number.</param>
        /// <param name="right">The second number.</param>
        /// <returns>The difference of <paramref name="left"/> - <paramref name="right"/>.</returns>
        public static ComplexF Subtract(ComplexF left, ComplexF right)
        {
            return left - right;
        }

        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="left">The first number.</param>
        /// <param name="right">The second number.</param>
        /// <returns>The multiply of <paramref name="left"/> * <paramref name="right"/>.</returns>
        public static ComplexF Multiply(ComplexF left, ComplexF right)
        {
            return left * right;
        }

        /// <summary>
        /// Devides two numbers.
        /// </summary>
        /// <param name="dividend">The first number.</param>
        /// <param name="divisor">The second number.</param>
        /// <returns>The result of <paramref name="dividend"/> / <paramref name="divisor"/>.</returns>
        public static ComplexF Divide(ComplexF dividend, ComplexF divisor)
        {
            return dividend / divisor;
        }
    }
}