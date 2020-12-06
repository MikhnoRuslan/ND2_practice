using System.ComponentModel.DataAnnotations;

namespace TicketReSail.DAL.Model
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
