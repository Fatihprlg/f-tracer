using UnityEngine;

internal class ConsoleLogger : ILogger
{
    public void Error(string message, Object @object = null)
    {
        Debug.LogError(message, @object);
    }

    public void Warning(string message, Object @object = null)
    {
        Debug.LogWarning(message, @object);
    }

    public void Log(string message, Object @object = null)
    {
        Debug.Log(message, @object);
    }

    public void Log(string message, FLoggerColors color)
    {
        Debug.Log($"<color={color.ToString().ToLower()}>{message}</color>");
    }
}