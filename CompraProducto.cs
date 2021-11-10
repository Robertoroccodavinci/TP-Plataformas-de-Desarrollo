
namespace TP_Plataformas_de_Desarrollo
{
    class CompraProducto
    {
        public int idCompraProducto { get; set; }
        public int idCompra { get; set; }
        public Compra compra { get; set; }
        public int idProducto { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }

        public CompraProducto() { }
      
    }
}
