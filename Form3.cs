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
            //merc = m;
            //label2.Text = u.nNombre;
            //string[] linea;
            //lines = System.IO.File.ReadAllLines(merc.Dest(0));

            datos = new List<List<string>>();

            
            //List<string> producto1 = new List<string>(new string[] { "TV", "50000", "200" });



            string line;


            //########################################################################
            // AGARRAMOS EL ARCHIVO DE PRODUCTOS
            //LO PASAMOS A UN ARRAY
            string[] lines;
            lines = System.IO.File.ReadAllLines(@"" + merc.Dest(0));

            
            foreach (string l in lines)
            {
                producto.Add(l);
            }
            datos.Add(producto);

        }

        

        private void refreshData(List<string> data)
        {
            //borro los datos
            dataGridView1.Rows.Clear();

            //agrego lo nuevo
            
            foreach (string p in data)
            {
                string[] lines = p.Split(',');
                dataGridView1.Rows.Add(lines);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("LINEAS");
            refreshData(producto);
        }
    }
}
