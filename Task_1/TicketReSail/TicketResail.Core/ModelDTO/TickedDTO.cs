namespace TicketReSail.Core.ModelDTO
{
    public class TickedDTO
    {
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public bool Bought { get; set; }
    }
}
