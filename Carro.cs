using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TP_Plataformas_de_Desarrollo
{
    class Carro 
    {
        
        private int ID;
        private Dictionary<Producto, int> Productos = new Dictionary<Producto, int>();

        public Carro(int ID)
        {
            this.ID = ID;
        }

        public void AgregarProducto(Producto P, int Cantidad)
        {
            Productos.Add(P, Cantidad);
        }
        public void QuitarProducto(Producto P, int Cantidad) 
        {
            if (Cantidad > 0)
            {
                Productos[P]= Productos[P]-Cantidad;
            }
            else {
                Productos.Remove(P);
            }
            
        }
        public void Vaciar()
        {
            Productos.Clear();
        }

        public override string ToString()
        {
            
            string leer="";
            foreach (KeyValuePair<Producto, int> kvp in Productos)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                leer+="Key = " + kvp.Key + ", Value = " + kvp.Value+"\n";
            }

            return "ID: " + ID + " - " + leer;
            
        }
    }
}
