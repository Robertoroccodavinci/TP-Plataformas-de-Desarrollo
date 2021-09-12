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

        public ClienteFinal(int ID, int DNI, string Nombre, string Apellido, string Mail, string Password, int CUIL)
            : base(ID, DNI, Nombre, Apellido, Mail, Password) {
            nCUIL = CUIL;
        }

        public int nCUIL {
            get { return CUIL; }
            set { CUIL = value; }
        }
        public string ToString()
        {
            return "Cliente Final - " + base.ToString()+"-"+nCUIL;
        }

    }
}
