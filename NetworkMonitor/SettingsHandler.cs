using System.Collections.Generic;

namespace NetworkMonitors
{
    public class SettingsHandler
    {
        #region Private Fields

        private static SettingsHandler _settingsHandler;

        private readonly List<string> _serverList;

        private int _bufferSizeInBytes;

        private int _pingCount;

        private int _pingTimeoutInMsec;

        #endregion

        #region Constructors

        private SettingsHandler()
        {
            _serverList = new List<string>();
        }

        #endregion

        public List<string> ServerList
        {
            get
            {
                if (_serverList != null)
                {
                    return _serverList;
                }
                return new List<string>();
            }
        }

        public int PingTimeoutInMsec
        {
            get
            {
                if (_bufferSizeInBytes != null)
                {
                    return _bufferSizeInBytes;
                }
                return 0;
            }

            set { _bufferSizeInBytes = value; }
        }

        public int BufferSizeInBytes
        {
            get
            {
                if (_pingTimeoutInMsec != null)
                {
                    return _pingTimeoutInMsec;
                }
                return 0;
            }

            set { _pingTimeoutInMsec = value; }
        }

        public int PingCount
        {
            get
            {
                if (_pingCount != null)
                {
                    return _pingCount;
                }
                return 0;
            }

            set { _pingCount = value; }
        }

        public static SettingsHandler GetInstance()
        {
            return _settingsHandler ?? (_settingsHandler = new SettingsHandler());
        }
    }
}
