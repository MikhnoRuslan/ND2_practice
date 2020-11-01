using System.Collections.Generic;

namespace TicketReSail.DAL.Model
{
    public class Localization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
