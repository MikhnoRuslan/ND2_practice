using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReSail.DAL.Model
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
