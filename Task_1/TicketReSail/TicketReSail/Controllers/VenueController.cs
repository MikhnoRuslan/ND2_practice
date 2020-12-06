using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.Queries;

namespace TicketReSail.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        public async Task<IActionResult> Index([FromQuery] VenueQuery venueQuery)
        {
            return View(await _venueService.GetVenuesByQuery(venueQuery));
        }
    }
}
