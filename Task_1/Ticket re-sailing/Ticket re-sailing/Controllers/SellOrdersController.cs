using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ticket_re_sailing.Business;
using Ticket_re_sailing.Business.Model;
using Ticket_re_sailing.Models;

namespace Ticket_re_sailing.Controllers
{
    public class SellOrdersController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public SellOrdersController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Selling()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Ticket.Seller.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.Selling)).ToList()
            };

            return View("SellOrdersList", orders);
        }

        public IActionResult Sold()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Ticket.Seller.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.Sold)).ToList()
            };

            return View("SellOrdersList", orders);
        }

        public IActionResult Waiting()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Ticket.Seller.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.Waiting)).ToList()
            };

            return View("SellOrdersList", orders);
        }

        
    }
}
