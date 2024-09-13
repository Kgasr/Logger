namespace Logging
{
    public static class LoggerProvider
    {     
        public static Logger CreateLogger(string filePath, LogLevel logLevel = Constants.DefaultLogLevel, string logFormat = Constants.DefaultLogFormat)
        {
            ILogDestination logDestination = new FileLogDestination(filePath);
            return new Logger(logDestination, logLevel, logFormat);
        }

        public static Logger CreateLogger(string eventSource, string eventLogName, LogLevel logLevel = Constants.DefaultLogLevel, string logFormat = Constants.DefaultLogFormat)
        {
            ILogDestination logDestination = new EventLogDestination(eventSource, eventLogName);
            return new Logger(logDestination, logLevel, logFormat);
        }
    }
}
