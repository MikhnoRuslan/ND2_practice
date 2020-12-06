using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using TicketReSail.Core.Services;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Test
{
    public class TestsOrder
    {
        private List<Order> _orders;
        private List<Ticket> _tickets;
        private List<User> _users;
        private Mock<DbSet<Order>> _orderMock;
        private Mock<DbSet<Ticket>> _ticketMock;
        private Mock<DbSet<User>> _userMock;
        private readonly Mock<TicketsContext> _mock = new Mock<TicketsContext>(new DbContextOptions<TicketsContext>());
        private OrderService _orderService;
        private UserService _userService;
        private TicketService _ticketService;
        private DataSeed _dataSeed = new DataSeed();

        [SetUp]
        public void Setup()
        {
            _users = _dataSeed.GetUsers();
            _userMock = DbContextMock.GetQueryableMockDbSet(_users);

            _tickets = _dataSeed.GetTickets();
            _ticketMock = DbContextMock.GetQueryableMockDbSet(_tickets);

            _orders = _dataSeed.GetOrders();
            _orderMock = DbContextMock.GetQueryableMockDbSet(_orders);
            
            _mock.Setup(c => c.Orders).Returns(_orderMock.Object);
            _mock.Setup(c => c.Tickets).Returns(_ticketMock.Object);
            _mock.Setup(c => c.Users).Returns(_userMock.Object);

            _userService = new UserService(_mock.Object);

            _ticketService = new TicketService(_mock.Object, _userService);

            _orderService = new OrderService(_mock.Object, _userService);
        }

        [Test]
        public async Task WhenCorrectTicketIdAndBuyerId_ThenChangeStatusOrderToConfirmedAndChangeStatusBought()
        {
            var expectedStatusOrder = Constants.Confirmed;
            var ticketId = _dataSeed.GetTicketId(_tickets[0]);

            await _orderService.ChangeStatusToSoldForSeller(ticketId, "id123");

            var resultStatusOrder = _orderService.GetStatusByTicketId(ticketId);
            var resultBoughtTicket = _ticketService.GetStatusBoughtByTicketId(ticketId);

            resultStatusOrder.Should().BeEquivalentTo(expectedStatusOrder);
            resultBoughtTicket.Should().BeTrue();
        }

        [Test]
        public async Task WhenInCorrectTicketIdAndBuyerId_ThenChangeStatusOrderToConfirmedAndChangeStatusBought()
        {
            var expectedStatusOrder = Constants.Rejected;
            var ticketId = _dataSeed.GetTicketId(_tickets[0]);

            await _orderService.ChangeStatusToSoldForSeller(ticketId, "id12311");

            var resultStatusOrder = _orderService.GetStatusByTicketId(ticketId);
            var resultBoughtTicket = _ticketService.GetStatusBoughtByTicketId(ticketId);

            resultStatusOrder.Should().BeEquivalentTo(expectedStatusOrder);
            resultBoughtTicket.Should().BeFalse();
        }
    }
}