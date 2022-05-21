using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string htmlMessage);
    }
}
