using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class VenueService : IVenueService, IAction<VenueDTO, Venue>
    {
        private readonly TicketsContext _context;

        public VenueService(TicketsContext context)
        {
            _context = context;
        }

        public async Task EditVenue(int venueId, VenueDTO venueDto)
        {
            var venue = await _context.Venues.FindAsync(venueId);
            if (venue != null)
            {
                venue.Name = venueDto.Name;
                venue.Address = venueDto.Address;
                venue.CityId = venueDto.CityId;

                _context.Update(venue);
            }

            await _context.SaveChangesAsync();
        }

        public int GetVenueIdByName(string name)
        {
            var venue = _context.Venues.FirstOrDefault(v => v.Name.Equals(name));
            return venue?.Id ?? default;
        }

        public async Task<Venue> GetVenueById(int id)
        {
            var venue = await _context.Venues
                .Include(c => c.City)
                .SingleOrDefaultAsync(c => c.Id == id);

            return venue;
        }

        public async Task<IEnumerable<Venue>> GetVenues()
        {
            var venue = _context.Venues
                .Include(c => c.City);

            return await venue.ToListAsync();
        }

        public async Task<IEnumerable<Venue>> GetVenuesByQuery(VenueQuery venueQuery)
        {
            var queryable = _context.Venues.AsQueryable();

            if (venueQuery.Cities != null)
                queryable = queryable.Where(v => venueQuery.Cities.Contains(v.CityId));

            return await queryable.ToListAsync();
        }

        public string GetNameVenueById(int id)
        {
            var venue = _context.Venues.Find(id);

            return venue.Name;
        }

        public string GetCityNameByVenueId(int id)
        {
            var cityId = GetCityIdByVenueId(id);
            var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);

            return city?.Name;
        }

        public string GetVenueAddressByVenueId(int id)
        {
            var address = _context.Venues.FirstOrDefault(v => v.Id == id);

            return address?.Address;
        }

        public async Task<OperationDetails> Create(VenueDTO modelDto)
        {
            var venue = new Venue
            {
                Name = modelDto.Name,
                Address = modelDto.Address,
                CityId = modelDto.CityId
            };

            var resultName = _context.Venues.FirstOrDefault(v => v.CityId.Equals(modelDto.CityId));
            var resultAddress = _context.Venues.FirstOrDefault(v => v.Address.Equals(modelDto.Address));

            if (resultName == null || resultAddress == null)
            {
                await _context.Venues.AddAsync(venue);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Venue is successfully created", string.Empty);
            }
            else
            {
                return new OperationDetails(false, "The venue with the name and address still exists!", "Name");
            }
        }

        public async Task<Venue> Delete(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
                _context.Venues.Remove(venue);

            await _context.SaveChangesAsync();

            return venue;
        }

        private int GetCityIdByVenueId(int id)
        {
            var venue = _context.Venues.Find(id);

            if (venue != null)
                return venue.CityId;

            return default;
        }
    }
}
