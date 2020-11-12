using System;
using Microsoft.AspNetCore.Http;

namespace TicketReSail.Core.ModelDTO
{
    public class EventDTO
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public int VenueId { get; set; }
        public IFormFile Banner { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
