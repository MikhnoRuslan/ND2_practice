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
        private readonly IAction<CategoryDTO, Category> _action;

        public CategoryController(ICategoryService categoryService, IAction<CategoryDTO, Category> action)
        {
            _categoryService = categoryService;
            _action = action;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategories());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryView)
        {
            if (ModelState.IsValid)
            {
                var categoryDto = new CategoryDTO { Name = categoryView.Name };
                var operationDetails = await _action.Create(categoryDto);
                if (operationDetails.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return View();
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _action.Delete(id);

            return RedirectToAction("Index");
        }
    }
}