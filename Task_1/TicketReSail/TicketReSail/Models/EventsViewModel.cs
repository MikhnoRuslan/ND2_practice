using System.Collections.Generic;
using TicketReSail.Business.Model;

namespace TicketReSail.Models
{
    public class EventsViewModel
    {
        public List<Category> Categories { get; set; }
        public City[] Cities { get; set; }
        public Venue[] Venues { get; set; }
        public Event[] Events { get; set; }
    }
}