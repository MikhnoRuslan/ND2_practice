using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IVenueService _venueService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<EventController> _localizer;
        private readonly TicketsContext _context;

        public EventController(IEventService eventService, IOrderService orderService, IStringLocalizer<EventController> localizer,
            TicketsContext context, ICategoryService categoryService, IVenueService venueService)
        {
            _eventService = eventService;
            _orderService = orderService;
            _localizer = localizer;
            _context = context;
            _categoryService = categoryService;
            _venueService = venueService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = (_categoryService.GetCategories()).ToList()
                .Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();

            categories.Insert(0, new Category { Id = 0, Name = _localizer["All"] });

            var eventsViewModel = new EventsViewModel
            {
                Categories = categories,
                Cities = (await _eventService.GetCities()).ToArray(),
                Venues = _venueService.GetVenues().ToArray(),
                Events = _eventService.GetEvents().ToArray()
            };

            if (categoryId != null && categoryId > 0)
                eventsViewModel.Events = _eventService.GetEvents().ToArray()
                    .Where(e => e.Category.Id == categoryId).ToArray();

            return View(eventsViewModel);
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var eventInfo = new EventInfoViewModel()
            {
                Tickets = (await _orderService.GetTicketsByIdEvents(id)).ToList(),
                Event = await _context.Events.FindAsync(id)
            };

            return View("Details", eventInfo);
        }

        public IActionResult CreateEvent()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Venues = new SelectList(_context.Venues, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EditorEventViewModel eventView)
        {
            if (ModelState.IsValid)
            {
                var @event = new Event
                {
                    Name = eventView.Name,
                    CategoryId = eventView.CategoryId,
                    Date = eventView.Date,
                    Description = eventView.Description,
                    VenueId = eventView.VenueId
                };

                if (eventView.Banner != null)
                {
                    byte[] imageData;

                    using (var binaryReader = new BinaryReader(eventView.Banner.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)eventView.Banner.Length);
                    }

                    @event.Banner = imageData;
                }

                await _context.Events.AddAsync(@event);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Admin");
            }

            return NotFound();
        }

        public FileContentResult GetImage(int id)
        {
            var @event = _context.Events.FirstOrDefault(e => e.Id == id);

            return @event != null ? File(@event.Banner, "image/jpg") : null;
        }
    }
}