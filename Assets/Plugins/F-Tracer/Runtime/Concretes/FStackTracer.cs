using System;
using UnityEngine;


public delegate void HandleException(string logString, string stackTrace); 

public class FStackTracer
{
    public static event HandleException OnExceptionRaised;

    internal FStackTracer()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void HandleLog(string condition, string stacktrace, UnityEngine.LogType type)
    {
        if (type == UnityEngine.LogType.Exception)
        {
            HandleCrush(condition, stacktrace);
        }
    }

    private void HandleCrush(string logString, string stackTrace)
    {
        OnExceptionRaised?.Invoke(logString, stackTrace);
    }
}