using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly TicketsContext _context;

        public CityController(ICityService cityService, TicketsContext context)
        {
            _cityService = cityService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_cityService.GetCities());
        }

        public IActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CityViewModel cityView, string returnUrl)
        {
            if (!ModelState.IsValid) return Redirect(returnUrl);
            var city = new City { Name = cityView.Name };
            var result = await _context.Cities.AddAsync(city);
            if (result != null)
                await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
                _context.Cities.Remove(city);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }
    }
}
