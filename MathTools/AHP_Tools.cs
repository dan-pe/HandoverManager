
namespace MathTools
{
    #region Usings

    using System;
    using System.Linq;

    #endregion

    public class AhpModel
    {

        #region Private Fields

        private double[,] InputWeights { get; }

        private int NumberOfCriterias { get; }

        #endregion

        #region Constructors

        public AhpModel(double[,] inputWeights)
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculates geometric mean of input vector.
        /// </summary>
        /// <param name="inputVector"></param>
        /// <returns>
        /// The <see cref="double"/>
        /// Geometric mean of input vector.
        /// </returns>
        private static double GeometricMean(double[] inputVector)
        {
            var quotient = inputVector[0];

            for (int i = 1; i < inputVector.Length; i++)
            {
                quotient *= inputVector[i];
            }

            return Math.Pow(quotient, 1 / inputVector.Length);
        }

        #endregion
    }
}
