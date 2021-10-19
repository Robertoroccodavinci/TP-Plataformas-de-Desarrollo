using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_PlataformasDeDesarrollo
{
    class Usuario : IComparable<Usuario>
    {
        private int ID;
        private int DNI;
        private string Nombre;
        private string Apellido;
        private string Mail;
        private string Password;
        private long CUIT_CUIL;
        private Carro MiCarro;
        private int rol; // 1-Cliente 2-Empresa 3-Administrador

        public Usuario(int ID, int DNI, string Nombre, string Apellido, string Mail, string Password, long CUIT_CUIL, int rol)
        {
            nDNI = DNI;
            nID = ID;
            nNombre = Nombre;
            nApellido = Apellido;
            nMail = Mail;
            nPassword = Password;
            nCarro = new Carro(nID);
            nCUIT_CUIL = CUIT_CUIL;
            nRol = rol;
        }

        public int nID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int nDNI
        {
            get { return DNI; }
            set { DNI = value; }
        }

        public string nNombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        public string nApellido
        {
            get { return Apellido; }
            set { Apellido = value; }
        }

        public string nMail
        {
            get { return Mail; }
            set { Mail = value; }
        }

        public string nPassword
        {
            get { return Password; }
            set { Password = value; }
        }


        public Carro nCarro
        {
            get { return MiCarro; }
            set { MiCarro = value; }
        }

        public long nCUIT_CUIL
        {
            get { return CUIT_CUIL; }
            set { CUIT_CUIL = value; }
        }

        public int nRol
        {
            get { return rol; }
            set { rol = value; }
        }

        public int CompareTo(Usuario otro)
        {
            return DNI.CompareTo(otro.DNI);
        }

        public override string ToString()
        {
            return nID + "-" + nDNI + "-" + nNombre + "-" + nApellido + "-" + nMail + "-" + nPassword;
        }
    }
}
