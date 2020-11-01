using System.Collections.Generic;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
    }
}
