namespace Logger
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class Logger
    {
        /// <summary>
        /// The log box.
        /// </summary>
        private static ListBox _logBox;

        /// <summary>
        /// The logger instance.
        /// </summary>
        private static Logger _logger;

        //private

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

            var listBoxItem = new ListBoxItem
            {
                Content = messageToLog,
                Background = HandleMessageThreshold(threshold)
            };

            _logBox.Items.Add(listBoxItem);
        }

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
    }
}