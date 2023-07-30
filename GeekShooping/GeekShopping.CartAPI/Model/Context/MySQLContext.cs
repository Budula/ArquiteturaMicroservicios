using GeekShopping.CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.CartAPI.Model.Context
{
    public class MySQLContext:DbContext
    {   
        public MySQLContext(DbContextOptions<MySQLContext> options):base(options) { }
       
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
    }
}
