using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;

namespace TicketReSail.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _cityService.GetCities());
        }
    }
}
