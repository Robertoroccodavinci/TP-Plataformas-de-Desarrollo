using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class ClienteFinal : Usuario 
    {
        private int CUIL;

        public ClienteFinal(int CUIL, string Nombre, string Apellido, string Mail, string Password)
            : base(string Nombre, string Apellido, string Mail, string Password) {
            nCUIL = CUIL;
        }

        public int nCUIL {
            get { return CUIL; }
            set { CUIL = value; }
        }

        public int CompareTo(ClienteFinal otro)
        {
            return base.DNI.CompareTo(otro);
           
        }

        
    }
}
