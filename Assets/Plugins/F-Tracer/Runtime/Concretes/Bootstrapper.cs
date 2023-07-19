using UnityEngine;

internal static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        var settings = FSettings.Load();       
        FStackTracer.Load(settings.fTracerSettings);
        FLogger.Load(settings.fLoggerSettings);
        FLogger.Log("Application started.", FLoggerColors.Green);
    }
}