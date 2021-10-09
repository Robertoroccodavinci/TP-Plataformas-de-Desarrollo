using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_PlataformasDeDesarrollo
{
    class Carro
    {

        private int ID;
        public Dictionary<Producto, int> Productos = new Dictionary<Producto, int>();

        public Carro(int ID)
        {
            nID = ID;
        }

        public int nID
        {
            get { return ID; }
            set { ID = value; }
        }

        public Dictionary<Producto, int> nProductos
        {
            get { return Productos; }
            set { Productos = value; }
        }

        public void Set(Producto key, int value)
        {
            if (Productos.ContainsKey(key))
            {
                Productos[key] = value;
            }
            else
            {
                Productos.Add(key, value);
            }
        }


        public bool AgregarProducto(Producto P, int Cantidad)
        {
            Set(P, Cantidad);
            return true;    
            
            //######################################################################
            //VER COMO PONER EL FALSE
        }
        public bool QuitarProducto(Producto P, int Cantidad)
        {
            if (Productos.ContainsKey(P))
            {
                if (Productos[P] > Cantidad)
                {
                    Productos[P] = Productos[P] - Cantidad;

                }
                else
                {
                    Productos.Remove(P);
                }
                return true;
            }
            else
            {
                Console.WriteLine("El producto no se encuentra en la lista");
                return false;
            }

        }
        public void Vaciar()
        {
            Productos.Clear();

        }

        public override string ToString()
        {

            string leer = "";
            foreach (KeyValuePair<Producto, int> kvp in Productos)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                leer += "Key = " + kvp.Key + ", Value = " + kvp.Value + "\n";
            }

            return "ID Carro: " + ID + " - " + leer;

        }
    }
}
