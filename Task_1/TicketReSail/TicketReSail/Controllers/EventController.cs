using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<EventController> _localizer;
        private readonly TicketsContext _context;

        public EventController(IEventService eventService, IOrderService orderService, IStringLocalizer<EventController> localizer,
            TicketsContext context)
        {
            _eventService = eventService;
            _orderService = orderService;
            _localizer = localizer;
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            //await _context.Database.EnsureDeletedAsync();
            //await _context.Database.EnsureCreatedAsync();

            var categories = (await _eventService.GetCategories()).ToList()
                .Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();

            categories.Insert(0, new Category { Id = 0, Name = _localizer["All"] });

            var eventsViewModel = new EventsViewModel
            {
                Categories = categories,
                Cities = (await _eventService.GetCities()).ToArray(),
                Venues = (await _eventService.GetVenues()).ToArray(),
                Events = (await _eventService.GetEvents()).ToArray()
            };

            if (categoryId != null && categoryId > 0)
                eventsViewModel.Events = (await _eventService.GetEvents()).ToArray()
                    .Where(e => e.Category.Id == categoryId).ToArray();

            return View(eventsViewModel);
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var tic = new EventInfoViewModel()
            {
                Tickets = (await _orderService.GetTicketsByIdEvents(id)).ToList(),
                Events = await _orderService.GetEventsById(id)
            };

            return View("Details", tic);
        }
    }
}