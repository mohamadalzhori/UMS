using System.Net;
using System.Net.Mail;

namespace UMS.Infrastructure.Email;

public static class EmailSender
{
   public static Task Send(string recipientEmail, string subject, string message)
   {
      var client = new SmtpClient("smtp-mail.outlook.com", 587)
      {
         EnableSsl = true,
         UseDefaultCredentials = false,
         Credentials = new NetworkCredential("a318add9b3@emaildbox.pro", "avzjoxmuibfwnvcn")
      }; 
     
      return client.SendMailAsync(
         new MailMessage(from: "a318add9b3@emaildbox.pro",
            to: recipientEmail,
            subject,
            message
         ));
   }
}