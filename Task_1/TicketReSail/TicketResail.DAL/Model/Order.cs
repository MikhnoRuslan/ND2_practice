namespace TicketReSail.DAL.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Track { get; set; }

        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
