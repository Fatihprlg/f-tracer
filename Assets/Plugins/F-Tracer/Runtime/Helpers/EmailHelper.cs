using System;
using System.Net.Mail;
using UnityEngine;
internal static class EmailHelper
{
    internal static void SendCrushReport(FEmailSettings emailSettings, string trace)
    {
        if (!emailSettings.IsCredentialsValid)
        {
            FLogger.Warning("Mail credentials is not valid!");
            return;
        }
        var mail = new MailMessage();
        foreach (var to in emailSettings.toEmail)
        {
            mail.To.Add(to);
        }
        mail.From = new MailAddress(emailSettings.fromEmail, "F-Tracer", System.Text.Encoding.UTF8);
        mail.Subject = $"F-Tracer caught an error on application {Application.productName}";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        var lastLogFile = FLogger.GetLastLogFileName();
        mail.Body = "<html>Hi, <br><br>" +
                    $"F-Tracer caught an error on your application named <bold>{Application.productName}</bold> . " +
                    $"Here is the last error message: <br><bold>{trace}</bold><br>" +
                    (string.IsNullOrEmpty(lastLogFile) ? "<br>" : "You can find the last log file as an attachment to this mail.<br><br>") +
                    "Sincerely,<br>" +
                    "<bold>F-Tracer</bold></html>";
        if(!string.IsNullOrEmpty(lastLogFile))
            mail.Attachments.Add(new Attachment($"{Application.persistentDataPath}/Logs/{lastLogFile}"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        var client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential(emailSettings.fromEmail, emailSettings.fromPassword);
        client.Port = 587;
        client.Host = emailSettings.smtpClientHost;
        client.EnableSsl = true;
        try
        {
            client.Send(mail);
            client.Dispose();
            mail.Dispose();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    
}