using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class Empresa : Usuario
    {
        private int CUIT;

        public Empresa(int ID, int DNI, string Nombre, string Apellido, string Mail, string Password, int CUIT)
            : base(ID, DNI, Nombre, Apellido, Mail,  Password)
        {
            nCUIT = CUIT;
        }

        public int nCUIT {
            get { return CUIT; }
            set { CUIT = value; }
        }

        public string ToString()
        {
            return base.ToString() + "-" + nCUIT;
        }

    }
}
