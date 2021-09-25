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
    public partial class Form1 : Form
    {
        private bool logued;
        private string[] argumentos;
        private string usuario;
        private string pass;
        public delegate void TransfDelegado(string usuario, string pass);
        public TransfDelegado TrasfEvento;

        public Form1(string[] args)
        {
            logued = false;
            InitializeComponent();
            argumentos = args;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuario = textBox1.Text;
            pass = inputPass.Text;

            if (usuario != null && usuario != "" && pass != null && pass != "")
            {
                this.TrasfEvento(usuario, pass);
                this.Close();
            }
            else
                MessageBox.Show("Debe ingresar un usuario!");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (inputPass.UseSystemPasswordChar == true)
            {
                inputPass.UseSystemPasswordChar = false;
            }
            else if (inputPass.UseSystemPasswordChar == false)
            {
                inputPass.UseSystemPasswordChar = true;
            }
               
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
