namespace HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public static int[,] GetSomeProfile()
        {
            return new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1 },
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
        SomeProfile,
        OtherProfile,
        OddProfile
    }

    #endregion
}
