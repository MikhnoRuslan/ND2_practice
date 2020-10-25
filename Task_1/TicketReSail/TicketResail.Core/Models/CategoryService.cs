using System.Collections.Generic;
using System.Linq;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Models
{
    public class CategoryService : ICategoryService
    {
        private readonly TicketsContext _context;

        public CategoryService(TicketsContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return  _context.Categories.ToList();
        }
    }
}
