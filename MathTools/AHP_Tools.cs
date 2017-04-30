#region Usings

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace MathTools
{
    public class AHPModel
    {

        #region Private Fields

        private double[,] InputWeights { get; set; }

        private int NumberOfCriterias { get; }

        #endregion

        #region Constructors

        public AHPModel(double[,] inputWeights)
        {
            this.InputWeights = inputWeights;
            this.NumberOfCriterias = (int)Math.Sqrt(inputWeights.Length);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes AHP weights.
        /// </summary>
        /// <returns>
        /// The <see cref="double[]"/>
        /// Output weights computed with AHP method.
        /// </returns>
        public double[] GetOutputWeights()
        {
            var meansVector = new double[NumberOfCriterias];
            var criteriaVector = new double[NumberOfCriterias];
            var weightCoefficients = new double[NumberOfCriterias];
            
            for (int i = 0; i < NumberOfCriterias; i++)
            {
                for (int j = 0; j < NumberOfCriterias; j++)
                {
                    criteriaVector[j] = InputWeights[i, j];
                }
                meansVector[i] = GeometricMean(criteriaVector);
            }

            for (int i = 0; i < NumberOfCriterias; i++)
            {
                weightCoefficients[i] = meansVector[i] / meansVector.Sum();
            }
            
            return weightCoefficients;
        }

        private static double[,] Normalize(double[,] inputArray)
        {
            return inputArray;
        }

        private static double GeometricMean(double[] inputVector)
        {
            double quotient = inputVector[0];

            for (int i = 1; i < inputVector.Length; i++)
            {
                quotient *= inputVector[i];
            }
            return Math.Pow(quotient, 1/inputVector.Length);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}  
