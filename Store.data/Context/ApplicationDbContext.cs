using Microsoft.EntityFrameworkCore;

using Store.data.Entity;

namespace Store.data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods{ get; set; }
    }
}
