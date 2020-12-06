using System.ComponentModel.DataAnnotations;

namespace TicketReSail.DAL.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name category")]
        public string Name { get; set; }
    }
}
