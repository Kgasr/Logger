namespace Logging
{
    public static class Constants
    {
        public const int RetryDelay = 1000;      // milliseconds
        public const int MaxRetryAttempts = 5;
        public const string DefaultLogDirectory = "D:\\Logs\\Logger\\";
        public const string DefaultLogFile = "DefaultLogFile.txt";
        public const string DefaultLogLevel = "Information";
        public const string DefaultLogFormat = "{Timestamp} [{Level}] {Message}{NewLine}{Exception}";
    }
}
