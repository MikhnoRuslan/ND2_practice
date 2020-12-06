using System;
using TicketReSail.DAL.Model;

namespace TicketReSail.Controllers.Api.Models
{
    public class EventResource
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime DateTime { get; set; }
        public int VenueId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Venue Venue { get; set; }
    }
}
