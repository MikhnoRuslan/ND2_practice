using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class TicketService : ITickerService, IAction<TickedDTO, Ticket>
    {
        private readonly TicketsContext _context;
        private readonly IUserService _userService;

        public TicketService(TicketsContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        public bool GetStatusBoughtByTicketId(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                if (ticket.Bought)
                    return true;
            }

            return default;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsForSell(string status, string userName)
        {
            return await _context.Tickets
                .Where(u => u.UserIdSeller.Equals(_userService.GetUserIdByName(userName)))
                .Where(o => o.Status.Equals(status)).ToListAsync();
        }

        public int GetEventIdByTicketId(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            return ticket.EventId;
        }

        public decimal GetPriceByTicketId(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            return ticket.Price;
        }

        public async Task<OperationDetails> Create(TickedDTO modelDto)
        {
            var ticket = new Ticket
            {
                Price = modelDto.Price,
                EventId = modelDto.EventId,
                UserIdSeller = modelDto.UserId,
                Description = modelDto.Description,
                Bought = false,
                Status = modelDto.TicketStatus
            };

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return new OperationDetails(true, string.Empty, string.Empty);
        }

        public async Task<Ticket> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
                _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();

            return ticket;
        }
    }
}
