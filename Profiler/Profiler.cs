using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Profiler
{
    #region Usings

    #endregion

    public sealed class Profiler
    {
        #region Private Static Fields

        /// <summary>
        /// The profiler.
        /// </summary>
        private static volatile Profiler _profiler;

        /// <summary>
        /// The locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The stored user profile.
        /// </summary>
        private UserProfile _userProfile;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        private Profiler()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The profiler instance.
        /// </summary>
        public static Profiler Instance
        {
            get
            {
                if (_profiler == null)
                    lock (Locker)
                    {
                        if (_profiler == null)
                            _profiler = new Profiler();
                    }

                return _profiler;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the user profile.
        /// </summary>
        /// <param name="userProfile">
        /// The user profile to set.
        /// </param>
        public void SetProfile(UserProfile userProfile)
        {
            this._userProfile = userProfile;
        }

        /// <summary>
        /// Loads the stored user profile.
        /// </summary>
        /// <returns>
        /// The stored user profile.
        /// </returns>
        public UserProfile GetCurrentProfile()
        {
            return this._userProfile;
        }

        public IEnumerable<string> LoadFromFile(string pathTofile)
        {
            var loadedUserProfiles = new List<UserProfile>();
            var fileConent = File.ReadAllText(pathTofile);

            var sections =
                Regex.Matches(fileConent, @"[\w{*}]")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();

            foreach (var section in sections)
            {
                //var subsection = section.Split(
                //    new[] { "[", ";"},
                //    StringSplitOptions.None);
                var numberRegex = "";

                var profileValues = Regex.Matches(section, numberRegex)
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToArray();


            }

            return sections;
        }

        #endregion
    }
}