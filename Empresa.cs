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

        public Empresa(string Nombre, string Apellido, string Mail, string Password,int CUIT)
            : base(string Nombre, string Apellido, string Mail, string Password){
            this.CUIT = CUIT;
        }

        public int nCUIT {
            get { return CUIT; }
            set { CUIT = value; }
        }
        public int CompareTo(Empresa otro)
        {
            return base.DNI.CompareTo(otro);
        }

    }
}
