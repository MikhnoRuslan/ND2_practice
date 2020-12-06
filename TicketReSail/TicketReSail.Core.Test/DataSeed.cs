using System.Collections.Generic;
using TicketReSail.DAL.Model;
using Bogus;

namespace TicketReSail.Core.Test
{
    public class DataSeed
    {
        private readonly Faker<City> _cities;
        private readonly Faker<Venue> _venues;
        private readonly Faker<Event> _events;
        private readonly List<Category> _categories;
        private readonly Faker<User> _users;
        private readonly Faker<Ticket> _tickets;
        private readonly Faker<Order> _orders;

        public DataSeed()
        {
            _categories = new List<Category>
            {
                new Category {Id = 0, Name = "Film"},
                new Category {Id = 1, Name = "Concert"},
                new Category {Id = 2, Name = "Party"}
            };

            var cityId = 0;
            _cities = new Faker<City>()
                .RuleFor(c => c.Id, f => cityId++)
                .RuleFor(c => c.Name, f => f.Address.City());

            var venueId = 0;
            _venues = new Faker<Venue>()
                .RuleFor(v => v.Id, f => venueId++)
                .RuleFor(v => v.Name, f => f.Name.JobArea())
                .RuleFor(v => v.Address, f => f.Address.StreetAddress())
                .RuleFor(x => x.City, () => _cities)
                .RuleFor(x => x.CityId, (f, v) => v.City.Id);

            var eventId = 0;
            _events = new Faker<Event>()
                .RuleFor(e => e.Id, f => eventId++)
                .RuleFor(e => e.CategoryId, f => f.Random.Number(0, 2))
                .RuleFor(e => e.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Venue, () => _venues)
                .RuleFor(x => x.VenueId, (f, e) => e.Venue.Id);

            var userId = new[] {"123id", "id123", "id123id"};
            _users = new Faker<User>()
                .RuleFor(u => u.Id, f => f.PickRandom(userId))
                .RuleFor(u => u.Login, f => f.Internet.UserName());

            var ticketId = 0;
            _tickets = new Faker<Ticket>()
                .RuleFor(t => t.Id, f => ticketId)
                .RuleFor(t => t.EventId, f => f.Random.Number(0, 2))
                .RuleFor(t => t.Bought, f => false)
                .RuleFor(t => t.UserIdSeller, f => "123id")
                .RuleFor(t => t.User, f => GetUsers()[0]);

            var orderId = 0;
            _orders = new Faker<Order>()
                .RuleFor(o => o.Id, f => orderId++)
                .RuleFor(x => x.Ticket, () => _tickets)
                .RuleFor(x => x.TicketId, (f, o) => o.Ticket.Id)
                .RuleFor(o => o.UserIdBuyer, f => "id123")
                .RuleFor(o => o.User, f => GetUsers()[1])
                .RuleFor(o => o.Status, f => Constants.Selling);

            _orders.Generate();
        }

        public List<City> GetCities()
        {
            return _cities.Generate(3);
        }

        public List<User> GetUsers()
        {
            return _users.Generate(3);
        }

        public List<Ticket> GetTickets()
        {
            return _tickets.Generate(1);
        }

        public List<Order> GetOrders()
        {
            return _orders.Generate(1);
        }

        public int GetTicketId(Ticket ticket)
        {
            return ticket.Id;
        }
    }
}