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
    public partial class Form3 : Form
    {
        //NECESITAMOS
        //      NOMBRE, APELLIDO, ID
        //      MERCADO
        //      Y DATOS
        
        public string[] argumentos;
        List<List<string>> datos;
        
        Mercado merc = new Mercado();
        List<string> producto = new List<string>();

        public Form3()
        {
            InitializeComponent();
            Console.WriteLine(merc.nProductos);

            //  DEBERIA LLEVAR PARAMETROS EL CONSTRUCTOR
            //  SE NECESITA NOMBRE O NOMBRE Y APELLIDO
            //  SE NECESITA ID Y TIPO DE USUARIO
            //  SE NECESITA MERCADO
            //
            //  PODRIAMOS PASAR DIRECAMENTE EL ID DEL USUARIO Y CON MERCADO HACERTODO

            // NOMBRE DEL USUARIO
            //label2.Text = u.nNombre;

            //MERCADO
            //merc = m;

            //  SI EL USUARIO ES CLIENTE O EMPRESA
            //  ESCONDEMOS LAS PESTAÑAS DE COMPRAS, USUARIOS 
            // MOSTRAR EL RESTO

            // SI ES ADMIN ESCONDER MI CARRO


            datos = new List<List<string>>();
            string line;

            //########################################################################
            //AGARRAMOS EL ARCHIVO DE PRODUCTOS
            //LO PASAMOS A UN ARRAY
            refreshData(merc);

            /*string[] lines;
            lines = System.IO.File.ReadAllLines(@"" + merc.Dest(0));
            foreach (string l in lines)
            {
                producto.Add(l);
            }
            datos.Add(producto);*/

        }

        private void refreshData(Mercado data)
        {
            //borro los datos
            dataGridView1.Rows.Clear();
            //agrego lo nuevo
            foreach (Categoria c in data.nCategorias)
            {
                if (c != null) { 
                    string[] cate = { c.nID.ToString(),
                                      c.nNombre };
                    
                    dataGridView2.Rows.Add(cate);
                }
            }

            foreach (Producto p in data.nProductos)
            {
                
                string[] prods = { p.nIDProd.ToString(), 
                                   p.nNombre, 
                                   p.nPrecio.ToString(), 
                                   p.nCantidad.ToString(), 
                                   p.nCategoria.nID.ToString() };
                dataGridView1.Rows.Add(prods);
            }
            foreach (Usuario u in data.nUsuarios)
            {
                string r;
                if (u.nRol == 1) r = "Cliente";
                else 
                if (u.nRol == 2) r = "Empresa";
                else  r = "Admin";

                string[] users = { u.nID.ToString(),
                                   u.nDNI.ToString(),
                                   u.nNombre,
                                   u.nApellido,
                                   u.nMail,
                                   u.nPassword,
                                   u.nCUIT_CUIL.ToString(),
                                   r };
                dataGridView3.Rows.Add(users);
            }
            foreach (Compra c in data.nCompras)
            {
                string[] comp = { c.nIDCompra.ToString(),
                    c.nComprador.ToString()
                                    };
                dataGridView3.Rows.Add(comp);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            refreshData(merc); //RECARGA LAS LISTAS
        }

        private void button2_Click(object sender, EventArgs e)
        {
            merc.guardarTodo();//SI LO HACEMOS BOOLEANO PARA COMPROBAR SI FUE EXITOSO O NO
            MessageBox.Show("");// MENSAJE CORROBORANDO QUE SE GUARDO EXITOSAMENTE
        }
    }
}
