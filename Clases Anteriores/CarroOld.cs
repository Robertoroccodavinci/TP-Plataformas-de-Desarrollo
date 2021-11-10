using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class CarroOld
    {

        private int ID;
        public Dictionary<Producto, int> Productos = new Dictionary<Producto, int>();

        public CarroOld(int ID)
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

            if (nProductos.ContainsKey(key))
            {
                nProductos[key] = value;
            }
            else
            {
                nProductos.Add(key, value);

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
            if (nProductos.ContainsKey(P))
            {
                if (nProductos[P] > Cantidad)
                {
                    nProductos[P] = Cantidad;

                }
                else
                {
                    nProductos.Remove(P);
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Vaciar()
        {
            nProductos.Clear();

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
