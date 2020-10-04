using System.Collections.Generic;
using TicketReSail.Business.Model;

namespace TicketReSail.Models
{
    public class EventInfoViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public Event Events { get; set; }
    }
}