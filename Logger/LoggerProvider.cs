using Microsoft.Extensions.Configuration;

namespace Logging
{
    public static class LoggerProvider
    {
        public static Logger CreateLogger(IConfiguration configuration)
        {
            var logLevel = Enum.Parse<LogLevel>(configuration["LogLevel"]?? Constants.DefaultLogLevel, true);
            var logFormat = configuration["LogFormat"] ?? Constants.DefaultLogFormat;
            var destinationType = configuration["Destination"];

            ILogDestination logDestination = destinationType switch
            {
                "File" => new FileLogDestination(configuration["FilePath"] ?? Constants.DefaultLogDirectory + Constants.DefaultLogFile),
                _ => throw new NotSupportedException($"Logging destination '{destinationType}' is not supported.")
            };

            return new Logger(logDestination, logLevel, logFormat);
        }
    }
}
