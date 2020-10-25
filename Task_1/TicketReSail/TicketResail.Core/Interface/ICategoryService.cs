using System.Collections.Generic;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
