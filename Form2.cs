using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
{
    public partial class Form2 : Form
    {
        private int DNI;
        private string pass;
        public delegate void TransfDelegado(int ID, string nombre, Object m);
        public TransfDelegado TrasfEvento;
        Mercado m=new Mercado();
        public Form2()
        {
            InitializeComponent();
            textBox8.Text = m.nTargetPath;
        }

        //################################################################################################
        //################################################################################################
        //
        //                         1. PESTAÑA FORMULARIO INICIO DE SESION
        //
        //################################################################################################
        //################################################################################################

        //######################################################
        //               BOTON INICIAR SESION
        //######################################################
        private void button1_Click(object sender, EventArgs e)
        {
            DNI = int.Parse(textBox1.Text);
            pass = inputPass.Text;
            m.nTargetPath = textBox8.Text;

            Usuario u;
            if (( u=m.IniciarSesion(DNI,pass))!=null)
            {
                MessageBox.Show("Te damos la bienvenida!");
                this.TrasfEvento(u.nID,u.nNombre, m);
            }
            else
                MessageBox.Show("Debes registrarte");
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(169, 169, 169);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }


        //######################################################
        //           BOTON VER - OCULTAR CONTRASEÑA
        //            FORMULARIO INICIO DE SESION
        //######################################################

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    if (inputPass.UseSystemPasswordChar == true)
        //    {
        //        inputPass.UseSystemPasswordChar = false;
        //    }
        //    else if (inputPass.UseSystemPasswordChar == false)
        //    {
        //        inputPass.UseSystemPasswordChar = true;
        //    }
        //} by Seba
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


        //################################################################################################
        //################################################################################################
        //
        //                        2. PESTAÑA FORMULARIO REGISTRO DE USUARIO NUEVO
        //
        //################################################################################################
        //################################################################################################

        //######################################################
        //           BOTON REGISTRAR USUARIO NUEVO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
            //si puso todos los datos y se registra sin problemas en la lista de usuarios
            //se salta a iniciar sesion
            try
            {
                m.AgregarUsuario(int.Parse(textDNI.Text), textNombre.Text, textApellido.Text, textMail.Text, textPass.Text, int.Parse(textCUIT_CUIL.Text), 1);
                textDNI.Text = "";
                textNombre.Text = "";
                textApellido.Text = "";
                textMail.Text = "";
                textPass.Text = "";
                textCUIT_CUIL.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR ingreso dato incorrecto");
            }

            tabControl1.SelectedTab = tabPage1;
        }

        //######################################################
        //           BOTON VER - OCULTAR CONTRASEÑA
        //        FORMULARIO REGISTRO DE USUARIO NUEVO
        //######################################################
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    if (textPass.UseSystemPasswordChar == true)
        //    {
        //        textPass.UseSystemPasswordChar = false;
        //    }
        //    else if (textPass.UseSystemPasswordChar == false)
        //    {
        //        textPass.UseSystemPasswordChar = true;
        //    }
        //} by Seba   iconButton1_Click

        private void button6_Click(object sender, EventArgs e)
        {
            if (textPass.UseSystemPasswordChar == true)
            {
                textPass.UseSystemPasswordChar = false;
            }
            else if (textPass.UseSystemPasswordChar == false)
            {
                textPass.UseSystemPasswordChar = true;
            }
        }
        //######################################################
        //                   BOTON BORRAR
        //        FORMULARIO REGISTRO DE USUARIO NUEVO
        //######################################################
        private void button7_Click(object sender, EventArgs e)
        {
            textDNI.Text="";
            textNombre.Text = "";
            textApellido.Text = "";
            textMail.Text = "";
            textPass.Text = "";
            textCUIT_CUIL.Text = "";



        }
        //######################################################
        //                   BOTON VOLVER
        //        FORMULARIO REGISTRO DE USUARIO NUEVO
        //######################################################
        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }



        //################################################################################################
        //################################################################################################
        //
        //                                   3. PESTAÑA CONFIGURACIÓN
        //
        //################################################################################################
        //################################################################################################
        


        //######################################################
        //                   BOTON CAMBIAR 
        //             FORMULARIO CONFIGURACION
        //######################################################R
        private void button8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            m.nTargetPath = fbd.SelectedPath;
            textBox8.Text = m.nTargetPath;
        }

        //######################################################
        //                   BOTON VOLVER
        //             FORMULARIO CONFIGURACION
        //######################################################
        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        
    }
}
