using System;

[Serializable]
public class FLoggerSettings
{
    public bool consoleLoggerActive = false;
    public bool fileLoggerActive = false;
    public bool firebaseLoggerActive = false;
    public FFileLoggerSettings fileLoggerSettings = new ();
    public FFirebaseLoggerSettings firebaseLoggerSettings = new ();
}