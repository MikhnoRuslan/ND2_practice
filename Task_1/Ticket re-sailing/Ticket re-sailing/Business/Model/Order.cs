namespace Ticket_re_sailing.Business.Model
{
    public class Order
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public string Status { get; set; }
        public User Buyer { get; set; }
        public int Track { get; set; }
    }
}
