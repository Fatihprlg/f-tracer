using System;

[Serializable]
public class FTracerSettings
{
    public bool sendEmailActive;
    public FEmailSettings emailSettings = new ();

}