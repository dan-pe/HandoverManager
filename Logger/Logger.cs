using System;
using System.IO;
using System.Windows.Controls;

namespace Logger
{
    #region Usings

    #endregion

    public class Logger
    {
        #region Private Fields

        private static Logger _logger;

        private ListBox _logBox;

        //private

        #endregion

        #region Constructors and Destructor

        private Logger(ListBox logBox)
        {
            _logBox = logBox;
            _logger = this;
        }

        #endregion

        #region Publics Methods

        public static Logger GetLoggerInstance(ListBox logBox)
        {
            return _logger ?? (_logger = new Logger(logBox));
        }

        public void AddMessage(string message)
        {
            var listBoxItem = new ListBoxItem {Content = message};
            _logBox.Items.Add(listBoxItem);
        }

        #endregion
    }
}
