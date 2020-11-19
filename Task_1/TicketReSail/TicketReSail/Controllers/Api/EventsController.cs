using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;

namespace TicketReSail.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEvents([FromQuery] EventQuery eventQuery)
        {
            var pageResult = await _eventService.GetEvents(eventQuery);
            HttpContext.Response.Headers.Add("x-total-count", pageResult.TotalCount.ToString());

            return Ok(_mapper.Map<IEnumerable<EventDTO>>(pageResult.Items));
        }
    }
}
