using System;
using System.Text;
using Object = UnityEngine.Object;

public static class FLogger
{
    private static ConsoleLogger _consoleLogger;
    private static FileLogger _fileLogger;
    private static FirebaseLogger _firebaseLogger;
    internal static string GetLastLogFileName() => _fileLogger != null ? _fileLogger.GetLastLogFileName() : "";

    internal static void Load(FLoggerSettings settings)
    {
        if (settings.consoleLoggerActive)
            _consoleLogger = new ConsoleLogger();
        if (settings.fileLoggerActive)
            _fileLogger = new FileLogger(settings.fileLoggerSettings);
        if (settings.firebaseLoggerActive)
            _firebaseLogger = new FirebaseLogger(settings.firebaseLoggerSettings);
    }

    
    public static void Error(string message, Object @object = null)
    {
        message = FormatLog(message, LogType.Error);
        _consoleLogger?.Error(message, @object);
        _fileLogger?.Error(message, @object);
        _firebaseLogger?.Error(message, @object);
    }

    public static void Warning(string message, Object @object = null)
    {
        message = FormatLog(message, LogType.Warning);
        _consoleLogger?.Warning(message, @object);
        _fileLogger?.Warning(message, @object);
        _firebaseLogger?.Warning(message, @object);
    }

    public static void Log(string message, Object @object = null)
    {
        message = FormatLog(message, LogType.Default);
        _consoleLogger?.Log(message, @object);
        _fileLogger?.Log(message, @object);
        _firebaseLogger?.Log(message, @object);
    }

    public static void Log(string message, FLoggerColors color)
    {
        message = FormatLog(message, LogType.Custom);
        _consoleLogger?.Log(message, color);
        _fileLogger?.Log(message, color);
        _firebaseLogger?.Log(message, color);
    }

    public static void LogEvent(string message, string paramName, string paramValue)
    {
        message = FormatLog(message, LogType.Event);
        _consoleLogger?.Event(message, paramName, paramValue);
        _fileLogger?.Event(message, paramName, paramValue);
        _firebaseLogger?.Event(message, paramName, paramValue);
    }
    public static void LogEvent(string message)
    {
        message = FormatLog(message, LogType.Event);
        _consoleLogger?.Event(message);
        _fileLogger?.Event(message);
        _firebaseLogger?.Event(message);
    }

    private static string FormatLog(string message, LogType type)
    {
        var sb = new StringBuilder();
        var color = type switch
        {
            LogType.Default => FLoggerColors.White.ToString().ToLower(),
            LogType.Error => FLoggerColors.Red.ToString().ToLower(),
            LogType.Warning => FLoggerColors.Yellow.ToString().ToLower(),
            LogType.Custom => "",
            LogType.Exception => FLoggerColors.Red.ToString().ToLower(),
            LogType.Event => FLoggerColors.Blue.ToString().ToLower(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        sb.Append(type == LogType.Custom ? "" : $"<color= {color}>");
        sb.Append($"[F-Logger]:[{type.ToString()}]: ");
        sb.Append(type == LogType.Custom ? "" : "</color> ");
        sb.Append(message);
        return sb.ToString();
    }
}