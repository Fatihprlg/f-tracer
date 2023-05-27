using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        FLogger.Error("This is an error log");
        FLogger.Warning("This is a warning log");
        FLogger.Log("This is a default log");
        FLogger.Log("This is a default log with obj", this);
        FLogger.Log("This is a custom log", FLoggerColors.Blue);
        FStackTracer.OnExceptionRaised += (logString, trace) => Debug.Log("ex," + trace + "   " + logString);
        Debug.LogException(new ArgumentException("Argument null olabilur", "param param"));
    }
}
