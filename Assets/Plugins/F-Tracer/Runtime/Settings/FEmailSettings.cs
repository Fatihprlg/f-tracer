using System;
using UnityEngine.Serialization;

[Serializable]
public class FEmailSettings
{
    public string smtpClientHost = Constants.DefaultSmtp;
    public string fromEmail = Constants.DefaultEmail;
    public string fromPassword = Constants.DefaultMailPwd;
    public string[] toEmail;
}