using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface IEventService
    {
        Task<Event> GetEventById(int id);
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Venue>> GetVenues();
        Task<IEnumerable<Event>> GetEvents();
    }
}
