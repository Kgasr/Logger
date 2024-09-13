namespace Logging
{
    public static class Utils
    {
        // Method to amend the log with exception details and write it to a file
        public static async Task LogDataWithExceptionAsync(string logRecord, Exception ex, string className)
        {
            // Amend logRecord to include exception details and the class name
            logRecord += $"Exception: {ex.Message}" + Environment.NewLine +
                         $"Class: {className}" + Environment.NewLine +
                         $"StackTrace: {ex.StackTrace}";

            // Define a temporary log file path
            string tempLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_"+Constants.DefaultLogFile);
            // Write the amended log to the temporary log file
            using StreamWriter writer = new(tempLogFile, append: true);
            await writer.WriteLineAsync(logRecord);
        }
    }
}