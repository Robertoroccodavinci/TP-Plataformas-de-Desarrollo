using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_Plataformas_de_Desarrollo.Migrations
{
    public partial class AgregamosContenido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "idCategoria", "nombre" },
                values: new object[,]
                {
                    { 1, "Comida" },
                    { 2, "Bebida" },
                    { 3, "Ropa" },
                    { 4, "Articulos de limpieza" },
                    { 5, "Electrodomesticos" },
                    { 6, "Informatica" },
                    { 7, "Herramientas" },
                    { 8, "Electronica" },
                    { 9, "Mascotas" },
                    { 10, "Libreria" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "idUsuario", "apellido", "cuit_cuil", "dni", "mail", "nombre", "password", "rol" },
                values: new object[,]
                {
                    { 1, "Admin", 34865218L, 123456, "admin@gmail.com", "Admin", "123456", 1 },
                    { 2, "Lopez", 25689475L, 654321, "pepitolopez@gmail.com", "Pepito", "654321", 2 },
                    { 3, "Perez", 20321548L, 32154869, "joseperez@hotmail.com", "José", "123456", 3 }
                });

            migrationBuilder.InsertData(
                table: "Carro",
                columns: new[] { "idCarro", "idUsuario" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 2, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Compra",
                columns: new[] { "idCompra", "idUsuario", "total" },
                values: new object[] { (short)1, 2, 723m });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "idProducto", "cantidad", "idCategoria", "nombre", "precio" },
                values: new object[,]
                {
                    { 49, 200, 10, "Resma", 700m },
                    { 15, 20, 6, "Monitor", 28000m },
                    { 16, 20, 6, "Notebook", 95000m },
                    { 32, 150, 7, "Taladro", 15000m },
                    { 33, 150, 7, "Amoladora", 7000m },
                    { 34, 150, 7, "Soldadora", 20000m },
                    { 35, 150, 7, "Sierra", 8000m },
                    { 7, 150, 8, "TV", 80000m },
                    { 36, 150, 8, "Hidrolavadora", 7000m },
                    { 37, 150, 8, "Parlantes", 10000m },
                    { 50, 200, 10, "Tablero dibujo", 3000m },
                    { 38, 150, 8, "Auriculares", 4500m },
                    { 14, 20, 6, "Teclado", 3500m },
                    { 41, 200, 9, "Alimento para Perros", 282m },
                    { 42, 200, 9, "Alimento para Gatos", 144m },
                    { 43, 200, 9, "Cuchas", 3000m },
                    { 44, 200, 9, "Correas", 1200m },
                    { 45, 200, 9, "Juguetes", 600m },
                    { 46, 200, 10, "Cuaderno", 250m },
                    { 47, 200, 10, "Marcadores", 1200m },
                    { 48, 200, 10, "Calculadora", 1500m },
                    { 39, 150, 8, "Celular", 50000m },
                    { 40, 150, 8, "Proyector", 45000m },
                    { 13, 20, 6, "Mouse", 1500m },
                    { 31, 20, 5, "Licuadora", 6000m },
                    { 5, 100, 1, "Palitos", 126m },
                    { 6, 100, 1, "Chizitos", 138m },
                    { 19, 100, 1, "Mani", 121m },
                    { 20, 100, 1, "Nachos", 241m },
                    { 1, 100, 2, "Gaseosa", 125m },
                    { 2, 50, 2, "Cerveza", 120m },
                    { 3, 100, 2, "Agua", 78m },
                    { 17, 100, 2, "Leche", 95m },
                    { 18, 100, 2, "Energizante", 108m },
                    { 9, 50, 3, "Pantalon Deportivo", 6500m },
                    { 10, 50, 3, "Camiseta Deportiva", 6500m },
                    { 21, 50, 3, "Campera", 6000m },
                    { 22, 50, 3, "Sweater", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "idProducto", "cantidad", "idCategoria", "nombre", "precio" },
                values: new object[,]
                {
                    { 23, 50, 3, "Jean", 2000m },
                    { 11, 50, 4, "Lavandina", 49m },
                    { 12, 50, 4, "Escoba", 340m },
                    { 24, 50, 4, "Detergente", 87m },
                    { 25, 50, 4, "Pala", 300m },
                    { 26, 50, 4, "Secador", 800m },
                    { 27, 20, 5, "Heladera", 83000m },
                    { 28, 20, 5, "Lavarropa", 78000m },
                    { 29, 20, 5, "Cocina", 50000m },
                    { 30, 20, 5, "Microondas", 25000m },
                    { 8, 30, 6, "PC", 60000m },
                    { 4, 100, 1, "Papas", 250m }
                });

            migrationBuilder.InsertData(
                table: "CarroProducto",
                columns: new[] { "idCarroProducto", "cantidad", "idCarro", "idProducto" },
                values: new object[] { 1, (byte)2, 2, 47 });

            migrationBuilder.InsertData(
                table: "CompraProducto",
                columns: new[] { "idCompraProducto", "cantidad", "idCompra", "idProducto" },
                values: new object[] { 1, (byte)3, (short)1, 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carro",
                keyColumn: "idCarro",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carro",
                keyColumn: "idCarro",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CarroProducto",
                keyColumn: "idCarroProducto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CompraProducto",
                keyColumn: "idCompraProducto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Carro",
                keyColumn: "idCarro",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Compra",
                keyColumn: "idCompra",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "idUsuario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "idUsuario",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "idCategoria",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "idUsuario",
                keyValue: 2);
        }
    }
}
