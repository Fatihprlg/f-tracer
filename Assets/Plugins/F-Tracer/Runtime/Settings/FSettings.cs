using System;
using System.IO;
using UnityEngine;

[Serializable]
public class FSettings
{
    public bool consoleLoggerActive = false;
    public bool fileLoggerActive = false;
    public bool databaseLoggerActive = false;
    public bool firebaseLoggerActive = false;
    public FFileLoggerSettings fileLoggerSettings = new ();
    public FDatabaseLoggerSettings databaseLoggerSettings = new ();
    public FFirebaseLoggerSettings firebaseLoggerSettings = new ();


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