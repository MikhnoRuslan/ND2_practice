namespace TicketReSail.DAL.Model
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public City CityId { get; set; }

        public City City { get; set; }
    }
}
