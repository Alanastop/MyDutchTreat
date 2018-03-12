namespace DutchTreat.MVC.Services.Interfaces
{
    public interface IMailService
    {
        void SendMail(string from, string to, string subject, string body);
        void SendMessage(string to, string subject, string body);
    }
}