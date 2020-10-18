using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class DataSeeder
    {
        private readonly TicketsContext _context;
        private static readonly List<User> _user = new List<User>
        {
            new User{Id = 0, Login = "Admin", Password = "admin", FirstName = "Admin", LastName = "Admin", Role = Constants.Administrator}
        };

        //private static readonly List<Category> _categories = new List<Category>
        //{
        //    new Category{Id = 0, Name = "film"}
        //};

        //private static readonly List<City> _cities = new List<City>
        //{
        //    new City{Id = 0, Name = "Brest"}
        //};

        //private static readonly List<Venue> _venues = new List<Venue>
        //{
        //    new Venue{Id = 0, Name = "lala", Address = "address", City = _cities[0]}
        //};

        //private static readonly List<Event> _events =new List<Event>
        //{
        //    new Event{Id = 0, Category = _categories[0], Name = "Home Alone",
        //        Date = new DateTime(2020,6,6), Venue = _venues[0], Banner = "Home Alone.jpg", Description = "lala"}
        //};

        public DataSeeder(TicketsContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Users.Any())
                await _context.Users.AddRangeAsync(_user);

            //if (!_context.Categories.Any())
            //    await _context.Categories.AddRangeAsync(_categories);

            //if (!_context.Cities.Any())
            //    await _context.Cities.AddRangeAsync(_cities);

            //if (!_context.Venues.Any())
            //    await _context.Venues.AddRangeAsync(_venues);

            //if (!_context.Events.Any())
            //    await _context.Events.AddRangeAsync(_events);

            await _context.SaveChangesAsync();
        }
    }
}
