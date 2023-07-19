using System;
using UnityEngine;
using Object = UnityEngine.Object;

internal class ConsoleLogger : ILogger
{
    public void Error(string message, Object @object = null)
    {
        Debug.LogError(message, @object);
    }

    public void Exception(string message, Object @object = null)
    {
        Debug.LogException(new Exception(message), @object);
    }

    public void Warning(string message, Object @object = null)
    {
        Debug.LogWarning(message, @object);
    }

    public void Event(string message, string paramName, string param)
    {
        Debug.Log($"{message}:[ParamName]{paramName}:[Param]{param}");
    }

    public void Event(string message)
    {
        Debug.Log(message);
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