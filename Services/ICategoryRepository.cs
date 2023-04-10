using asp.net_core_web_api_learn.Models;

namespace asp.net_core_web_api_learn.Services
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAll(string search, string sortBy, int page = 1);
        CategoryVM GetById(int id);
        CategoryVM Add(CategoryModel category);
        void Update(CategoryModel category, int id);
        void Delete(int id);
    }
}