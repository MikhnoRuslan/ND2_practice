using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class OrderService : IOrderService
    {
        private readonly TicketsContext _context;

        public OrderService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByIdEvents(int id)
        {
            var tickets = _context.Tickets.Where(t => t.EventId.Id.Equals(id));
            return await tickets.ToListAsync();
        }

        public async Task<Event> GetEventsById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserLogin(string login)
        {
            return await _context.Orders.Where(o => o.BuyerId.Login.Equals(login)).ToListAsync();
        }
    }
}
