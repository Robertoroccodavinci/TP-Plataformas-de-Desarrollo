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
        ID++;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Mail = Mail;
        this.Password = Password;
        MiCarro = new Carro(nID);
    }

    public static int nID
    {
        get { return ID; }
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

  
    public Carro ncarro
    {
        get { return MiC
                    arro; }
        set { Carro = value; }
    }

    public int CompareTo(Usuario otro) { }

    public string toString()
    {


        return "";
    }
}
}
