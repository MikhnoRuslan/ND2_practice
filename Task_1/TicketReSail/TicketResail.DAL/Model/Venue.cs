using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReSail.DAL.Model
{
    public class Venue
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int CityId { get; set; }
        [Required]
        public City City { get; set; }
        public ICollection<Event> Tickets { get; set; }
    }
}
