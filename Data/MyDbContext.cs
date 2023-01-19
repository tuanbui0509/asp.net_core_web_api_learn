using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;

namespace asp.net_core_web_api_learn.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        #region DbSet
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        #endregion
    }
}