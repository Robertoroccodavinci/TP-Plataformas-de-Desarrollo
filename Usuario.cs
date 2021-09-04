using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    abstract class Usuario 
    {
        private static int ID = 0;
    private int DNI;
    private string Nombre;
    private string Apellido;
    private string Mail;
    private string Password;
    private Carro MiCarro;

    public Usuario(string Nombre, string Apellido, string Mail, string Password)
    {
        this.ID = Usuario.ID++;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Mail = Mail;
        this.Password = Password;
        carro = new Carro(this.ID);
    }

    public static int ID
    {
        get { return ID; }
    }

    public int nDNI
    {
        get { return DNI; }
        set { DNI = value; }
    }

    public string Nombre
    {
        get { return Nombre; }
        set { Nombre = value; }
    }

    public string Apellido
    {
        get { return Apellido; }
        set { Apellido = value; }
    }

    public string Mail
    {
        get { return Mail; }
        set { Mail = value; }
    }

    public string Password
    {
        get { return Password; }
        set { Password = value; }
    }

    public string Password
    {
        get { return Password; }
        set { Password = value; }
    }

    public Carro carro
    {
        get { return Carro; }
        set { Carro = value; }
    }

    public int CompareTo(Usuario otro) { }

    public string toString()
    {


        return "";
    }
}
}
