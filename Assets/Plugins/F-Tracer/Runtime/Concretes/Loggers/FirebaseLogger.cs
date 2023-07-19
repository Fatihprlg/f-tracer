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
        FirebaseHelper.Log(message, LogType.Error);
    }

    public void Exception(string message, Object @object = null)
    {
        FirebaseHelper.LogCrush(message);
    }

    public void Warning(string message, Object @object = null)
    {
        FirebaseHelper.Log(message, LogType.Warning);
    }

    public void Event(string message, string paramName, string param)
    {
        FirebaseHelper.LogEvent(message, paramName, param);
    }

    public void Event(string message)
    {
        FirebaseHelper.Log(message, LogType.Event);
    }

    public void Log(string message, Object @object = null)
    {
        FirebaseHelper.Log(message, LogType.Default);
    }

    public void Log(string message, FLoggerColors color)
    {
        FirebaseHelper.Log(message, LogType.Custom);
    }
}
