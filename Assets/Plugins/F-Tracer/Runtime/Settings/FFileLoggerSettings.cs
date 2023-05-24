using System;

[Serializable]
public class FFileLoggerSettings
{
    public FileLoggerType loggerType = FileLoggerType.Binary;
    public FileLoggerPeriod loggerPeriod = FileLoggerPeriod.Daily;
}