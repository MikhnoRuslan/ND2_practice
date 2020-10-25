using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly TicketsContext _context;

        public CategoryController(ICategoryService categoryService, TicketsContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetCategories());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryView, string returnUrl)
        {
            if (!ModelState.IsValid) return Redirect(returnUrl);
            var category = new Category { Name = categoryView.Name };
            var result = await _context.Categories.AddAsync(category);
            if (result != null)
                await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
                _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }
    }
}
