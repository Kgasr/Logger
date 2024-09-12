namespace Logging
{
    public class FileLogDestination : ILogDestination
    {
        private readonly string _filePath;

        public FileLogDestination(string filePath)
        {
            _filePath = filePath ?? Constants.DefaultLogDirectory + Constants.DefaultLogFile;
        }

        public async Task WriteLogAsync(string logRecord)
        {
            int maxRetries = Constants.MaxRetryAttempts;
            int retryDelay = Constants.RetryDelay;

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    using FileStream fs = new(_filePath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true);
                    using StreamWriter writer = new(fs);
                    await writer.WriteLineAsync(logRecord);
                    return;
                }
                catch (IOException) when (attempt < maxRetries - 1)     
                {
                    await Task.Delay(retryDelay);                   // Wait before retrying
                }
                catch (Exception)                                   // Catch all other exceptions
                {
                    string failedLogRecordPath = Path.Combine(
                        Path.GetDirectoryName(_filePath) ?? Constants.DefaultLogDirectory ,
                        Path.GetFileName(_filePath) == "" ? "Failed_log_record_" + Path.GetFileName(_filePath) : Constants.DefaultLogFile);

                    using var streamWriter = new StreamWriter(failedLogRecordPath);
                    await streamWriter.WriteLineAsync(logRecord);
                    break;                                          // Exit loop if a non-recoverable exception occurs
                }
            }
        }
    }
}
