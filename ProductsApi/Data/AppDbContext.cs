using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class ProductDatabase : DbContext
    {
        public ProductDatabase(DbContextOptions<ProductDatabase> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}