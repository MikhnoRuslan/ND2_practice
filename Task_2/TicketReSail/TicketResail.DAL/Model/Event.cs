using System;
using System.Collections.Generic;

namespace TicketReSail.DAL.Model
{
    public class Event
    {
        public int Id { get; set; }
        public Category CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public string Banner { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
