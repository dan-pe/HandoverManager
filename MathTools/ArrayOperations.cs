namespace MathTools
{
    using System;

    public static class ArrayOperations
    {
        /// <summary>
        /// Converts single dimension array of doubles to two dimensional array.
        /// </summary>
        /// <param name="inputArray">
        /// The input array.
        /// </param>
        /// <returns>
        /// Two dimensional representation of input array.
        /// </returns>
        public static double[,] ConvertToTwoDimensionalArray(double[] inputArray)
        {
            var rowsNumber = (int)Math.Sqrt(inputArray.Length);

            var resultArray = new double[rowsNumber, rowsNumber];

            int indexer = 0;

            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < rowsNumber; j++)
                {
                    resultArray[i, j] = inputArray[indexer];
                    indexer++;
                }
            }

            return resultArray;
        }

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
        /// Multiplies vector by inversion of sum.
        /// </summary>
        /// <param name="inputVector">
        /// The input vector
        /// </param>
        /// <returns>
        /// The result of multiplying.
        /// </returns>
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
    }
}