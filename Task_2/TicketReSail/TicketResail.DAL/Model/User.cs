using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TicketReSail.DAL.Model
{
    public class User : IdentityUser
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Localization { get; set; }
        public string Address { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
