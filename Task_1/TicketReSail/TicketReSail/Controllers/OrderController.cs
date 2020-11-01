using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;

namespace TicketReSail.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAction<OrderDTO> _action;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, IAction<OrderDTO> action, IUserService userService)
        {
            _orderService = orderService;
            _action = action;
            _userService = userService;
        }

        public IActionResult IndexBuy()
        {
            return View("IndexBuy");
        }

        public IActionResult IndexSell()
        {
            return View("IndexSell");
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> Buy(string status)
        {
            return View("Buy", await _orderService.GetOrdersForBuy(status, User.Identity.Name));
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> Confirmed(string status)
        {
            return View("Confirmed", await _orderService.GetOrdersForBuy(status, User.Identity.Name));
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> Rejected(string status)
        {
            return View("Rejected", await _orderService.GetOrdersForBuy(status, User.Identity.Name));
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> Sell(string status)
        {
            return View("Sell", await _orderService.GetOrdersForSell(status, User.Identity.Name));
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> WaitingConfirm(string status)
        {
            return View("WaitingConfirm", await _orderService.GetOrdersForSell(status, User.Identity.Name));
        }

        [HttpGet("[controller]/[action]/{status}")]
        public async Task<IActionResult> Sold(string status)
        {
            return View("Sold", await _orderService.GetOrdersForSell(status, User.Identity.Name));
        }

        public async Task<IActionResult> CreateOrderWithStatusWaiting(int ticketId, string sellerId)
        {
            var order = _orderService.GetOrderByTicketId(ticketId);

            if (order.UserIdBuyer == null)
            {
                order.UserIdBuyer = _userService.GetUserIdByName(User.Identity.Name);
                await _orderService.SetStatusForOrderSeller(ticketId, sellerId);
            }
            else
            {
                var orderDto = new OrderDTO
                {
                    Status = Constants.Waiting,
                    TicketId = ticketId,
                    SellerId = sellerId,
                    BuyerId = _userService.GetUserIdByName(User.Identity.Name)
                };

                await _action.Create(orderDto);
            }

            return RedirectToAction("Buy", new {status = Constants.Waiting});
        }

        public async Task<IActionResult> ConfirmOrder(int ticketId, string buyerId, string sellerId)
        {
            await _orderService.ChangeStatusToSoldForSeller(ticketId, sellerId, buyerId);

            return RedirectToAction("Sold", new {Constants.Confirmed});
        }
    }
}
