using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> BuyTicket(string status)
        {
            var orders = new OrdersViewModel
            {
                Orders = (await _orderService.GetOrders()).ToArray()
                    .Where(u => u.User.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(status)).ToList()
            };

            return View("Buy", orders);
        }

        public async Task<IActionResult> SellTicket(string status)
        {
            var orders = new OrdersViewModel
            {
                Orders = (await _orderService.GetOrders()).ToList()
                    .Where(u => u.Ticket.User.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(status)).ToList()
            };

            return View("Sell", orders);
        }
    }
}
