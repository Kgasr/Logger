namespace Logging
{
    public class FileLogDestination : ILogDestination
    {
        private readonly string _filePath;

        public FileLogDestination(string filePath)
        {
            _filePath = filePath;
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
                catch (Exception ex)                                   // Catch all other exceptions
                {
                    await Utils.LogDataWithExceptionAsync(logRecord, ex, nameof(FileLogDestination));
                    break;
                }
            }
        }
    }
}
