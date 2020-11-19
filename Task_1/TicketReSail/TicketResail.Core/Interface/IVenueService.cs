using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.Core.ModelDTO;
using TicketReSail.Core.Queries;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenuesByQuery(VenueQuery venueQuery);
        string GetCityNameByVenueId(int id);
        string GetNameVenueById(int id);
        string GetVenueAddressByVenueId(int id);
        Task<IEnumerable<Venue>> GetVenues();
        Task<Venue> GetVenueById(int id);
        int GetVenueIdByName(string name);
        Task EditVenue(int venueId, VenueDTO venueDto);
    }
}
