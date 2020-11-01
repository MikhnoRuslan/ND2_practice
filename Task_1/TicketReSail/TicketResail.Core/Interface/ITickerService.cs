using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface ITickerService
    {
        int GetEventIdByTicketId(int ticketId);
        decimal GetPriceByTicketId(int ticketId);
        int GetLastTicketIdByUserName();
        Ticket GetTicketById(int ticketId);
    }
}
