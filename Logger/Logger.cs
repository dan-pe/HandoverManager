using System.Windows.Media;

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
        /// <param name="message">
        /// Message to log.
        /// </param>
        /// <param name="threshold">
        /// Message threshold.
        /// </param>
        public static void AddMessage(string message, MessageThreshold threshold = MessageThreshold.NOERROR)
        {
            string messageToLog = $@"[{DateTime.Now:HH:mm:ss}] {message}";

            var listBoxItem = new ListBoxItem {Content = messageToLog};
            listBoxItem.Background = HandleMessageThreshold(threshold);
            _logBox.Items.Add(listBoxItem);
        }

        /// <summary>
        /// Handles display of threshold levels.
        /// </summary>
        /// <param name="threshold">
        /// Message threshold.
        /// </param>
        /// <returns>
        /// Brush.
        /// </returns>
        private static Brush HandleMessageThreshold(MessageThreshold threshold)
        {
            switch (threshold)
            {
                case MessageThreshold.SUCCESS:
                    return new SolidColorBrush(Color.FromRgb(0, 204, 102));

                case MessageThreshold.FAIL:
                    return new SolidColorBrush(Color.FromRgb(255, 51, 51));

                case MessageThreshold.WARNING:
                    return new SolidColorBrush(Color.FromRgb(255, 255, 102));

                case MessageThreshold.NOERROR:
                    return new SolidColorBrush(Color.FromRgb(255, 255, 255));

                default:
                    throw new ArgumentOutOfRangeException(nameof(threshold), threshold, null);
            }
        }

        #endregion
    }
}
