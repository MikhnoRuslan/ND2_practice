using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;

namespace TicketReSail.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;
        private readonly IMapper _mapper;

        public VenuesController(IVenueService venueService, IMapper mapper)
        {
            _venueService = venueService;
            _mapper = mapper;
        }

        /// <summary>
        /// Filters venues about city
        /// </summary>
        /// <param name="venueQuery">request cityId</param>
        /// <returns>Returns a filtered query by city</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VenueDTO>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<VenueDTO>> GetVenuesByCity([FromQuery] VenueQuery venueQuery)
        {
            return _mapper.Map<IEnumerable<VenueDTO>>(await _venueService.GetVenues(venueQuery));
        }
    }
}
