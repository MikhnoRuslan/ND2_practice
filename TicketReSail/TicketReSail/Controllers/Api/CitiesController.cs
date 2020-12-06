using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IAction<CityDTO, City> _action;

        public CitiesController(ICityService cityService, IAction<CityDTO, City> action)
        {
            _cityService = cityService;
            _action = action;
        }

        /// <summary>
        /// GEt all cities
        /// </summary>
        /// <returns> Lis of cities</returns>
        // GET: api/v1/Cities
        [HttpGet]
        public async Task<IEnumerable<City>> GetCities()
        {
            return await _cityService.GetCities();
        }

        /// <summary>
        /// Get city by id
        /// </summary>
        /// <param name="id"> City id </param>
        /// <returns> Get city </returns>
        // GET: api/Cities/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityService.GetCityBuId(id);

            if (city == null)
                return NotFound();

            return new ObjectResult(city);
        }

        /// <summary>
        /// Create city
        /// </summary>
        /// <param name="cityViewModel"> Entered city </param>
        /// <returns> Added city </returns>
        // POST: api/Cities
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<City>> CreateCity(CityViewModel cityViewModel)
        {
            //TODO validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cityDTO = new CityDTO { Name = cityViewModel.Name };
            var operationDetails = await _action.Create(cityDTO);

            if (operationDetails.Succeeded)
            {
                cityViewModel.Id = _cityService.GetCityIdByName(cityDTO.Name);
                return CreatedAtAction("GetCity", new { id = cityViewModel.Id }, cityViewModel);
            }
            else
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Remove city by id
        /// </summary>
        /// <param name="id"> City id </param>
        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _action.Delete(id);

            return Ok(city);
        }
    }
}
