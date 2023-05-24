using UnityEngine;

public static class FLogger
{
    private static readonly ConsoleLogger ConsoleLogger;
    private static readonly DatabaseLogger DatabaseLogger;
    private static readonly FileLogger FileLogger;
    private static readonly FirebaseLogger FirebaseLogger;

    static FLogger()
    {
        var fSettings = FSettings.Load();
        if (fSettings.consoleLoggerActive)
            ConsoleLogger = new ConsoleLogger();
        if (fSettings.fileLoggerActive)
            FileLogger = new FileLogger(fSettings.fileLoggerSettings);
        if (fSettings.databaseLoggerActive)
            DatabaseLogger = new DatabaseLogger(fSettings.databaseLoggerSettings);
        if (fSettings.firebaseLoggerActive)
            FirebaseLogger = new FirebaseLogger(fSettings.firebaseLoggerSettings);
    }
    
    public static void Error(string message, Object @object = null)
    {
        ConsoleLogger?.Error(message, @object);
        FileLogger?.Error(message, @object);
        DatabaseLogger?.Error(message, @object);
        FirebaseLogger?.Error(message, @object);
    }

    public static void Warning(string message, Object @object = null)
    {
        ConsoleLogger?.Warning(message, @object);
        FileLogger?.Warning(message, @object);
        DatabaseLogger?.Warning(message, @object);
        FirebaseLogger?.Warning(message, @object);
    }

    public static void Log(string message, Object @object = null)
    {
        ConsoleLogger?.Log(message, @object);
        FileLogger?.Log(message, @object);
        DatabaseLogger?.Log(message, @object);
        FirebaseLogger?.Log(message, @object);
    }

    public static void Log(string message, FLoggerColors color)
    {
        ConsoleLogger?.Log(message, color);
        FileLogger?.Log(message, color);
        DatabaseLogger?.Log(message, color);
        FirebaseLogger?.Log(message, color);
    }
}