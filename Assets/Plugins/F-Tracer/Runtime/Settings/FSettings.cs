using System;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class FSettings
{
    public FLoggerSettings fLoggerSettings = new();
    public FTracerSettings fTracerSettings = new();

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        var fullPath = $"{Application.dataPath}/{Constants.ConfigPath}";
        File.WriteAllText(fullPath, jsonString);
        Debug.Log("Settings saved successfully");
    }

    public static FSettings Load()
    {
        var fullPath = $"{Application.dataPath}/{Constants.ConfigPath}";
        var jsonString = File.ReadAllText(fullPath);
        return string.IsNullOrEmpty(jsonString) ? new FSettings() : JsonUtility.FromJson<FSettings>(jsonString);
    }
}