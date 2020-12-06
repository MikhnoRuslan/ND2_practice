using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface ITickerService
    {
        int GetEventIdByTicketId(int ticketId);
        decimal GetPriceByTicketId(int ticketId);
        bool GetStatusBoughtByTicketId(int id);
        Task<IEnumerable<Ticket>> GetTicketsForSell(string status, string userName);
        Task<IEnumerable<Ticket>> GetTickets();
    }
}
