using System.Linq;
using System.Threading.Tasks;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class TicketService : ITickerService, IAction<TickedDTO>
    {
        private readonly TicketsContext _context;

        public TicketService(TicketsContext context)
        {
            _context = context;
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

        public int GetLastTicketIdByUserName( )
        {
            var ticket = _context.Tickets.AsEnumerable().Last();
            if(ticket != null)
                return ticket.Id;

            return default;
        }

        public Ticket GetTicketById(int ticketId)
        {
            return _context.Tickets.Find(ticketId);
        }

        public async Task<OperationDetails> Create(TickedDTO modelDto)
        {
            var ticket = new Ticket
            {
                Price = modelDto.Price,
                EventId = modelDto.EventId,
                UserIdSeller = modelDto.UserId,
                Description = modelDto.Description,
                Bought = false
            };

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return new OperationDetails(true, string.Empty, string.Empty);
        }

        public async Task Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
                _context.Tickets.Remove(ticket);

            await _context.SaveChangesAsync();
        }
    }
}
