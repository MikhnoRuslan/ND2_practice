using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Business;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult BuyTicket(string status)
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Buyer.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(status)).ToList()
            };

            return View("Buy", orders);
        }

        public IActionResult SellTicket(string status)
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Ticket.Seller.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(status)).ToList()
            };

            return View("Sell", orders);
        }
    }
}
