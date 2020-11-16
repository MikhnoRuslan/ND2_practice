using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategories());
        }
    }
}