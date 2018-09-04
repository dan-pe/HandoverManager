using System.Collections.Generic;

namespace NetworkManager
{
    public class ServerListHandler
    {
        #region Private Fields

        private static ServerListHandler _serverListHandler;

        private readonly List<string> _serverList;

        #endregion

        #region Constructors

        private ServerListHandler()
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

        public static ServerListHandler GetInstance()
        {
            return _serverListHandler ?? (_serverListHandler = new ServerListHandler());
        }
    }
}
