using System;
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
    public class OrderService : IOrderService, IAction<OrderDTO>
    {
        private readonly TicketsContext _context;
        private readonly IUserService _userService;
        private readonly ITickerService _tickerService;

        public OrderService(TicketsContext context, IUserService userService, ITickerService tickerService)
        {
            _context = context;
            _userService = userService;
            _tickerService = tickerService;
        }

        public  Order GetOrderByTicketId(int ticketId)
        {
            return  _context.Orders.FirstOrDefault(o => o.TicketId == ticketId);
        }

        public async Task SetStatusForOrderSeller(int ticketId, string sellerId)
        {
            var order = _context.Orders
                .Where(o => o.Ticket.UserIdSeller.Equals(sellerId))
                .FirstOrDefault(t => t.TicketId == ticketId);

            if (order != null)
            {
                order.Status = Constants.Waiting;
                _context.Update(order);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForBuy(string status, string userName)
        {
            return await _context.Orders
                .Where(u => u.UserIdBuyer.Equals(_userService.GetUserIdByName(userName)))
                .Where(o => o.Status.Equals(status)).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForSell(string status, string userName)
        {
            return await _context.Orders
                .Where(u => u.Ticket.UserIdSeller.Equals(_userService.GetUserIdByName(userName)))
                .Where(o => o.Status.Equals(status)).ToListAsync();
        }

        public async Task ChangeStatusToSoldForSeller(int ticketId, string sellerId, string buyerId)
        {
            var ticket = _tickerService.GetTicketById(ticketId);

            ticket.Bought = true;
            _context.Update(ticket);


            var ordersArray = _context.Orders.Where(o => o.TicketId == ticketId).ToArray();
            foreach (var order in ordersArray)
            {
                if (order.UserIdBuyer.Equals(buyerId))
                {
                    order.Status = Constants.Confirmed;
                    order.Track = Guid.NewGuid();

                    _context.Update(order);
                }
                else
                {
                    order.Status = Constants.Rejected;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<OperationDetails> Create(OrderDTO modelDto)
        {
            var order = new Order
            {
                Status = modelDto.Status,
                TicketId = modelDto.TicketId,
                UserIdBuyer = modelDto.BuyerId
            };
            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return new OperationDetails(true, "Order is successfully created", string.Empty);
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
                _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
    }
}
