using System;
using Object = UnityEngine.Object;

internal class FileLogger : ILogger
{
    private readonly FFileLoggerSettings _settings;
    private readonly string _saveFileName;

    internal FileLogger(FFileLoggerSettings settings)
    {
        _settings = settings;
        _saveFileName = DateTime.Today.ToString(_settings.loggerPeriod == FileLoggerPeriod.Daily ? "dd-MM-yyyy" : "MM-yyyy");
    }

    public string GetLastLogFileName()
    {
        return _settings.loggerType == FileLoggerType.Binary ? $"{_saveFileName}.log" : $"{_saveFileName}.json";
    }
    
    public void Error(string message, Object @object = null)
    {
        var contextName = @object ? @object.ToString() : "LogError";
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> {contextName} : {message}",
            logType = LogType.Error
        };
        SaveLogData(logData);
    }

    public void Exception(string message, Object @object = null)
    {
        var contextName = @object ? @object.ToString() : "LogException";
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> {contextName} : {message}",
            logType = LogType.Exception
        };
        SaveLogData(logData);    }

    public void Warning(string message, Object @object = null)
    {
        var contextName = @object ? @object.ToString() : "LogWarning";
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> {contextName} : {message}",
            logType = LogType.Warning
        };
        SaveLogData(logData);  
    }

    public void Event(string message, string paramName, string param)
    {
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> LogEvent : [{paramName}] : {param} : {message}",
            logType = LogType.Event
        };
        SaveLogData(logData);  
    }

    public void Event(string message)
    {
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> LogEvent : {message}",
            logType = LogType.Event
        };
        SaveLogData(logData);    
    }

    public void Log(string message, Object @object = null)
    {
        var contextName = @object ? @object.ToString() : "Log";
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> {contextName} : {message}",
            logType = LogType.Default
        };
        SaveLogData(logData);
    }

    public void Log(string message, FLoggerColors color)
    {
        var logData = new LogData
        {
            logMessage = $"<{DateTime.Now:hh:mm:ss t z}> CustomLog : {message}",
            logType = LogType.Custom
        };
        SaveLogData(logData);
        
    }

    private void SaveLogData(LogData logData)
    {
        switch (_settings.loggerType)
        {
            case FileLoggerType.Binary:
                FileHelper.SaveBinary($"{_saveFileName}.log", logData);
                break;
            case FileLoggerType.Json:
                FileHelper.SaveJson($"{_saveFileName}.json", logData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}