using Microsoft.EntityFrameworkCore;

namespace GeekShooping.ProductAPI.Model.Context
{
    public class MySQLContext:DbContext
    {
        public MySQLContext()
        {            
        }
        public MySQLContext(DbContextOptions<MySQLContext> options):base(options) { }
       
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Camiseta Homer Simpsons",
                Price = 159.9M,
                Description = "Camiseta Simpsons",
                ImageUrl = "https://www.stargeek.es/1266-large_default/comprar-camiseta-homer-simpson-biceps-personalizada-ropa-online-de-videojuegos.jpg",
                CategoryName = "T-shirt",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Camiseta Bart Simpsons",
                Price = 250.9M,
                Description = "Camiseta Bart Simpsons",
                ImageUrl = "https://sublistamp.com/11171-large_default/camiseta-de-bart-simpson.jpg",
                CategoryName = "T-shirt",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Caneca Bart",
                Price = 89.9M,
                Description = "Caneca Bart Simpsons",
                ImageUrl = "https://i.zst.com.br/thumbs/51/e/32/1484288505.jpg",
                CategoryName = "COFFE MUG",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "Caneca Hommer",
                Price = 39.9M,
                Description = "Caneca Hommer simpsons",
                ImageUrl = "https://m.media-amazon.com/images/I/71tZUZ6SmyL._AC_UF894,1000_QL80_.jpg",
                CategoryName = "COFFE MUG",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 8,
                Name = "Caneca Programmer",
                Price = 159.9M,
                Description = "Caneca Programmer",
                ImageUrl = "https://i.pinimg.com/originals/0e/a6/63/0ea66335d03125fe176c9dfa1101e719.png",
                CategoryName = "COFFE MUG",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 9,
                Name = "Caneca StarWars",
                Price = 69.9M,
                Description = "Caneca StarWars",
                ImageUrl = "https://m.media-amazon.com/images/I/41fnon3MqbL._AC_UF894,1000_QL80_.jpg",
                CategoryName = "COFFE MUG",
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
