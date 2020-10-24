using System.Collections.Generic;

namespace TicketReSail.DAL.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Venue> Venues { get; set; }
    }
}
