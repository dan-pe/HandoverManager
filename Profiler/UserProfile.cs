namespace Profiler
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using RadioNetworks;

    #endregion

    public class UserProfile
    {
        #region Private Fields

        /// <summary>
        /// The profile name.
        /// </summary>
        private string _profileName;

        /// <summary>
        /// The user profile weights.
        /// </summary>
        public Dictionary<Dictionary<string, string>, double> ProfileWeights; 

        #endregion

        #region Constructors

        public UserProfile(string sectionHeader, double[,] profileDoubleValues)
        {
            if (!this.IsValidProfile(profileDoubleValues))
            {
                throw new Exception("User profile is not valid.");
            }

            this._profileName = sectionHeader;
            this.ProfileWeights = RegisterDictionariesValues(profileDoubleValues);
        }

        #endregion


        #region Private Methods

        private bool IsValidProfile(double[,] inputWeights)
        {
            // NxN 2dimensional matrix

            if (!inputWeights.Rank.Equals(2) ||
                !inputWeights.GetLength(0).Equals(inputWeights.GetLength(1)))
            {
                return false;
            }

            // Diagonal values are equal to 1.
            for (int i = 0; i < inputWeights.GetLength(0) - 1; i++)
            {
                if (!((int)inputWeights[i, i]).Equals(1))
                {
                    return false;
                }
            }

            // Pair-wise comparison
            // Is a[i,j] == 1/a[j,i]
            for (int i = 0; i < inputWeights.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < (inputWeights.GetLength(0) - 1 - i); j++)
                {
                    if (!inputWeights[i, j].Equals(1 / inputWeights[j, i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Registers weights to parameter dictionary. 
        /// </summary>
        /// <param name="weightsArray">
        /// The weights array.
        /// </param>
        /// <returns>
        /// Dictionary with profile values.
        /// </returns>
        private static Dictionary<Dictionary<string, string>, double> RegisterDictionariesValues(double[,] weightsArray)
        {
            throw new NotImplementedException();

            Type t = typeof(NetworkParameters);

            PropertyInfo[] properties = t.GetProperties();

            var propertiesNames = new List<string>();

            foreach (var propertyInfo in properties)
            {
                propertiesNames.Add(propertyInfo.Name);
            }

            var helperDictionary = new Dictionary<string,string>();
            var registeredDictionary = new Dictionary<Dictionary<string, string>, double>();

            //TODO: sort out this dictionary issue ..

            foreach (var keyA in propertiesNames)
            {
                foreach (var keyB in propertiesNames)
                {
                    helperDictionary.Add(keyA, keyB);

                    registeredDictionary.Add(
                        helperDictionary(keyA,keyB), 
                        weightsArray.Rank);
                }
            }

        }

        #endregion
    }
}