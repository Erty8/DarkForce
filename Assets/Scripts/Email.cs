using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Email : MonoBehaviour
{
    public InputField message;
    public InputField senderEmail;
    // Start is called before the first frame update
    
    public void sendmail()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;
        SmtpServer.EnableSsl = true;
        mail.From = new MailAddress("darkforcethegame@gmail.com");
        mail.To.Add (new MailAddress("darkforcethegame@gmail.com"));
        mail.Subject = (senderEmail.text + "'s feedback");
        mail.Body = message.text;
        SmtpServer.Credentials = new System.Net.NetworkCredential("darkforcethegame@gmail.com", "Darkforce123");
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
