using UnityEngine;

internal class Bootsrapper
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