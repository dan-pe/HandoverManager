using System;

namespace Profiler
{
    public class UserProfile
    {
        #region Private Fields

        /// <summary>
        /// The profile name.
        /// </summary>
        private string _profileName;

        /// <summary>
        /// The user profile weights array.
        /// </summary>
        private int[,] _userProfileWeights;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        public UserProfile(string profileName, int[,] userProfileWeights)
        {
            if (!IsValidProfile(userProfileWeights))
            {
                throw new Exception("User profile is not valid.");
            }

            this._profileName = profileName;
            this._userProfileWeights = userProfileWeights;
        }

        #endregion


        #region Private Methods

        private bool IsValidProfile(int[,] InputWeights)
        {
            // NxN 2dimensional matrix

            if (!InputWeights.Rank.Equals(2) ||
                !InputWeights.GetLength(0).Equals(InputWeights.GetLength(1)))
            {
                return false;
            }

            // Diagonal values are equal to 1.
            for (int i = 0; i < InputWeights.GetLength(0) - 1; i++)
            {
                if (!((int)InputWeights[i, i]).Equals(1))
                {
                    return false;
                }
            }

            // Pair-wise comparison
            // Is a[i,j] == 1/a[j,i]
            for (int i = 0; i < InputWeights.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < (InputWeights.GetLength(0) - 1 - i); j++)
                {
                    if (!InputWeights[i, j].Equals(1 / InputWeights[j, i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}