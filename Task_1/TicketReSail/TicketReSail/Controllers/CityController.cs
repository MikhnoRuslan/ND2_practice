using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IAction<CityDTO, City> _action;

        public CityController(ICityService cityService, IAction<CityDTO, City> action)
        {
            _cityService = cityService;
            _action = action;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _cityService.GetCities());
        }

        public IActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CityViewModel cityView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var cityDto = new CityDTO{Name = cityView.Name};
                var operationDetails = await _action.Create(cityDto);
                if (operationDetails.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return View();
                }
            }

            return NotFound("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _action.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
