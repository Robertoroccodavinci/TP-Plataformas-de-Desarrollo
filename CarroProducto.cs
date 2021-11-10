
namespace TP_Plataformas_de_Desarrollo
{
    class CarroProducto
    {
        public int idCarroProducto { get; set; }
        public int idCarro { get; set; }
        public Carro carro { get; set; }
        public int idProducto { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }

        public CarroProducto() { }
    }
}
