using asp.net_core_web_api_learn.Models;

namespace asp.net_core_web_api_learn.Services
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1);
        ProductModel GetById(string id);
        ProductModel Add(ProductVM product);
        void Update(ProductVM product, string id);
        void Delete(string id);
    }
}