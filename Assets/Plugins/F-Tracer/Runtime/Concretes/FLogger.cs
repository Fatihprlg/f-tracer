using UnityEngine;

public static class FLogger
{
    private static ConsoleLogger _consoleLogger;
    private static DatabaseLogger _databaseLogger;
    private static FileLogger _fileLogger;
    private static FirebaseLogger _firebaseLogger;
    internal static string GetLastLogFileName() => _fileLogger != null ? _fileLogger.GetLastLogFileName() : "";

    internal static void Load(FLoggerSettings settings)
    {
        if (settings.consoleLoggerActive)
            _consoleLogger = new ConsoleLogger();
        if (settings.fileLoggerActive)
            _fileLogger = new FileLogger(settings.fileLoggerSettings);
        if (settings.databaseLoggerActive)
            _databaseLogger = new DatabaseLogger(settings.databaseLoggerSettings);
        if (settings.firebaseLoggerActive)
            _firebaseLogger = new FirebaseLogger(settings.firebaseLoggerSettings);
    }

    
    public static void Error(string message, Object @object = null)
    {
        _consoleLogger?.Error(message, @object);
        _fileLogger?.Error(message, @object);
        _databaseLogger?.Error(message, @object);
        _firebaseLogger?.Error(message, @object);
    }

    public static void Warning(string message, Object @object = null)
    {
        _consoleLogger?.Warning(message, @object);
        _fileLogger?.Warning(message, @object);
        _databaseLogger?.Warning(message, @object);
        _firebaseLogger?.Warning(message, @object);
    }

    public static void Log(string message, Object @object = null)
    {
        _consoleLogger?.Log(message, @object);
        _fileLogger?.Log(message, @object);
        _databaseLogger?.Log(message, @object);
        _firebaseLogger?.Log(message, @object);
    }

    public static void Log(string message, FLoggerColors color)
    {
        _consoleLogger?.Log(message, color);
        _fileLogger?.Log(message, color);
        _databaseLogger?.Log(message, color);
        _firebaseLogger?.Log(message, color);
    }
}