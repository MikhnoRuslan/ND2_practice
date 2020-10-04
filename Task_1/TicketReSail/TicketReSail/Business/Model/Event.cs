using System;

namespace TicketReSail.Business.Model
{
    public class Event
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public string Banner { get; set; }
        public string Description { get; set; }
    }
}
