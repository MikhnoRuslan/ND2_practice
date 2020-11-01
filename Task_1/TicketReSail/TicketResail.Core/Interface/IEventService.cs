using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IEventService
    {
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Event>> GetEvents();
        string GetEventNameByTicketId(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByIdEvents(int id);
    }
}
