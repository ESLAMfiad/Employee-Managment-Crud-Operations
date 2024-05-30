using demo.DAL.Models;
using System.Net.Mail;
using System.Net;
namespace Demo.PL.Helpers
{
    public static class EmailSettings 
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587); //simplemailtransfer protocol
            Client.EnableSsl = true; //makes email encrypted
            Client.UseDefaultCredentials = false;
            Client.Credentials = new NetworkCredential("eslam.hossny2001@gmail.com", "uhwrnbfjoxuwgcjx");
            Client.Send("eslam.hossny2001@gmail.com", email.To, email.Subject, email.Body);

        }

        
    }
}
