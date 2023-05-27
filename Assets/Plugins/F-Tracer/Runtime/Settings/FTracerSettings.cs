using System;

[Serializable]
public class FTracerSettings
{
    public bool sendEmailActive = false;
    public bool customSmtp = false;
    public FEmailSettings emailSettings = new ();

}