using UnityEngine;

internal class DatabaseLogger : ILogger
{
    private readonly FDatabaseLoggerSettings _settings;

    internal DatabaseLogger(FDatabaseLoggerSettings settings)
    {
        _settings = settings;
    }
    
    public void Error(string message, Object @object = null)
    {
        throw new System.NotImplementedException();
    }

    public void Warning(string message, Object @object = null)
    {
        throw new System.NotImplementedException();
    }

    public void Log(string message, Object @object = null)
    {
        throw new System.NotImplementedException();
    }

    public void Log(string message, FLoggerColors color)
    {
        throw new System.NotImplementedException();
    }
}