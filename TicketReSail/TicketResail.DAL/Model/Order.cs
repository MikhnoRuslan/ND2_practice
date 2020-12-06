using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReSail.DAL.Model
{
    public class Order
    {
        public int Id { get; set; }
        public Guid Track { get; set; }
        public string Status { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string UserIdBuyer { get; set; }
        [ForeignKey("UserIdBuyer")]
        public User User { get; set; }
    }
}
