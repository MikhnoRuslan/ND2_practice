using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket_re_sailing.Business;
using Ticket_re_sailing.Business.Model;
using Ticket_re_sailing.Models;

namespace Ticket_re_sailing.Controllers
{
    public class EventController : Controller
    {
        private readonly EventsRepository _eventsRepository;
        private readonly OrderRepository _orderRepository;

        public EventController(EventsRepository eventsRepository, OrderRepository orderRepository)
        {
            _eventsRepository = eventsRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index(int? categoryId)
        {
            List<Category> categories = _eventsRepository.GetCategories()
                .Select(c => new Category { Id = c.Id, Name = c.Name} ).ToList();
            categories.Insert(0, new Category{Id = 0, Name = "All"});

            EventsViewModel eventsViewModel = new EventsViewModel
            {
                Categories = categories,
                Cities = _eventsRepository.GetCities(),
                Venues = _eventsRepository.GetVenues(),
                Events = _eventsRepository.GetEvents()
            };

            if (categoryId != null && categoryId > 0)
                eventsViewModel.Events = (Event[]) _eventsRepository.GetEvents().Where(e => e.Category.Id == categoryId);

            return View(eventsViewModel);
        }

        
        public IActionResult Details([FromRoute]int id)
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
