using System.ComponentModel;

namespace Profiler
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    #endregion

    public sealed class ProfileManager
    {
        #region Private Static Fields

        /// <summary>
        /// The profiler.
        /// </summary>
        private static volatile ProfileManager _profileManager;

        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The stored user profile.
        /// </summary>
        public List<UserProfile> StoredUserProfiles { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        private ProfileManager()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The profiler instance.
        /// </summary>
        public static ProfileManager Instance
        {
            get
            {
                if (_profileManager == null)
                    lock (Locker)
                    {
                        if (_profileManager == null)
                            _profileManager = new ProfileManager();
                    }

                return _profileManager;
            }
        }

        #endregion

        #region Public Methods

        ///// <summary>
        ///// Sets the user profile.
        ///// </summary>
        ///// <param name="userProfile">
        ///// The user profile to set.
        ///// </param>
        //public void SetProfile(UserProfile userProfile)
        //{
        //    this._userProfile = userProfile;
        //}

        /// <summary>
        /// Loads the stored user profile.
        /// </summary>
        /// <returns>
        /// The stored user profile.
        /// </returns>
        public UserProfile GetProfileByName(string profileName)
        {
            return this.StoredUserProfiles.
                FirstOrDefault( profile => profile.Name == profileName);
        }

        /// <summary>
        /// Loads user profiles from file.
        /// </summary>
        /// <param name="pathTofile">
        /// Path to file.
        /// </param>
        /// <returns>
        /// List of loaded user profiles.
        /// </returns>
        public List<UserProfile> LoadFromFile(string pathTofile)
        {
            var loadedUserProfiles = new List<UserProfile>();
            var fileConent = File.ReadAllText(pathTofile);
            var sections = fileConent.Split(new[] { "\r\n\r\n" },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var section in sections)
            {
                var sectionHeader =
                    Regex.Matches(section, @"\w{1,}\r")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList()
                        .First();

                var profileStringValues = Regex.Matches(section, @"[0-9]+(\/[0-9]?)?")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToArray();

                var profileDoubleValues = Array.ConvertAll(profileStringValues, UserProfileConverter);

                loadedUserProfiles.Add(
                    new UserProfile(
                        sectionHeader,
                        MathTools.ArrayOperations.ConvertToTwoDimensionalArray(profileDoubleValues)));
            }

            return loadedUserProfiles;
        }

        private static double UserProfileConverter(string input)
        {
            if (input.Length == 1)
            {
                return double.Parse(input);
            }
            else
            {
                var one = double.Parse(input[0].ToString());
                var twp = double.Parse(input[2].ToString());

                return  one / twp;
            }
        }



        #endregion
    }

  
}