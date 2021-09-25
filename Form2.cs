using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_2_PlataformasDeDesarrollo
{
    public partial class Form2 : Form
    {
        Form1 hijoLogin;
        Form3 hijoMain;
        internal string texto;
        string usuario;
        bool logued;
        public bool touched;

        public Form2()
        {
            InitializeComponent();
            logued = false;
            hijoLogin = new Form1(new string[1]);
            IsMdiContainer = true;
            hijoLogin.MdiParent = this;
            hijoLogin.TrasfEvento += TransfDelegado;

            hijoLogin.Show();
            touched = false;
        }

        private void TransfDelegado(string Usuario, string pass)
        {
            this.usuario = Usuario;
            if (usuario != null && usuario != "")
            {
                MessageBox.Show("Log in correcto, Usuario: " + usuario);
                hijoLogin.Close();
                hijoMain = new Form3(new string[] { usuario });
                hijoMain.MdiParent = this;
                hijoMain.Show();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
