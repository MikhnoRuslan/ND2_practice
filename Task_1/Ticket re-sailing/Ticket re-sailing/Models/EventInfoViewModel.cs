using System.Collections.Generic;
using Ticket_re_sailing.Business.Model;

namespace Ticket_re_sailing.Models
{
    public class EventInfoViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public Event Events { get; set; }
    }
}
