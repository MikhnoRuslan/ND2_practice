using System.ComponentModel.DataAnnotations;

namespace TicketReSail.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name category")]
        public string Name { get; set; }
    }
}
