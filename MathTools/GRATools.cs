using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTools
{
    public static class GRATools
    {
        static float NormalizateSmallerTheBetter(float highestValue, float actualValue, float lowerValue)
        {
            return (highestValue - actualValue)/(highestValue - lowerValue);
        }

        static float NormalizateLargerTheBetter(float highestValue, float actualValue, float lowerValue)
        {
            return (actualValue - lowerValue) / (highestValue - lowerValue);
        }
    }
}
