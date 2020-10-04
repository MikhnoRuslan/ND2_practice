using System.ComponentModel.DataAnnotations;

namespace TicketReSail.Business.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
    }
}
