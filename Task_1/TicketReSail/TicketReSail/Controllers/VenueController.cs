using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class VenueController : Controller
    {
        private readonly TicketsContext _context;
        private readonly IVenueService _venueService;
        

        public VenueController(TicketsContext context, IVenueService venueService)
        {
            _context = context;
            _venueService = venueService;
        }

        public IActionResult Index()
        {
            return View(_venueService.GetVenues());
        }

        public IActionResult CreateVenue()
        {
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(VenueViewModel venueView)
        {
            if (ModelState.IsValid)
            {
                var venue = new Venue
                {
                    Name = venueView.Name,
                    Address = venueView.Address,
                    CityId = venueView.CityId
                };
                var result = await _context.Venues.AddAsync(venue);

                if (result != null)
                    await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
                _context.Venues.Remove(venue);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
