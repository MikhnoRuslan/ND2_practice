using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly ICityService _cityService;
        private readonly IAction<VenueDTO> _action;

        public VenueController(IVenueService venueService, ICityService cityService, IAction<VenueDTO> action)
        {
            _venueService = venueService;
            _cityService = cityService;
            _action = action;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _venueService.GetVenues());
        }

        public async Task<IActionResult> CreateVenue()
        {
            ViewBag.Cities = new SelectList(await _cityService.GetCities(), "Id", "Name");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(VenueViewModel venueView)
        {
            if (ModelState.IsValid)
            {
                var venueDto = new VenueDTO
                {
                    Name = venueView.Name,
                    Address = venueView.Address,
                    CityId = venueView.CityId
                };

                var operationDetails = await _action.Create(venueDto);
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
        public async Task<IActionResult> DeleteVenue(int id)
        {
            await _action.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
