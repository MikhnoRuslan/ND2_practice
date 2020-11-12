using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReSail.DAL.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
