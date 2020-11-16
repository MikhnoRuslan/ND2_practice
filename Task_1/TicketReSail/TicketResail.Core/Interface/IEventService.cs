using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.Core.Queries;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IEventService
    {
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Event>> GetEvents(/*EventQuery eventQuery*/);
        string GetEventNameByTicketId(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByIdEvents(int id);
    }
}
