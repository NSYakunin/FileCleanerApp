namespace FileCleaner.Core;

public interface ILogger
{
    void Log(string message);
    string GetLogFilePath();
}