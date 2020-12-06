using TicketReSail.DAL.Model;

namespace TicketReSail.Models
{
    public class VenueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
