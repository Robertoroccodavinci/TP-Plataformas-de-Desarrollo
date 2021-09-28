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
    public partial class Form2 : Form
    {
        private bool logued;
        private string[] argumentos;
        private string usuario;
        private string pass;
        public delegate void TransfDelegado(string usuario,string pass);
        public TransfDelegado TrasfEvento;
        public Form2(string[] args)
        {
            logued = false;
            InitializeComponent();
            argumentos = args;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        //######################################################
        //               BOTON INICIAR SESION
        //######################################################
        private void button1_Click_1(object sender, EventArgs e)
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
        //######################################################
        //           BOTON VER - OCULTAR CONTRASEÑA
        //            FORMULARIO INICIO DE SESION
        //######################################################
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
        //######################################################
        //                  BOTON REGISTRARSE
        //######################################################
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }
        //######################################################
        //                BOTON CONFIGURACION
        //######################################################

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //######################################################
        //           BOTON REGISTRAR USUARIO NUEVO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
            //si puso todos los datos y se registra sin problemas en la lista de usuarios
            //se salta a iniciar sesion
            tabControl1.SelectedTab = tabPage1;
        }
        //######################################################
        //           BOTON VER - OCULTAR CONTRASEÑA
        //        FORMULARIO REGISTRO DE USUARIO NUEVO
        //######################################################
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox6.UseSystemPasswordChar == true)
            {
                textBox6.UseSystemPasswordChar = false;
            }
            else if (textBox6.UseSystemPasswordChar == false)
            {
                textBox6.UseSystemPasswordChar = true;
            }
        }
    }
}
