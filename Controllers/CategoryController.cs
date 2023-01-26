using asp.net_core_web_api_learn.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;

namespace asp.net_core_web_api_learn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        public CategoryController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _dbContext.Categories.ToList();
            return Ok(new
            {
                Data = categories
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNewCategory(CategoryVM model)
        {
            try
            {
                var category = new Category
                {
                    CategoryName = model.CategoryName
                };
                _dbContext.Add(category);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById(int id, CategoryVM model)
        {
            var category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, category);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }
        }
    }
}