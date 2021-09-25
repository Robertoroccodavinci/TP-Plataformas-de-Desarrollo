using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2_PlataformasDeDesarrollo
{
    class Categoria 
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
        

        public string ToString()
        {
            return nID + "-" + nNombre;
        }


    }

}
