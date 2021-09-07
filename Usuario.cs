using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    abstract class Usuario : IComparable<Usuario>
    {
    private int ID = 0;
    private int DNI;
    private string Nombre;
    private string Apellido;
    private string Mail;
    private string Password;
    private Carro MiCarro;

    public Usuario(int ID, int DNI, string Nombre, string Apellido, string Mail, string Password)
    {
        nDNI = DNI;
        nID = ID;
        nNombre = Nombre;
        nApellido = Apellido;
        nMail = Mail;
        nPassword = Password;
        nCarro = new Carro(nID);
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

    public int CompareTo(Usuario otro) {
        return DNI.CompareTo(otro.DNI);
    }

    public string ToString()
    {
        return nID+"-"+nDNI+ "-" + nNombre+"-"+nApellido+"-"+nMail+"-"+nPassword+"-"+nCarro.ToString();
    }
}
}
