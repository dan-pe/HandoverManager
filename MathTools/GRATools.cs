using System;
using System.Collections.Generic;
using System.Linq;

namespace MathTools
{
    public static class GraTools
    {
        /// <summary>
        /// Normalizes passed value, by larger the better pattern.
        /// </summary>
        /// <param name="passedValues">
        /// Passed values collection.
        /// </param>
        /// <param name="value">
        /// Value to normalize.
        /// </param>
        /// <returns>
        /// <see cref="double"/>
        /// Normalized value.
        /// </returns>
        public static double NormalizeLargerTheBetter(IEnumerable<double> passedValues, double value)
        {
            var enumerable = passedValues as double[] ?? passedValues.ToArray();
            var result = (value - enumerable.Min()) / (enumerable.Max() - enumerable.Min());

            if (Double.IsNaN(result))
            {
                return 1;
            }

            return result;
        }

        /// <summary>
        /// Normalizes passed value, by smaller the better pattern.
        /// </summary>
        /// <param name="passedValues">
        /// Passed values collection.
        /// </param>
        /// <param name="value">
        /// Value to normalize.
        /// </param>
        /// <returns>
        /// <see cref="double"/>
        /// Normalized value.
        /// </returns>
        public static double NormalizeSmallerTheBetter(IEnumerable<double> passedValues, double value)
        {
            var enumerable = passedValues as double[] ?? passedValues.ToArray();
            var result = (enumerable.Max() - value) / (enumerable.Max() - enumerable.Min());

            if (Double.IsNaN(result))
            {
                return 1;
            }

            return result;
        }
    }
}