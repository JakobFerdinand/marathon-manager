namespace Logging.Interfaces
{
    public interface ILogger
    {
        void LogMessage(string message);
        void LogSuccess(string message);
        void LogError(string message);
    }
}
