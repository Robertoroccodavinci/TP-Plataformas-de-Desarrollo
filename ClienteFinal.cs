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

        public ClienteFinal(int CUIL, string Nombre, string Apellido, string Mail, string Password, int ID)
            : base( Nombre, Apellido, Mail, Password, ID) {
            nCUIL = CUIL;
        }

        public int nCUIL {
            get { return CUIL; }
            set { CUIL = value; }
        }
        public string toString()
        {
            return base.toString()+"-"+nCUIL;
        }

    }
}
