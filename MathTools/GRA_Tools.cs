#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace MathTools
{
    public static class GRATools
    {
        #region Public Methods

        public static float NormalizateSmallerTheBetter(IEnumerable<float> passedValues, float value)
        {
            var enumerable = passedValues as float[] ?? passedValues.ToArray();
            return (enumerable.Max() - value) / (enumerable.Max() - enumerable.Min());
        }

        public static float NormalizeLargerTheBetter(IEnumerable<float> passedValues, float value)
        {
            var enumerable = passedValues as float[] ?? passedValues.ToArray();
            return (value - enumerable.Min()) / (enumerable.Max() - enumerable.Min());
        }

        #endregion

    }

}

