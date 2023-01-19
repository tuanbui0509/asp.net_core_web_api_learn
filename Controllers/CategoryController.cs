using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net_core_web_api_learn.Data;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
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
            var categories = _dbContext.Category.ToList();
            return Ok(new {
                Data = categories
            });
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _dbContext.Category.SingleOrDefault(ca => ca.CategoryId == id);
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
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById(int id, CategoryVM model)
        {
            var category = _dbContext.Category.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                _dbContext.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}