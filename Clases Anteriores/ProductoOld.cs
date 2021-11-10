using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class ProductoOld : IComparable<Producto>
    {
        private int IDProd;
        private string Nombre;
        private double Precio;
        private int Cantidad;
        public Categoria Cat;

        public ProductoOld(int IDProd, string nombre, double precio, int cant, Categoria cat)
        {
            nIDProd = IDProd;
            nNombre = nombre;
            nPrecio = precio;
            nCantidad = cant;
            nCategoria = cat;
        }

        public int nIDProd
        {
            get { return IDProd; }
            set { IDProd = value; }
        }

        public string nNombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }


        public double nPrecio
        {
            get { return Precio; }
            set { Precio = value; }
        }

        public int nCantidad
        {
            get { return Cantidad; }
            set { Cantidad = value; }
        }

        public Categoria nCategoria
        {
            get { return Cat; }
            set { Cat = value; }
        }

        public int CompareTo(Producto otro)
        {
            return nNombre.CompareTo(otro.nombre);
        }

        public override string ToString()
        {
            return nIDProd + "-" + nNombre + "-" + nPrecio + "-" + nCantidad + "-" + nCategoria.ToString();
        }

    }
}
