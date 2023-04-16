using asp.net_core_web_api_learn.Models;
using asp.net_core_web_api_learn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_web_api_learn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, string? sortBy = "productName_desc", int page = 1)
        {
            try
            {
                var categories = _categoryRepository.GetAll(search, sortBy, page);
                return Ok(new
                {
                    Data = categories
                });
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _categoryRepository.GetById(id);
                if (category != null)
                {
                    return Ok(category);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult CreateNewCategory(CategoryModel model)
        {
            try
            {
                var category = _categoryRepository.Add(model);
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById(int id, CategoryModel model)
        {
            var category = _categoryRepository.GetById(id);
            if (id != category.CategoryId)
            {
                return BadRequest();
            }
            try
            {
                _categoryRepository.Update(model, id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}