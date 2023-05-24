using UnityEngine;

internal class FirebaseLogger : ILogger
{
    private readonly FFirebaseLoggerSettings _settings;

    internal FirebaseLogger(FFirebaseLoggerSettings settings)
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