using System.Collections.Generic;
using System.Linq;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class CityService : ICityService
    {
        private readonly TicketsContext _context;

        public CityService(TicketsContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }
    }
}
