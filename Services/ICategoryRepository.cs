using asp.net_core_web_api_learn.Model;

namespace asp.net_core_web_api_learn.Services
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(int id);
        CategoryVM Add(CategoryModel category);
        void Update(CategoryVM category);
        void Delete(int id);
    }
}