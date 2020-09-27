using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ticket_re_sailing.Business;
using Ticket_re_sailing.Business.Model;
using Ticket_re_sailing.Models;

namespace Ticket_re_sailing.Controllers
{
    public class BuyOrdersController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public BuyOrdersController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult WaitingForConfirmation()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Buyer.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.WaitingForConfirmation)).ToList()
            };

            return View("BuyOrdersList", orders);
        }

        public IActionResult Confirmation()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Buyer.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.Confirmation)).ToList()
            };

            return View("BuyOrdersList", orders);
        }

        public IActionResult Rejected()
        {
            var orders = new OrdersViewModel
            {
                Orders = _orderRepository.GetOrders()
                    .Where(u => u.Buyer.Login.Equals(User.Identity.Name))
                    .Where(o => o.Status.Equals(Constants.Rejected)).ToList()
            };

            return View("BuyOrdersList", orders);
        }
    }
}
