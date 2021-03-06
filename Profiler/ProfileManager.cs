﻿namespace Profiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public sealed class ProfileManager
    {
        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The profiler.
        /// </summary>
        private static volatile ProfileManager _profileManager;

        /// <summary>
        ///
        /// </summary>
        private ProfileManager()
        {
        }

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

        public string SelectedUserProfileName { get; set; }

        /// <summary>
        /// The stored user profile.
        /// </summary>
        public List<UserProfile> StoredUserProfiles { get; set; }

        /// <summary>
        /// Loads the stored user profile.
        /// </summary>
        /// <returns>
        /// The stored user profile.
        /// </returns>
        public UserProfile GetProfileByName(string profileName)
        {
            return this.StoredUserProfiles.
                FirstOrDefault(profile => profile.Name == profileName);
        }

        public UserProfile GetSelectedProfile()

        {
            return Instance.GetProfileByName(this.SelectedUserProfileName);
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
                        .First()
                        .Replace("\r", string.Empty)
                        .Replace("\n", string.Empty);

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
            this.StoredUserProfiles = loadedUserProfiles;
            return loadedUserProfiles;
        }

        /// <summary>
        /// Sets the user profile.
        /// </summary>
        /// <param name="userProfile">
        /// The user profile to set.
        /// </param>
        public void SetProfile(string userProfileName)
        {
            this.SelectedUserProfileName = userProfileName;
        }

        private static double UserProfileConverter(string input)
        {
            if (input.Length == 1)
            {
                return double.Parse(input);
            }

            var one = double.Parse(input[0].ToString());
            var twp = double.Parse(input[2].ToString());

            return one / twp;
        }
    }
}