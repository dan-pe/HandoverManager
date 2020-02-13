namespace Profiler
{
    using RadioNetworks;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class UserProfile
    {
        public UserProfile(string sectionHeader, double[,] profileDoubleValues)
        {
            //if (!this.IsValidProfile(profileDoubleValues))
            //{
            //    throw new Exception("User profile is not valid.");
            //}

            this.Name = sectionHeader;
            this.ProfileWeights = profileDoubleValues;
        }

        /// <summary>
        /// The profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The user profile weights.
        /// </summary>
        public double[,] ProfileWeights { get; set; }

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

            var helperDictionary = new Dictionary<string, string>();
            var registeredDictionary = new Dictionary<Dictionary<string, string>, double>();

            //TODO: sort out this dictionary issue ..

            foreach (var keyA in propertiesNames)
            {
                foreach (var keyB in propertiesNames)
                {
                    helperDictionary.Add(keyA, keyB);
                }
            }
        }

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
    }
}