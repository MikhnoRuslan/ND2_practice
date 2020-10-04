using System.Collections.Generic;
using System.Linq;
using TicketReSail.Business.Model;

namespace TicketReSail.Business
{
    public class TicketsRepository
    {
        private readonly List<Ticket> _tickets;
        private readonly EventsRepository _events;
        private readonly UserRepository _users;

        public TicketsRepository(EventsRepository eventsRepository, UserRepository userRepository)
        {
            _events = eventsRepository;
            _users = userRepository;

            _tickets = new List<Ticket>
            {
                new Ticket{Id = 1, Event = _events.GetEvents()[0], Price = 5m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 2, Event = _events.GetEvents()[1], Price = 7m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 3, Event = _events.GetEvents()[2], Price = 10m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 4, Event = _events.GetEvents()[3], Price = 4.5m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 5, Event = _events.GetEvents()[4], Price = 6m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 6, Event = _events.GetEvents()[5], Price = 10m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 7, Event = _events.GetEvents()[6], Price = 5m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 8, Event = _events.GetEvents()[7], Price = 6m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 9, Event = _events.GetEvents()[8], Price = 7m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 10, Event = _events.GetEvents()[9], Price = 6m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 11, Event = _events.GetEvents()[10], Price = 9m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 12, Event = _events.GetEvents()[11], Price = 11m, Seller = _users.GetUsers()[0]},

                new Ticket{Id = 13, Event = _events.GetEvents()[0], Price = 12m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 14, Event = _events.GetEvents()[1], Price = 10m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 15, Event = _events.GetEvents()[2], Price = 8m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 16, Event = _events.GetEvents()[3], Price = 4m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 17, Event = _events.GetEvents()[4], Price = 4m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 18, Event = _events.GetEvents()[5], Price = 11m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 19, Event = _events.GetEvents()[6], Price = 15m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 20, Event = _events.GetEvents()[7], Price = 16m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 21, Event = _events.GetEvents()[8], Price = 17m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 22, Event = _events.GetEvents()[9], Price = 6m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 23, Event = _events.GetEvents()[10], Price = 8m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 24, Event = _events.GetEvents()[11], Price = 10m, Seller = _users.GetUsers()[1]},

                new Ticket{Id = 25, Event = _events.GetEvents()[0], Price = 11m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 26, Event = _events.GetEvents()[1], Price = 12m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 27, Event = _events.GetEvents()[2], Price = 13m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 28, Event = _events.GetEvents()[3], Price = 6m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 29, Event = _events.GetEvents()[4], Price = 7m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 30, Event = _events.GetEvents()[5], Price = 8m, Seller = _users.GetUsers()[0]},
                new Ticket{Id = 31, Event = _events.GetEvents()[6], Price = 9m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 32, Event = _events.GetEvents()[7], Price = 10m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 33, Event = _events.GetEvents()[8], Price = 11m, Seller = _users.GetUsers()[1]},
                new Ticket{Id = 34, Event = _events.GetEvents()[9], Price = 18m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 35, Event = _events.GetEvents()[10], Price = 19m, Seller = _users.GetUsers()[2]},
                new Ticket{Id = 36, Event = _events.GetEvents()[11], Price = 20m, Seller = _users.GetUsers()[2]},
            };
        }

        public Ticket[] GetTickets()
        {
            return _tickets.ToArray();
        }

        public List<User> GetTicketsByUserLogin(string login)
        {
            return _users.GetUsers().Where(u => u.Login.Equals(login)).ToList();
        }
    }
}
