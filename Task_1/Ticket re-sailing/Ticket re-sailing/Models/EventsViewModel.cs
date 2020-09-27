using System.Collections.Generic;
using Ticket_re_sailing.Business.Model;

namespace Ticket_re_sailing.Models
{
    public class EventsViewModel
    {
        public List<Category> Categories { get; set; }
        public City[] Cities { get; set; }
        public Venue[] Venues { get; set; }
        public Event[] Events { get; set; }
    }
}
