using System.Collections.Generic;
using System.Linq;
using Ticket_re_sailing.Business.Model;

namespace Ticket_re_sailing.Business
{
    public class OrderRepository
    {
        private readonly List<Order> _orders;
        private readonly EventsRepository _events;
        private readonly UserRepository _users;
        private readonly TicketsRepository _tickets;

        public OrderRepository(UserRepository userRepository, EventsRepository eventsRepository, TicketsRepository ticketsRepository)
        {
            _users = userRepository;

            _events = eventsRepository;

            _tickets = ticketsRepository;

            _orders = new List<Order>
            {
                new Order{Id = 1, Status = Constants.Selling, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[0]},
                new Order{Id = 2, Status = Constants.Selling, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[1]},
                new Order{Id = 3, Status = Constants.Sold, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[2]},
                new Order{Id = 4, Status = Constants.Sold, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[3]},
                new Order{Id = 5, Status = Constants.Waiting, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[4]},
                new Order{Id = 6, Status = Constants.Waiting, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[5]},

                new Order{Id = 7, Status = Constants.Selling, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[6]},
                new Order{Id = 8, Status = Constants.Selling, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[7]},
                new Order{Id = 9, Status = Constants.Sold, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[8]},
                new Order{Id = 10, Status = Constants.Sold, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[9]},
                new Order{Id = 11, Status = Constants.Waiting, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[10]},
                new Order{Id = 12, Status = Constants.Waiting, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[11]},

                new Order{Id = 13, Status = Constants.Selling, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[12]},
                new Order{Id = 14, Status = Constants.Selling, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[13]},
                new Order{Id = 15, Status = Constants.Sold, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[14]},
                new Order{Id = 16, Status = Constants.Sold, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[15]},
                new Order{Id = 17, Status = Constants.Waiting, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[16]},
                new Order{Id = 18, Status = Constants.Waiting, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[17]},

                new Order{Id = 19, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[18]},
                new Order{Id = 20, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[19]},
                new Order{Id = 21, Status = Constants.Confirmation, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[20]},
                new Order{Id = 22, Status = Constants.Confirmation, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[21]},
                new Order{Id = 23, Status = Constants.Rejected, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[22]},
                new Order{Id = 24, Status = Constants.Rejected, Buyer = _users.GetUsers()[0], Ticket = _tickets.GetTickets()[23]},

                new Order{Id = 25, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[24]},
                new Order{Id = 26, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[25]},
                new Order{Id = 27, Status = Constants.Confirmation, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[26]},
                new Order{Id = 28, Status = Constants.Confirmation, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[27]},
                new Order{Id = 29, Status = Constants.Rejected, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[28]},
                new Order{Id = 30, Status = Constants.Rejected, Buyer = _users.GetUsers()[1], Ticket = _tickets.GetTickets()[29]},

                new Order{Id = 31, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[30]},
                new Order{Id = 32, Status = Constants.WaitingForConfirmation, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[31]},
                new Order{Id = 33, Status = Constants.Confirmation, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[32]},
                new Order{Id = 34, Status = Constants.Confirmation, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[33]},
                new Order{Id = 35, Status = Constants.Rejected, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[34]},
                new Order{Id = 36, Status = Constants.Rejected, Buyer = _users.GetUsers()[2], Ticket = _tickets.GetTickets()[35]},
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
