using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Controllers.Api.Models;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;
        private readonly IAction<VenueDTO, Venue> _action;

        public VenuesController(IVenueService venueService, IMapper mapper, IAction<VenueDTO, Venue> action)
        {
            _venueService = venueService;
            _mapper = mapper;
            _action = action;
        }

        /// <summary>
        /// Filters venues about city
        /// </summary>
        /// <param name="venueQuery"> Request cityId </param>
        /// <returns> Returns a filtered query by city </returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetVenuesByCity([FromQuery] VenueQuery venueQuery)
        {
            var venues = await _venueService.GetVenuesByQuery(venueQuery);
            var result = _mapper.Map<IEnumerable<VenueResource>>(venues);

            return Ok(result);
        }

        /// <summary>
        /// Get all venues
        /// </summary>
        /// <returns> List of venues </returns>
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IEnumerable<Venue>> GetVenues()
        //{
        //    return await _venueService.GetVenues();
        //}

        /// <summary>
        /// Get venue by id
        /// </summary>
        /// <param name="id"> Venue id </param>
        /// <returns> Get venue </returns>
        // GET: api/v1/venues/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            var venue = await _venueService.GetVenueById(id);
            if (venue == null)
                return NotFound();

            return new ObjectResult(venue);
        }

        /// <summary>
        /// Create new venue
        /// </summary>
        /// <param name="venueViewModel"> Data to create </param>
        /// <returns> Added venue </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //не работает. не получается передать id города

        public async Task<IActionResult> CreateVenue(VenueViewModel venueViewModel)
        {
            //TODO validation of null
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venueDTO = CreateModelDTO(venueViewModel);

            var operationDetails = await _action.Create(venueDTO);

            if (operationDetails.Succeeded)
            {
                venueViewModel.Id = _venueService.GetVenueIdByName(venueDTO.Name);
                return CreatedAtAction("GetVenue", new { id = venueViewModel.Id }, venueViewModel);
            }
            else
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Update venue
        /// </summary>
        /// <param name="id"> Venue id </param>
        /// <param name="venueViewModel"> Data to update </param>
        /// <returns> Updated venue </returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Venue>> ChangeVenue(int id, VenueViewModel venueViewModel)
        {
            if (venueViewModel == null)
                return BadRequest();

            var venue = await _venueService.GetVenueById(id);
            if (venue == null)
                return NotFound();


            var venueDTO = CreateModelDTO(venueViewModel);
            await _venueService.EditVenue(venue.Id, venueDTO);

            return NoContent();
        }

        /// <summary>
        /// Remove venue by id
        /// </summary>
        /// <param name="id"> Venue id </param>
        // DELETE: api/v1/venues/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Venue>> Delete(int id)
        {
            var venue = await _action.Delete(id);

            return Ok(venue);
        }

        private VenueDTO CreateModelDTO(VenueViewModel venueView)
        {
            var venueDTO = new VenueDTO
            {
                Name = venueView.Name,
                Address = venueView.Address,
                CityId = venueView.CityId
            };

            return venueDTO;
        }
    }
}
