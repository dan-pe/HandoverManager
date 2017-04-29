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
    public class AHP_Tools
    {

        #region Private Fields

        private float[,] InputWeights { get; set; }

        #endregion

        #region Constructors

        public AHP_Tools(float[,] inputWeights)
        {
            this.InputWeights = inputWeights;

           
        }

        #endregion

        #region Public Methods

        public float[] GetOutputWeights()
        {
            var costam = InputWeights;
            for (int i = 0; i < Math.Sqrt(costam.Length); i++)
            {
                costam = Normalize(costam);
            }
            return null;
        }

        private static float[,] Normalize(float[,] inputArray)
        {
            return inputArray;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}  
