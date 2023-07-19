using System;
using Firebase.Analytics;
using Firebase.Crashlytics;
internal static class FirebaseHelper
{
    internal static void Log(string message, LogType logType)
    {
        FirebaseAnalytics.LogEvent(message, "Log Type", logType.ToString());
    }

    internal static void LogEvent(string message, string paramName, string param)
    {
        FirebaseAnalytics.LogEvent(message, paramName, param);
    }
    internal static void LogCrush(string message)
    {
        var ex = new Exception(message);
        Crashlytics.LogException(ex);
    }
}
