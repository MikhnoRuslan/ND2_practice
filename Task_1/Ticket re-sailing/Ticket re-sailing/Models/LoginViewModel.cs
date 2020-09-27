using System.ComponentModel.DataAnnotations;

namespace Ticket_re_sailing.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
