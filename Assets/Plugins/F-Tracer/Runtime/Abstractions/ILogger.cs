using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface ILogger
{
    public void Error(string message, Object @object = null);

    public void Exception(string message, Object @object = null);

    public void Warning(string message, Object @object = null);

    public void Event(string message, string paramName, string param);
    public void Event(string message);

    public void Log(string message, Object @object = null);

    public void Log(string message, FLoggerColors color);
}
