namespace Logging
{
    public static class Constants
    {
        public const int RetryDelay = 1000;      // milliseconds
        public const int MaxRetryAttempts = 5;       
        public const string DefaultLogFile = "DefaultLog.txt";
        public const LogLevel DefaultLogLevel = LogLevel.Information;
        public const string DefaultLogFormat = "{Timestamp} [{Level}] {Message}{NewLine}{Exception}";
    }
}
