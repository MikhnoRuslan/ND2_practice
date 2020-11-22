using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReSail.Core.Enuns;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IAction<EventDTO, Event> _action;
        private readonly IVenueService _venueService;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;

        public EventController(IEventService eventService,
            ICategoryService categoryService, IVenueService venueService,
            IAction<EventDTO, Event> action, ICityService cityService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _venueService = venueService;
            _action = action;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new  EventsViewModel
            {
                SortBy = new SelectList(new List<SortBy>()
                {
                    SortBy.Date,
                    SortBy.Venue,
                    SortBy.City
                }),

                SortOrder = new SelectList(new List<SortOrder>()
                {
                    SortOrder.None,
                    SortOrder.Ascending,
                    SortOrder.Descending
                })
            };
            return View(model);
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var eventInfo = new EventsViewModel()
            {
                Tickets = (await _eventService.GetTicketsByIdEvents(id)).ToList(),
                Event = (await _eventService.GetEventById(id)),
                VenueId = id
            };

            return View("Details", eventInfo);
        }

        public async Task<IActionResult> CreateEvent([FromQuery] VenueQuery venueQuery)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            ViewBag.Venues = new SelectList(await _venueService.GetVenuesByQuery(venueQuery), "Id", "Name");

            var eventsViewModel = new EventsViewModel
            {
                Cities = (await _cityService.GetCities()).ToArray()
            };

            return View(eventsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Administrator)]
        public async Task<IActionResult> CreateEvent(EventsViewModel eventView)
        {
            if (ModelState.IsValid)
            {
                var eventDto = new EventDTO
                {
                    Name = eventView.Name,
                    CategoryId = eventView.CategoryId,
                    DateTime = eventView.Date,
                    VenueId = eventView.VenueId,
                    Description = eventView.Description,
                    Banner = eventView.Banner
                };

                var operationDetails = await _action.Create(eventDto);
                if (operationDetails.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return View();
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Constants.Administrator)]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _action.Delete(id);

            return RedirectToAction("Index");
        }

        public async Task<FileContentResult> GetImage(int id)
        {
            var @event = await _eventService.GetEventById(id);

            return @event != null ? File(@event.Banner, "image/jpg") : null;
        }
    }
}