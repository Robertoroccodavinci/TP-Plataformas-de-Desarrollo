using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
{
    public partial class Form1 : Form
    {
        private Form2 hijoLogin;
        private FormCliente hijoCliente;
        private FormAdmin hijoAdmin;
        private Mercado merc;

        public Form1()
        {
            InitializeComponent();
            
            hijoLogin = new Form2();
            hijoLogin.MdiParent = this;
            hijoLogin.TrasfEvento += TransfDelegado;
            hijoLogin.Show();
            
        }
        private void TransfDelegado(int ID, string nombre,Object m)
        {
            merc = (Mercado)m;
            if (merc.esAdmin(ID))
            {
                hijoLogin.Close();
                hijoAdmin = new FormAdmin(ID, nombre, m);
                hijoAdmin.TrasfEvento += TransfDelegado2;
                hijoAdmin.MdiParent = this;
                hijoAdmin.Show();
            }
            else 
            {
                hijoLogin.Close();
                hijoCliente = new FormCliente(ID, nombre, m);
                hijoCliente.TrasfEvento += TransfDelegado2;
                hijoCliente.MdiParent = this;
                hijoCliente.Show();
            }
        }

        private void TransfDelegado2()
        {
            hijoLogin = new Form2();
            hijoLogin.MdiParent = this;
            hijoLogin.TrasfEvento += TransfDelegado;
            hijoLogin.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
