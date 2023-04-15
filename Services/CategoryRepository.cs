using asp.net_core_web_api_learn.Data;
using asp.net_core_web_api_learn.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_learn.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        public static int PAGE_SIZE { get; set; } = 5;
        private readonly MyDbContext _context;
        public CategoryRepository(MyDbContext dbContext)
        {
            _context = dbContext;
        }
        public CategoryVM Add(CategoryModel category)
        {
            var newCategory = new Category
            {
                CategoryName = category.CategoryName
            };
            _context.Add(newCategory);
            _context.SaveChanges();
            return new CategoryVM
            {
                CategoryId = newCategory.CategoryId,
                CategoryName = newCategory.CategoryName
            };
        }

        public void Delete(int id)
        {
            var category = _context.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public List<CategoryVM> GetAll(string search, string sortBy, int page = 1)
        {
            var allCategories = _context.Categories.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allCategories = allCategories.Where(hh => hh.CategoryName.Contains(search));
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allCategories = allCategories.OrderBy(hh => hh.CategoryName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "productName_desc":
                        allCategories = allCategories.OrderByDescending(hh => hh.CategoryName);
                        break;
                     case "categoryId_desc":
                        allCategories = allCategories.OrderByDescending(hh => hh.CategoryId);
                        break;
                }
            }
            #endregion

            var result = PaginatedList<Category>.CreatePage(allCategories, page, PAGE_SIZE);

            return result.Select(ca => new CategoryVM()
            {
                CategoryId = ca.CategoryId,
                CategoryName = ca.CategoryName
            }).ToList();
        }

        public CategoryVM GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                return new CategoryVM()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
            }
            return null;
        }

        public void Update(CategoryModel category, int id)
        {
            var _category = _context.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            _category.CategoryName = category.CategoryName;
            _context.SaveChanges();
        }
    }
}