using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekShooping.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 4L, "T-shirt", "Camiseta Simpsons", "https://www.stargeek.es/1266-large_default/comprar-camiseta-homer-simpson-biceps-personalizada-ropa-online-de-videojuegos.jpg", "Camiseta Homer Simpsons", 159.9m },
                    { 5L, "T-shirt", "Camiseta Bart Simpsons", "https://sublistamp.com/11171-large_default/camiseta-de-bart-simpson.jpg", "Camiseta Bart Simpsons", 250.9m },
                    { 6L, "COFFE MUG", "Caneca Bart Simpsons", "https://i.zst.com.br/thumbs/51/e/32/1484288505.jpg", "Caneca Bart", 89.9m },
                    { 7L, "COFFE MUG", "Caneca Hommer simpsons", "https://m.media-amazon.com/images/I/71tZUZ6SmyL._AC_UF894,1000_QL80_.jpg", "Caneca Hommer", 39.9m },
                    { 8L, "COFFE MUG", "Caneca Programmer", "https://i.pinimg.com/originals/0e/a6/63/0ea66335d03125fe176c9dfa1101e719.png", "Caneca Programmer", 159.9m },
                    { 9L, "COFFE MUG", "Caneca StarWars", "https://m.media-amazon.com/images/I/41fnon3MqbL._AC_UF894,1000_QL80_.jpg", "Caneca StarWars", 69.9m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 9L);
        }
    }
}
