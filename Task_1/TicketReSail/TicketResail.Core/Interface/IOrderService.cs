using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Ticket>> GetTicketsByIdEvents(int id);
        Task<Event> GetEventsById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserLogin(string login);
    }
}
