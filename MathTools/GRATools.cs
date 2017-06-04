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
        public static float NormalizateSmallerTheBetter(IList<float> passedValues, float value)
        {
            return (passedValues.Max() - value)/(passedValues.Max() - passedValues.Min());
        }

        public static float NormalizeLargerTheBetter(IList<float> passedValues, float value)
        {
            return (value - passedValues.Min())/(passedValues.Max() - passedValues.Min());
        }
    }

}

