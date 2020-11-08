using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class TicketController : Controller
    {
        private readonly IAction<TickedDTO> _actionTicket;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public TicketController(IAction<TickedDTO> action, IUserService userService,
            IEventService eventService)
        {
            _actionTicket = action;
            _userService = userService;
            _eventService = eventService;
        }

        public async Task<IActionResult> CreateTicket()
        {
            ViewBag.Events = new SelectList(await _eventService.GetEvents(), "Id", "Name");

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketViewModel ticketView, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var ticketDto = new TickedDTO
                {
                    Price = ticketView.Price,
                    Description = ticketView.SellerNote,
                    EventId = id,
                    UserId = _userService.GetUserIdByName(User.Identity.Name),
                    Bought = false,
                    TicketStatus = Constants.Selling
                };

                var operationDetails = await _actionTicket.Create(ticketDto);
                if (operationDetails.Succeeded)
                {
                    return RedirectToAction("Details", "Event", new { id = ticketDto.EventId });
                }
                
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task DeleteTicket(int id)
        {
            await _actionTicket.Delete(id);
        }
    }
}
