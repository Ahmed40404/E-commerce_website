using Microsoft.EntityFrameworkCore;

namespace E_commerce.Models
{
    public class AppDbcontext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-084A9VI\\SQLEXPRESS;Initial Catalog=Ecommerce;User ID=sa;Password=123;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
