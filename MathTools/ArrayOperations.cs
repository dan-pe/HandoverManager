namespace MathTools
{
    #region Usings

    using System;

    #endregion

    public static class ArrayOperations
    {
        #region Public Methods

        /// <summary>
        /// Multiplies two arrays.
        /// </summary>
        /// <param name="arrayA">
        /// Array A.
        /// </param>
        /// <param name="arrayB">
        /// Array B.
        /// </param>
        /// <returns>
        /// The <see cref="double[,]"/>
        /// Result array.
        /// </returns>
        public static double[,] MultiplyArrays(double[,] arrayA, double[,] arrayB)
        {
            int rowsA = arrayA.GetLength(0);
            int columnsA = arrayA.GetLength(1);
            int columnsB = arrayB.GetLength(1);

            double[,] resultArray = new double[rowsA, columnsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < columnsB; j++)
                {
                    var tempCellResult = 0.0d;
                    for (int k = 0; k < columnsA; k++)
                    {
                        tempCellResult += arrayA[i, k] * arrayB[k, j];
                    }
                    resultArray[i, j] = tempCellResult;
                }
            }

            return resultArray;
        }

        /// <summary>
        /// Sums rows in two dimensional array.
        /// </summary>
        /// <param name="inputArray">
        /// Input array.
        /// </param>
        /// <returns>
        /// Vector of summed rows.
        /// </returns>
        public static double[] SumRows(double[,] inputArray)
        {
            double[] resultArray = new double[inputArray.GetLength(0)];

            int rowsA = inputArray.GetLength(0);
            int columnsA = inputArray.GetLength(1);

            for (int i = 0; i < rowsA; i++)
            {
                double tempCellResult = 0;
                for (int j = 0; j < columnsA; j++)
                {
                    tempCellResult += inputArray[i, j];
                }

                resultArray[i] = tempCellResult;
            }

            return resultArray;
        }

        public static double[] MultiplyByInversionofSum(double[] inputVector)
        {
            int vectorRowsNumber = inputVector.GetLength(0);
            double vectorElementsSum = 0.0d;
            double[] resultVector = new double[vectorRowsNumber];

            for (int i = 0; i < vectorRowsNumber; i++)
            {
                vectorElementsSum += inputVector[i];
            }

            for (int i = 0; i < vectorRowsNumber; i++)
            {
                resultVector[i] = inputVector[i] * (Math.Pow(vectorElementsSum, -1));
            }

            return resultVector;
        }

        #endregion
    }
}
