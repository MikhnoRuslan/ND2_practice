using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Core.ModelDTO;
using TicketReSail.DAL.Model;
using TicketReSail.Models;

namespace TicketReSail.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IAction<CategoryDTO, Category> _action;
        
        public CategoriesController(ICategoryService categoryService, IAction<CategoryDTO, Category> action)
        {
            _categoryService = categoryService;
            _action = action;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return new ObjectResult(category);
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="categoryView">Entered category characteristics</param>
        /// <returns>Added category</returns>

        // POST api/categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryView)
        {
            if (ModelState.IsValid)
            {
                var categoryDto = new CategoryDTO { Name = categoryView.Name };
                var operationDetails = await _action.Create(categoryDto);
                if (operationDetails.Succeeded)
                    return CreatedAtAction("GetCategory", new {id = _categoryService.GetCategoryIdByName(categoryDto.Name)}, categoryView);
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    return NotFound();
                }
            }

            return NotFound();
        }

        // DELETE api/categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _action.Delete(id);

            return Ok(category);
        }
    }
}
