using demo.DAL.Models;

namespace Demo.PL.Helpers
{
    public interface IEmailSettings
    {
        public void SendMail(Email email);
    }
}
