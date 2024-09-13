namespace Logging
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }
    public class Logger
    {
        private readonly ILogDestination _logDestination;
        private readonly LogLevel _logLevel;
        private readonly string _logFormat;

        public Logger(ILogDestination logDestination, LogLevel logLevel, string logFormat)
        {
            _logDestination = logDestination;
            _logLevel = logLevel;
            _logFormat = logFormat;
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;
        public async Task Log(LogLevel logLevel, string message, Exception? exception = null)
        {
            if (!IsEnabled(logLevel)) return;
            string logRecord = FormatLogRecord(logLevel, message, exception);
            await _logDestination.WriteLogAsync(logRecord);
        }

        private string FormatLogRecord(LogLevel logLevel, string message, Exception? exception = null)
        {
            return _logFormat
                .Replace("{Timestamp}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{Level}", logLevel.ToString())
                .Replace("{Message}", message)
                .Replace("{Exception}", exception?.ToString() ?? string.Empty)
                .Replace("{NewLine}", Environment.NewLine);
        }
    }
}