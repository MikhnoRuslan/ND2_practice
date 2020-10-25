using System.Threading.Tasks;

namespace TicketReSail.Core.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
