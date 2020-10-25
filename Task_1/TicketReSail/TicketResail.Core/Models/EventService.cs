using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class EventService : IEventService
    {
        private readonly TicketsContext _context;

        public EventService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public IEnumerable<Event> GetEvents()
        {
            return  _context.Events.ToList();
        }
    }
}
