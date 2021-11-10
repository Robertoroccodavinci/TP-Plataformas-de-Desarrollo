using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class CategoriaOld
    {
        private int ID;
        private string Nombre;

        public CategoriaOld(int ID, string Nombre)
        {
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


        public override string ToString()
        {
            return nID + "-" + nNombre;
        }
    }
}
