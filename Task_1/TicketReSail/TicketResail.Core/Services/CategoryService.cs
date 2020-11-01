using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketReSail.Core.Infrastructure;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail.Core.Services
{
    public class CategoryService : ICategoryService, IAction<CategoryDTO>
    {
        private readonly TicketsContext _context;

        public CategoryService(TicketsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<OperationDetails> Create(CategoryDTO categoryDto)
        {
            var category = new Category{Name = categoryDto.Name};

            var result = _context.Categories.FirstOrDefault(c => c.Name.Equals(categoryDto.Name));

            if (result == null)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Category is successfully created", string.Empty);
            }
            else
            {
                return new OperationDetails(false, "The category with th name still exists!", "Name");
            }
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
                _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}
