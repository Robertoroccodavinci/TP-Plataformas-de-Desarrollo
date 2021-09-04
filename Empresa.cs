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

        public Empresa(string Nombre, string Apellido, string Mail, string Password,int CUIT, int ID)
            : base(Nombre,  Apellido, Mail,  Password, ID)
        {
            nCUIT = CUIT;
        }

        public int nCUIT {
            get { return CUIT; }
            set { CUIT = value; }
        }

        public string toString()
        {
            return base.toString() + "-" + nCUIT;
        }

    }
}
