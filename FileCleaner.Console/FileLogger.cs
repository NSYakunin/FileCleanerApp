using FileCleaner.Core;

namespace FileCleaner.Console;

public class FileLogger : ILogger
{
    private readonly string _logFile;

    public FileLogger()
    {
        var logDir = "logs";
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);

        _logFile = Path.Combine(logDir, $"log_{DateTime.Now:yyyyMMddHHmmss}.txt");
    }

    public void Log(string message) =>
        File.AppendAllText(_logFile, $"{DateTime.Now:G}: {message}{Environment.NewLine}");

    public string GetLogFilePath() => _logFile;
}