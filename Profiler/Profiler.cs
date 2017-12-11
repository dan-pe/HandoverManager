namespace Profiler
{
    #region Usings

    #endregion

    public sealed class Profiler
    {
        #region Private Static Fields

        private static volatile Profiler _profiler;

        private static readonly object Locker = new object();

        private UserProfile _userProfile;

        #endregion

        #region Constructors

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

        public void SetProfile(UserProfile userProfile)
        {
            this._userProfile = userProfile;
        }

        public UserProfile GetCurrentProfile()
        {
            return this._userProfile;
        }

        #endregion
    }

    public class UserProfile
    {
        private string profileName;

        private int[,] userProfileWeights;
    }
}