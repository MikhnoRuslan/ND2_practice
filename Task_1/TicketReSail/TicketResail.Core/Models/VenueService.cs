using System.Collections.Generic;
using System.Linq;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class VenueService : IVenueService
    {
        private readonly TicketsContext _context;

        public VenueService(TicketsContext context)
        {
            _context = context;
        }

        public IEnumerable<Venue> GetVenues()
        {
            return _context.Venues.ToList();
        }
    }
}
