using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading;

/// <summary>
/// Summary description for MailSender
/// </summary>
public class MailSender
{
    public string recipient, subject, messageBody;

    public MailSender(string _recipient, string _subject, string _messageBody)
    {
        recipient = _recipient;
        subject = _subject;
        messageBody = _messageBody;
    }

    public MailSender()
    {

    }

    public void SendMail()
    {
        Thread t = new Thread(new ThreadStart(ThreadedMailSend));
        t.Start();
    }

    public void ThreadedMailSend()
    {
        try
        {
            MailMessage message = new MailMessage("admin@deerpark.org", recipient);
            message.Body = messageBody;
            message.Subject = subject;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            //message.ReplyTo = new MailAddress(email.ReplyToAddress);
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Send(message);
            }
            catch (System.Net.Mail.SmtpException)
            {
                return;
            }
            return;
        }
        catch
        {
            // We cannot allow exceptions to ever happen in a thread or it kills all of ASP.NET.

        }
    }
}
