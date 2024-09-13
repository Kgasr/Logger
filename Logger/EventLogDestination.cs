using System.Diagnostics;

namespace Logging
{
    public class EventLogDestination : ILogDestination
    {
        private readonly string _logSource;
        private readonly string _logName;

        public EventLogDestination(string logSource, string logName)
        {
            _logSource = logSource;
            _logName = logName;

            if (OperatingSystem.IsWindows() && !EventLog.SourceExists(_logSource))
            {
                EventLog.CreateEventSource(_logSource, _logName);
            }
        }

        public async Task WriteLogAsync(string logRecord)
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    EventLogEntryType logType = EventLogEntryType.Information;

                    if (logRecord.IndexOf("[error]", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        logType = EventLogEntryType.Error;
                    }
                    else if (logRecord.IndexOf("[warning]", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        logType = EventLogEntryType.Warning;
                    }
                    await Task.Run(() =>
                    {
                        using EventLog eventLog = new(_logName)
                        {
                            Source = _logSource
                        };
                        eventLog.WriteEntry(logRecord, logType);
                    });
                }
                else
                {
                    // Handle non-Windows logging and simply do nothing
                    return;
                }
            }
            catch (Exception ex)
            {
                await Utils.LogDataWithExceptionAsync(logRecord, ex, nameof(EventLogDestination));
            }
        }
    }
}