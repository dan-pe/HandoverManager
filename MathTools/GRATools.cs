﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace MathTools
{
    public static class GraTools
    {
        #region Public Methods

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
        /// <see cref="float"/>
        /// Normalized value.
        /// </returns>
        public static double NormalizateSmallerTheBetter(IEnumerable<double> passedValues, double value)
        {
            return (passedValues.Max() - value) / (passedValues.Max() - passedValues.Min());
        }

        /// <summary>
        /// Normalizes passed value, by larger the better pattern.
        /// </summary>
        /// <param name="passedValues">
        /// Passed values collection.
        /// </param>
        /// <param name="value"></param>
        /// <returns>
        /// <see cref="float"/>
        /// Normalized value.
        /// </returns>
        public static double NormalizeLargerTheBetter(IEnumerable<double> passedValues, double value)
        {
            return (value - passedValues.Min()) / (passedValues.Max() - passedValues.Min());
        }

        #endregion
    }

}

