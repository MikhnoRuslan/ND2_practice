using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketReSail.Business;
using TicketReSail.Business.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class EventController : Controller
    {
        private readonly EventsRepository _eventsRepository;
        private readonly OrderRepository _orderRepository;
        private readonly IStringLocalizer<EventController> _localizer;

        public EventController(EventsRepository eventsRepository, OrderRepository orderRepository, IStringLocalizer<EventController> localizer)
        {
            _eventsRepository = eventsRepository;
            _orderRepository = orderRepository;
            _localizer = localizer;
        }

        public IActionResult Index(int? categoryId)
        {
            var categories = _eventsRepository.GetCategories()
                .Select(c => new Category { Id = c.Id, Name = c.Name }).ToList();
            categories.Insert(0, new Category { Id = 0, Name = _localizer["All"] });

            var eventsViewModel = new EventsViewModel
            {
                Categories = categories,
                Cities = _eventsRepository.GetCities(),
                Venues = _eventsRepository.GetVenues(),
                Events = _eventsRepository.GetEvents()
            };

            if (categoryId != null && categoryId > 0)
                eventsViewModel.Events = _eventsRepository.GetEvents().Where(e => e.Category.Id == categoryId).ToArray();

            return View(eventsViewModel);
        }


        public IActionResult Details([FromRoute] int id)
        {
            var tic = new EventInfoViewModel()
            {
                Tickets = _orderRepository.GetTicketByIdEvents(id),
                Events = _orderRepository.GetEventsById(id)
            };

            return View("Details", tic);
        }
    }
}