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
        Form2 hijoLogin;
        Form3 hijoMain;
        
        bool logued;

        internal string texto;
        string usuario;
        public bool touched;
        Mercado merc;
        public Form1()
        {
            InitializeComponent();
            

            logued = false;
            hijoLogin = new Form2();

            hijoLogin.MdiParent = this;
            hijoLogin.TrasfEvento += TransfDelegado;

            hijoLogin.Show();
            touched = false;
        }
        private void TransfDelegado(int ID, string nombre,Object m)
        {
                hijoLogin.Close();
                hijoMain = new Form3(ID,nombre,m);
                hijoMain.MdiParent = this;
                hijoMain.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
