using GeekShopping.OrderAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.OrderAPI.Model.Context
{
    public class MySQLContext:DbContext
    {   
        public MySQLContext(DbContextOptions<MySQLContext> options):base(options) { }
               
        public DbSet<OrderHeader> Headers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
    }
}
