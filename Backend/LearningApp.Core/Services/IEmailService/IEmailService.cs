
using System.Threading.Tasks;

namespace LearningApp.Core.Services.EmailService
{

    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
