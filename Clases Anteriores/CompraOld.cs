using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
   /* class CompraOld : IComparable<Compra>
    {
        private int IDCompra;
        private UsuarioOLD Comprador;
        private Dictionary<Producto, int> Productos = new Dictionary<Producto, int>();
        private double Total;

        public CompraOld() { }

        public CompraOld(int ID, UsuarioOLD Comprador, Dictionary<Producto, int> Producto, double Total)
        {
            nIDCompra = ID;
            nComprador = Comprador;
            foreach (KeyValuePair<Producto, int> kvp in Producto)
            {
                nProductos.Add(kvp.Key, kvp.Value);
            }
            nTotal = Total;


        }

        public int nIDCompra
        {
            get { return IDCompra; }
            set { IDCompra = value; }
        }

        public UsuarioOLD nComprador
        {
            get { return Comprador; }
            set { Comprador = value; }
        }

        public Dictionary<Producto, int> nProductos
        {
            get { return Productos; }
            set { Productos = value; }
        }

        public void Agregar(Producto key, int value)
        {
            nProductos.Add(key, value);
        }

        public double nTotal
        {
            get { return Total; }
            set { Total = value; }
        }

        public int CompareTo(Compra otro)
        {
            return nIDCompra.CompareTo(otro.nIDCompra);
        }

        public override string ToString()
        {

            string leer = "";
            foreach (KeyValuePair<Producto, int> kvp in Productos)
            {
                leer += "Key = " + kvp.Key + ", Value = " + kvp.Value + "\n";
            }

            return nIDCompra + "-" + nComprador + "-" + nTotal + "-" + leer;

        }
    }*/
}
