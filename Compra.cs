using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class Compra
    {
        public int idCompra {get; set; }
        public Usuario usuario {get; set;}
        public int idUsuario  { get; set; }
        public List<CompraProducto> compraProducto { get; set; } = new List<CompraProducto>();
        public ICollection<Producto> productos { get; } = new List<Producto>();
        public double total { get; set; }

        public Compra() { }
        public Compra(Usuario usuario, List<Producto> productos, double total ) 
        {
            this.usuario = usuario;
            idUsuario = usuario.idUsuario;
            this.productos = productos;
            this.total = total;
        }


    }
}
