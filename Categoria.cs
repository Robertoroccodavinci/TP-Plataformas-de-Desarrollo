using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class Categoria : IComparable<Categoria>
    {
        private int ID;
        private string Nombre;

        public Categoria(int ID, string Nombre) {
            nID = ID;
            nNombre = Nombre;
        }

        public int nID
        {
            get { return ID; }
            set { ID = value; }
        }
        public string nNombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }
        public int CompareTo(Categoria otro)
        {
            return nNombre.CompareTo(otro.nNombre);
        }

        public string toString()
        {
            return nID + "-" + nNombre;
        }


    }

}
