using asp.net_core_web_api_learn.Data;
using asp.net_core_web_api_learn.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_learn.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProducts = _context.Products.Include(hh => hh.Category).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(hh => hh.ProductName.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(hh => hh.ProductName);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "productName_desc":
                        allProducts = allProducts.OrderByDescending(hh => hh.ProductName);
                        break;
                    case "price_asc":
                        allProducts = allProducts.OrderBy(hh => hh.Price);
                        break;
                    case "price_desc":
                        allProducts = allProducts.OrderByDescending(hh => hh.Price);
                        break;
                }
            }
            #endregion

            var result = PaginatedList<Data.Product>.CreatePage(allProducts, page, PAGE_SIZE);

            return result.Select(hh => new ProductModel
            {
                ProductId = hh.ProductId,
                ProductName = hh.ProductName,
                Price = hh.Price,
                CategoryName = hh.Category?.CategoryName
            }).ToList();
        }

        public ProductModel GetById(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
            if (product != null)
            {
                return new ProductModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    CategoryName = product.Category?.CategoryName,
                };
            }
            return null;
        }

        public ProductModel Add(ProductVM product)
        {
            Data.Product newProduct = new Data.Product()
            {
                ProductId = Guid.NewGuid(),
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product?.CategoryId,
                Description = product.Description,
                Discount = product.Discount
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            var category = _context.Categories.SingleOrDefault(ca => ca.CategoryId == newProduct.CategoryId);

            return new ProductModel()
            {
                ProductId = newProduct.ProductId,
                ProductName = newProduct.ProductName,
                Price = newProduct.Price,
                CategoryName = category?.CategoryName,
            };
        }

        public void Update(ProductVM product, string id)
        {
            var productEditing = _context.Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
            //Update
            product.ProductName = productEditing.ProductName;
            product.Price = productEditing.Price;
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}