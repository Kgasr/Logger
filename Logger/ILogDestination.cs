namespace Logging
{
    public interface ILogDestination
    {
        Task WriteLogAsync(string logRecord);
    }
}
