namespace HandoverAlgorithmBase.NovelAlgorithm
{
    #region Usings

    #endregion

    /// <summary>
    /// TODO: Replace this class with more generic mechanism
    /// User profiles for input preferences.
    /// </summary>
    public static class NovelNetworkProfiles
    {
        #region Public Static Methods

        /// <summary>
        /// Returns some profile wages array.
        /// </summary>
        /// <returns></returns>
        public static double[,] GetSomeProfile()
        {
            return new double[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
        }

        /// <summary>
        /// Returns other profile wages array.
        /// </summary>
        /// <returns></returns>
        public static double[,] GetOtherProfile()
        {
            return new double[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }

            };
        }

        /// <summary>
        /// Returns odd profile wages array.
        /// </summary>
        /// <returns></returns>
        public static double[,] GetOddProfile()
        {
            return new double[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }

            };
        }

        #endregion
    }

    #region User Profiles Enum

    /// <summary>
    /// Novel Network Profiles
    /// </summary>
    public enum NovelNetworkProfile
    {
        BalancedProfile,
        MaxEfficency,
        Connectivity
    }

    #endregion
}
