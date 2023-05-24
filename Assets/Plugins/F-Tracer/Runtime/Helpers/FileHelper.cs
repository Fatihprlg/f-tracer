using System;
using System.IO;
using UnityEngine;

internal static class FileHelper
{
    internal static void SaveBinary(string fileName, LogData logData)
    {
        var saveFolderPath = $"{Application.persistentDataPath}/Logs";
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);
        var fileFullPath = $"{saveFolderPath}/{fileName}";
        TryLoadBinary(fileName, out var oldLogData);
        try
        {
            using (var writer = new BinaryWriter(File.Open(fileFullPath, FileMode.OpenOrCreate)))
            {
                if (oldLogData != null)
                {
                    writer.Write(oldLogData.Length + 1);
                    for (int i = 0; i < oldLogData.Length; i++)
                    {
                        writer.Write((byte)oldLogData[i].logType);
                        writer.Write(oldLogData[i].logMessage);
                    }
                }
                else writer.Write((byte)1);
                writer.Write((byte)logData.logType);
                writer.Write(logData.logMessage);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing file {fileName}: {e.Message}");
        }
        
    }
    
    internal static void TryLoadBinary(string fileName, out LogData[] logData)
    {
        var fileFullPath = $"{Application.persistentDataPath}/Logs/{fileName}";
        if (File.Exists(fileFullPath))
        {
            try
            {
                using (var reader = new BinaryReader(File.Open(fileFullPath, FileMode.Open)))
                {
                    var logCount = reader.ReadInt32();
                    logData = new LogData[logCount];
                    for (int i = 0; i < logCount; ++i)
                    {
                        logData[i].logType = (LogType)reader.ReadByte();
                        logData[i].logMessage = reader.ReadString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error reading file: {ex.Message}");
                logData = null;
            }
        }
        else
        {
            Debug.LogError($"File not found at path: {fileFullPath}");
            logData = null;
        }
    }
    
    internal static void SaveJson(string fileName, LogData saveData)
    {
        var saveFolderPath = $"{Application.persistentDataPath}/Logs";
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);
        var fileFullPath = $"{saveFolderPath}/{fileName}";
        TryLoadJson(fileName, out var oldLogData);
        try
        {
            var wrapper = new LogDataWrapper();
            if (oldLogData != null)
            {
                wrapper.logDatas = new LogData[oldLogData.Length + 1];
                for (int i = 0; i < oldLogData.Length; i++)
                {
                    wrapper.logDatas[i] = oldLogData[i];
                }

                wrapper.logDatas[^1] = saveData;
            }
            else wrapper.logDatas = new[] { saveData };
        
            var jsonString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(fileFullPath, jsonString);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing file {fileName}: {e.Message}");
        }
        
    }

    internal static void TryLoadJson(string fileName, out LogData[] saveData)
    {
        var fileFullPath = $"{Application.persistentDataPath}/Logs/{fileName}";
        if (File.Exists(fileFullPath))
        {
            try
            {
            
                var jsonString = File.ReadAllText(fileFullPath);
                var wrapper = JsonUtility.FromJson<LogDataWrapper>(jsonString);
                saveData = wrapper.logDatas;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading file {fileName}: {e.Message}");
                saveData = null;
            }
        }
        else
        {
            Debug.LogError($"Error reading file {fileName}: file not exists");
            saveData = null;
        }
    }
    
}