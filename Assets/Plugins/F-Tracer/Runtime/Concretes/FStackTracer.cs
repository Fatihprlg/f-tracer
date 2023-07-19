using System;
using UnityEngine;


public delegate void HandleException(string logString, string stackTrace); 

public static class FStackTracer
{
    public static event HandleException OnExceptionRaised;
    private static FTracerSettings _settings;

    internal static void Load(FTracerSettings settings)
    {
        Application.logMessageReceived += HandleLog;
        _settings = settings;
    }

    private static void HandleLog(string condition, string stacktrace, UnityEngine.LogType type)
    {
        if (type == UnityEngine.LogType.Exception)
        {
            FLogger.Error(stacktrace);
            HandleCrush(condition, stacktrace);
        }
    }

    private static void HandleCrush(string logString, string stackTrace)
    {
        OnExceptionRaised?.Invoke(logString, stackTrace);
        if (_settings.sendEmailActive)
        {
            EmailHelper.SendCrushReport(_settings.emailSettings, stackTrace);
        }
        
    }
}