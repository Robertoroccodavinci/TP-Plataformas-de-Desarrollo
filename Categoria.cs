using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class Categoria
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }

        public ICollection<Producto> productos { get; set; }
        
        public Categoria() { }

        public Categoria(int ID, string Nombre)
        {
            this.idCategoria = ID;
            this.nombre = Nombre;
        }
        
        public override string ToString()
        {
            return idCategoria + "-" + nombre;
        }
    }
}
