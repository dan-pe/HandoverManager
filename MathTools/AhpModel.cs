﻿
namespace MathTools
{
    #region Usings

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// The Analytic Hierarchy Model.
    /// </summary>
    public class AhpModel
    {

        #region Private Fields

        private double[,] InputWeights { get; }

        private int NumberOfCriterias { get; }

        private const double Epsilion = 0.001d;

        #endregion

        #region Constructors

        public AhpModel(double[,] inputWeights)
        {
            InputWeights = inputWeights;
            NumberOfCriterias = (int)Math.Sqrt(inputWeights.Length);
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

        /// <summary>
        /// Computes normalized array vector (versor).
        /// </summary>
        /// <returns>
        /// </returns>
        public static double[] ComputeNormalizedVector(double[,] inputWeightsArray)
        {
            bool isSmallerThanEpsilion = false;

            // Step 1: 
            // Multiply matrix.
            double[,] stepOneOriginal = ArrayOperations.MultiplyArrays(inputWeightsArray, inputWeightsArray);

            // Step2:
            // Sum rows in matrix.
            double[] stepTwoOriginal = ArrayOperations.SumRows(stepOneOriginal);

            // Step3:
            // Multiply vector by inversion of sum.
            double[] stepThreeOriginal = ArrayOperations.MultiplyByInversionofSum(stepTwoOriginal);

            while (!isSmallerThanEpsilion)
            {
                // Step 1: 
                // Multiply matrix.
                double[,] stepOne = ArrayOperations.MultiplyArrays(stepOneOriginal, stepOneOriginal);

                // Step2:
                // Sum rows in matrix.
                double[] stepTwo = ArrayOperations.SumRows(stepOne);

                // Step3:
                // Multiply vector by inversion of sum.
                double[] stepThree = ArrayOperations.MultiplyByInversionofSum(stepTwo);

                isSmallerThanEpsilion = IsDeltaSmallerThenEpsilion(stepThreeOriginal, stepThree, Epsilion);

                stepThreeOriginal = stepThree;
                stepOneOriginal = stepOne;

            }

            return stepThreeOriginal;
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

        private static bool IsDeltaSmallerThenEpsilion(double[] inputVectorA, double[] inputVectorB, double epsilion)
        {
            int vectorLengthA = inputVectorA.GetLength(0);
            bool isSmaller = true;

            for (int i = 0; i < vectorLengthA; i++)
            {
                double delta = inputVectorA[i] - inputVectorB[i];

                if (delta > epsilion)
                {
                    isSmaller = false;
                }
            }

            return isSmaller;
        }

        #endregion
    }
}