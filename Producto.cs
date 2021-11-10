using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class Producto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public Categoria cat { get; set; }
        public int idCategoria { get; set; }

        public List<CarroProducto> carroProducto { get; set; }
        public List<CompraProducto> compraProducto { get; set; }

        public Producto() { }
        public Producto(int idProducto, string nombre, double precio, int cantidad, 
                        Categoria cat,int idCategoria,
                        List<CarroProducto> carroProducto, List<CompraProducto> compraProducto)
        {
            this.idProducto     = idProducto;
            this.nombre         = nombre;
            this.precio         = precio;
            this.cantidad       = cantidad;
            this.cat            = cat;
            this.idCategoria    = idCategoria;
            this.carroProducto  = carroProducto;
            this.compraProducto = compraProducto;
        }

        public override string ToString()
        {
            return idProducto + "-" + nombre + "-" + precio + "-" + cantidad + "-" + cat;
        }

    }
}


