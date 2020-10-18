using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface ITickerService
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Task<IEnumerable<Ticket>> GetTicketsByUserLogin(string login);
    }
}
