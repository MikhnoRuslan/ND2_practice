namespace TicketReSail.DAL.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event EventId { get; set; }
        public decimal Price { get; set; }
        public User SellerId { get; set; }

        //public Event Event { get; set; }
        //public User User { get; set; }
    }
}
