using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class TicketService : ITickerService
    {
        private readonly TicketsContext _context;

        public TicketService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserLogin(string login)
        {
            return await _context.Tickets.Where(u => u.User.Login.Equals(login)).ToListAsync();
        }
    }
}
