using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
