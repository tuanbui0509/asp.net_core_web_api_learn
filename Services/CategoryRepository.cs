using asp.net_core_web_api_learn.Data;
using asp.net_core_web_api_learn.Model;

namespace asp.net_core_web_api_learn.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _dbContext;
        public CategoryRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CategoryVM Add(CategoryModel category)
        {
            var newCategory = new Category
            {
                CategoryName = category.CategoryName
            };
            _dbContext.Add(newCategory);
            _dbContext.SaveChanges();
            return new CategoryVM
            {
                CategoryId = newCategory.CategoryId,
                CategoryName = newCategory.CategoryName
            };
        }

        public void Delete(int id)
        {
            var category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }

        public List<CategoryVM> GetAll()
        {
            var categories = _dbContext.Categories.Select(ca => new CategoryVM()
            {
                CategoryId = ca.CategoryId,
                CategoryName = ca.CategoryName
            });
            return categories.ToList();
        }

        public CategoryVM GetById(int id)
        {
            var category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == id);
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

        public void Update(CategoryVM category)
        {
            var _category = _dbContext.Categories.SingleOrDefault(ca => ca.CategoryId == category.CategoryId);
            _category.CategoryName = category.CategoryName;
            _dbContext.SaveChanges();
        }
    }
}