using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.Core.Queries;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenues(VenueQuery venueQuery);
        string GetCityNameByVenueId(int id);
        string GetNameVenueById(int id);
        string GetVenueAddressByVenueId(int id);
    }
}
