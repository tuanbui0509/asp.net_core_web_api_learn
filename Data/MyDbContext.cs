using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_learn.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(o => o.OrderId);
                e.Property(o => o.OrderDay).HasDefaultValueSql("getutcdate()");
                e.Property(o => o.ReceivePerson).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.HasOne(e => e.Order)
                                    .WithMany(e => e.OrderDetails)
                                    .HasForeignKey(e => e.OrderId)
                                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.ProductId)
                    .HasConstraintName("PK_ProductDetail_Order");
                // entity.HasOne(e => e.Order)
                //     .WithMany(e => e.OrderDetails)
                //     .HasForeignKey(e => e.OrderId)
                //     .HasConstraintName("FK_OrderDetail_Order");

                // entity.HasOne(e => e.Product)
                //     .WithMany(e => OrderDetails)
                //     .HasForeignKey(e => e.ProductId)
                //     .HasConstraintName("PK_ProductDetail_Order");
            });
        }
    }
}