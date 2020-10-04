namespace TicketReSail.Business.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public decimal Price { get; set; }
        public User Seller { get; set; }
    }
}
