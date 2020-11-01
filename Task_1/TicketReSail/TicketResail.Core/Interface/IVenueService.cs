using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenues();
        string GetCityNameByVenueId(int id);
        string GetNameVenueById(int id);
        string GetVenueAddressByVenueId(int id);
    }
}
