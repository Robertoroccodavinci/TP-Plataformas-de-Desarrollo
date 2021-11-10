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
        public List<CompraProducto> compraProducto { get; set; }
        public double total { get; set; }

        public Compra() { }
        public Compra(int idCompra, Usuario usuario,int idUsuario, List<CompraProducto> compraProducto, double total)
        {
            this.idCompra       = idCompra;
            this.usuario        = usuario;
            this.idUsuario      = idUsuario;
            this.compraProducto = compraProducto;
            this.total          = total;
        }

    }
}
