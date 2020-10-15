using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using TicketReSail.Business.Model;

namespace TicketReSail.Business
{
    public class OrderRepository
    {
        private readonly List<Order> _orders;
        private readonly EventsRepository _events;
        private readonly UserRepository _users;
        private readonly TicketsRepository _tickets;
        private readonly IStringLocalizer<OrderRepository> _localizer;

        public OrderRepository(UserRepository userRepository, EventsRepository eventsRepository, TicketsRepository ticketsRepository,
            IStringLocalizer<OrderRepository> localizer)
        {
            _users = userRepository;

            _events = eventsRepository;

            _tickets = ticketsRepository;

            _localizer = localizer;

            _orders = new List<Order>
            {
                new Order{Id = 1, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[0]},
                new Order{Id = 2, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[1]},
                new Order{Id = 3, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[2]},
                new Order{Id = 4, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[3]},
                new Order{Id = 5, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[4]},
                new Order{Id = 6, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[5]},

                new Order{Id = 7, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[6]},
                new Order{Id = 8, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[7]},
                new Order{Id = 9, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[8]},
                new Order{Id = 10, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[9]},
                new Order{Id = 11, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[10]},
                new Order{Id = 12, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[11]},

                new Order{Id = 13, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[12]},
                new Order{Id = 14, Status = _localizer[Constants.Selling].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[13]},
                new Order{Id = 15, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[14]},
                new Order{Id = 16, Status = _localizer[Constants.Sold].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[15]},
                new Order{Id = 17, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[16]},
                new Order{Id = 18, Status = _localizer[Constants.Waiting].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[17]},

                new Order{Id = 19, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[18]},
                new Order{Id = 20, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[19]},
                new Order{Id = 21, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[20]},
                new Order{Id = 22, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[21]},
                new Order{Id = 23, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[22]},
                new Order{Id = 24, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[23]},

                new Order{Id = 25, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[24]},
                new Order{Id = 26, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[25]},
                new Order{Id = 27, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[26]},
                new Order{Id = 28, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[27]},
                new Order{Id = 29, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[28]},
                new Order{Id = 30, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[29]},

                new Order{Id = 31, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[30]},
                new Order{Id = 32, Status = _localizer[Constants.WaitingForConfirmation].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[31]},
                new Order{Id = 33, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[32]},
                new Order{Id = 34, Status = _localizer[Constants.Confirmation].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[33]},
                new Order{Id = 35, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[34]},
                new Order{Id = 36, Status = _localizer[Constants.Rejected].Value, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[35]},
            };
        }

        public List<Order> GetOrders()
        {
            return _orders.ToList();
        }

        public List<Ticket> GetTicketByIdEvents(int id)
        {
            var allTickets = _tickets.GetTickets().Where(ticket => ticket.Event.Id.Equals(id)).ToList();

            return allTickets;
        }

        public Event GetEventsById(int id)
        {
            return _events.GetEventsById(id);
        }

        public List<Order> GetOrderByUserLogin(string login)
        {
            return _orders.Where(u => u.Buyer.Login.Equals(login)).ToList();
        }
    }
}
