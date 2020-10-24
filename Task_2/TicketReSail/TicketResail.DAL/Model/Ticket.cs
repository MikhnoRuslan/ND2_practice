using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReSail.DAL.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
