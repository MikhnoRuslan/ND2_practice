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
    public class CityService : ICityService, IAction<CityDTO>
    {
        private readonly TicketsContext _context;

        public CityService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _context.Cities.ToListAsync();
        }

        public int GetCityIdByName(string name)
        {
            var result = _context.Cities.FirstOrDefault(c => c.Name.Equals(name));
            return result?.Id ?? default;
        }

        public async Task<OperationDetails> Create(CityDTO cityDto)
        {
            var city = new City {Name = cityDto.Name};
            
            var res = _context.Cities.FirstOrDefault(c => c.Name.Equals(cityDto.Name));

            if (res == null)
            {
                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();

                return new OperationDetails(true, "City is successfully created", "");
            }
            else
            {
                return new OperationDetails(false, "The city with this name still exists!", "Name");
                
            }
        }

        public async Task Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if(city != null)
                _context.Cities.Remove(city);

            await _context.SaveChangesAsync();
        }
    }
}
