using System.Collections.Generic;
using TicketReSail.DAL.Model;

namespace TicketReSail.Models
{
    public class EventInfoViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public Event Event { get; set; }
    }
}