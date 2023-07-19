using System;
using System.Globalization;
using UnityEngine.Serialization;

[Serializable]
public class FEmailSettings
{
    public string smtpClientHost;
    public string fromEmail;
    public string fromPassword;
    public string[] toEmail;
    public bool IsCredentialsValid => !string.IsNullOrEmpty(fromEmail) && 
                                      !string.IsNullOrEmpty(fromPassword) && 
                                      !string.IsNullOrEmpty(smtpClientHost);
}