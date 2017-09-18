namespace Logger
{
    #region Usings

    using System;
    using System.Windows.Controls;

    #endregion

    public class Logger
    {
        #region Private Fields

        /// <summary>
        /// The logger instance.
        /// </summary>
        private static Logger _logger;

        /// <summary>
        /// The log box.
        /// </summary>
        private static ListBox _logBox;

        //private

        #endregion

        #region Constructors and Destructor

        /// <summary>
        /// The Logger.
        /// </summary>
        /// <param name="logBox"></param>
        private Logger(ListBox logBox)
        {
            _logBox = logBox;
            _logger = this;
            AddMessage("Logger Initialized!");
        }

        #endregion

        #region Publics Methods

        /// <summary>
        /// Initializes logger for passed log box.
        /// </summary>
        /// <param name="logBox">
        /// Log box that logger attaches to.
        /// </param>
        /// <returns>
        /// Logger Instance.
        /// </returns>
        public static Logger InitializeLogger(ListBox logBox)
        {
            return _logger ?? (_logger = new Logger(logBox));
        }


        /// <summary>
        /// Add message.
        /// </summary>
        /// <param name="message"></param>
        public static void AddMessage(string message)
        {
            string messageToLog = $@"[{DateTime.Now:HH:mm:ss}] {message}";

            var listBoxItem = new ListBoxItem {Content = messageToLog};
            _logBox.Items.Add(listBoxItem);
        }

        #endregion
    }
}
