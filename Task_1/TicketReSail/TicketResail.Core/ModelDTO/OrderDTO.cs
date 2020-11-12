namespace TicketReSail.Core.ModelDTO
{
    public class OrderDTO
    {
        public string Status { get; set; }
        public int Track { get; set; }
        public int TicketId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public int EventId { get; set; }
        public string TicketStatus { get; set; }
    }
}
