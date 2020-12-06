using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;

namespace TicketReSail.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IAction<OrderDTO, Order> _action;
        private readonly IUserService _userService;
        private readonly ITickerService _tickerService;

        public OrderController(IOrderService orderService, IAction<OrderDTO, Order> action,
            IUserService userService, ITickerService tickerService)
        {
            _orderService = orderService;
            _action = action;
            _userService = userService;
            _tickerService = tickerService;
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
            return View("Sell", await _tickerService.GetTicketsForSell(status, User.Identity.Name));
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

        public async Task<IActionResult> CreateOrderWithStatusWaiting(int ticketId, string sellerId, int eventId)
        {
            var orderDto = new OrderDTO
            {
                TicketId = ticketId,
                SellerId = sellerId,
                BuyerId = _userService.GetUserIdByName(User.Identity.Name),
                Status = Constants.Waiting,
                EventId = eventId
            };

            await _action.Create(orderDto);

            return RedirectToAction("Buy", new {status = Constants.Waiting});
        }

        public async Task<IActionResult> ConfirmOrder(int ticketId, string buyerId)
        {
            await _orderService.ChangeStatusToSoldForSeller(ticketId, buyerId);

            return RedirectToAction("Sold", new {status = Constants.Confirmed});
        }
    }
}
